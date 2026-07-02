using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using Entidades;
using Negocio.Datos;
using System.Linq;
using System.Collections.Generic;

namespace Servicios {
    static class MailServicio {
        public static void enviarMailPrestamoAprobadoRechazado(Prestamo prestamo, string estadoPrestamo) {
            var apiKey = System.Configuration.ConfigurationManager.AppSettings["SENDGRID_API_KEY"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("progra7a@gmail.com", "PrestamoYa");
            var to = new EmailAddress(prestamo.Cliente.Email, prestamo.Cliente.Username);
            var subject = $"Tu préstamo fue ${estadoPrestamo}!";
            var htmlContent = $"<p>Hola <strong>{prestamo.Cliente.Username}</strong>, tu préstamo por <strong>${prestamo.Monto:N2}</strong> fue {estadoPrestamo}!</p>";

            if (estadoPrestamo == "aprobado") {
                CuotaDatos cuotaDatos = new CuotaDatos();
                List<Cuota> listaCuotas = cuotaDatos.ListarPorPrestamo(prestamo.IdPrestamo);

                htmlContent += "<br><br>A continuación te brindamos el plan de cuotas definido:<br><br>";
                htmlContent += "<table style='border-collapse: collapse; width: 400px;'>";
                htmlContent += "<tr style='background-color: #f2f2f2;'>" +
                        "<th style='border: 1px solid #ddd; padding: 10px 20px; text-align: left;'>N° Cuota</th>" +
                        "<th style='border: 1px solid #ddd; padding: 10px 20px; text-align: left;'>Monto</th>" +
                        "<th style='border: 1px solid #ddd; padding: 10px 20px; text-align: left;'>Vencimiento</th>" +
                        "</tr>";

                int numeroCuota = 1;
                listaCuotas.ForEach(c => {
                    htmlContent += $"<tr>" +
                            $"<td style='border: 1px solid #ddd; padding: 10px 20px; text-align: left;'>{numeroCuota}</td>" +
                            $"<td style='border: 1px solid #ddd; padding: 10px 20px; text-align: left;'>${c.Monto:N2}</td>" +
                            $"<td style='border: 1px solid #ddd; padding: 10px 20px; text-align: left;'>{c.FechaVencimiento:dd/MM/yyyy}</td>" +
                            $"</tr>";
                    numeroCuota++;
                });

                htmlContent += "</table><br><br>Cuando quieras abonar, podés acercarte a cualquiera de nuestras sucursales. ¡Te esperamos!";

            } else if (estadoPrestamo == "rechazado") {
                htmlContent += "<br><br>Lamentablemente tu solicitud no pudo ser aprobada en esta ocasión.";
                htmlContent += "<br>Por dudas o inquietudes, por favor acercate a cualquiera de nuestras sucursales. ¡Te esperamos!";
            }

            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
            client.SendEmailAsync(msg).ConfigureAwait(false).GetAwaiter().GetResult();

        }


        public static int envioMailsMasivo(List<Cuota> cuotas) {
            Cliente cliente = new Cliente();
            CuotaDatos cuotaDatos = new CuotaDatos();
            PrestamoDatos prestamoDatos = new PrestamoDatos();

            int contadorCorreos = 0;
            cuotas.ForEach(cuota => {
                var apiKey = System.Configuration.ConfigurationManager.AppSettings["SENDGRID_API_KEY"];
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("progra7a@gmail.com", "PrestamoYa");

                cliente = prestamoDatos.ObtenerPorId(cuota.Prestamo.IdPrestamo).Cliente;
                var to = new EmailAddress(cliente.Email, cliente.Username);
                var subject = $"Tiene cuotas vencidas!";
                var htmlContent = $"<p>Hola <strong>{cliente.Username}</strong>, tu cuota con ID N°{cuota.IdCuota} por <strong>${cuota.Monto:N2}</strong> se encuentra vencida.</p>" +
                $"<br><p>Por favor, acercate a una sucursal para regularizar el credito a la brevedad.</p>";

                var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
                var response = client.SendEmailAsync(msg).ConfigureAwait(false).GetAwaiter().GetResult();

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted) {
                    contadorCorreos++;
                }
            });

            return contadorCorreos;
        }
    }
}