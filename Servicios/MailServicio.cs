using Entidades;
using Negocio.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net;
using System.Net.Mail;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Servicios {
    static class MailServicio {
        public static void enviarMailPrestamoAprobado(Prestamo prestamo) {
            var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525) {
                Credentials = new NetworkCredential("fb35bc4e77c7b5", "41df1bd737b529"),
                EnableSsl = true
            } ;

            CuotaDatos cuotaDatos = new CuotaDatos();
            List<Cuota> cuotas = cuotaDatos.ListarPorPrestamo(prestamo.IdPrestamo);
            string remitente = "progra7a@gmail.com";
            string destinatario = prestamo.Cliente.Email;
            string asunto = "Prestamo aprobado!";
            string cuerpo = $"Estimado {prestamo.Cliente.Username}, desde PrestamoYa le informamos que su prestamo por $${prestamo.Monto} ha sido aprobado!" +
                $"\n\nA continuación le brindamos un detalle de su plan de cuotas:";
            for (int i = 0; i < cuotas.Count; i++) {
                cuerpo += $"\nCuota N°{i+1}: ${cuotas[i].Monto} - Vto: {cuotas[i].FechaVencimiento.ToString()}";
            }


            client.Send(remitente, destinatario, asunto, cuerpo);
        }


        public static void enviarMailPrestamoRechazado(Prestamo prestamo, string observacion) {
            var client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525) {
                Credentials = new NetworkCredential("fb35bc4e77c7b5", "41df1bd737b529"),
                EnableSsl = true
            };

            
            string remitente = "progra7a@gmail.com";
            string destinatario = prestamo.Cliente.Email;
            string asunto = "Prestamo rechazado!";
            string cuerpo = $"Estimado {prestamo.Cliente.Username}, desde PrestamoYa lamentamos informarle que su prestamo por $${prestamo.Monto} ha sido rechazado con la siguiente observación" +
                            $"\n'{observacion}'";

            client.Send(remitente, destinatario, asunto, cuerpo);
        }
    }
}
