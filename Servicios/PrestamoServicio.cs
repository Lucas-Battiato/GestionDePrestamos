using Entidades;
using Negocio.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Servicios {
    public class PrestamoServicio {
        PrestamoDatos prestamoDatos = new PrestamoDatos();

        //Metodo para generar un prestamo SIN GUARDARLO en la DB.
        //Util para llamarlo desde sección de simulación en "GestionDePrestamos/Cliente/SolicitarPrestamo.aspx"
        public Prestamo simular(ProductoPrestamo productoDeseado, decimal montoDeseado, int cuotasDeseadas, Cliente cliente) {
            Prestamo prestamoSimulado = new Prestamo();
            prestamoSimulado.ProductoPrestamo = productoDeseado;
            prestamoSimulado.Cliente = cliente;
            prestamoSimulado.Monto = montoDeseado;
            prestamoSimulado.CantidadCuotas = cuotasDeseadas;
            calcularInteres(prestamoSimulado);

            return prestamoSimulado;
        }


        // Metodo para generar una nueva solicitud de prestamo (NO aprueba, solo lo registra en la DB)
        public int generar(Prestamo prestamo) {
            //Se valida previamente en code behind de "GestionDePrestamos/Cliente/SolicitarPrestamo.aspx".
            // Agregar registro en historial de cambiuos de eestado de prestamo.
            EstadoPrestamo estadoSolicitado = new EstadoPrestamo();
            estadoSolicitado.IdEstadoPrestamo = 1; // Solicitado
            prestamo.EstadoPrestamo = estadoSolicitado;
            int idPrestamo = prestamoDatos.agregar(prestamo);

            HistorialEstadoPrestamo historialSolicitado = new HistorialEstadoPrestamo();
            historialSolicitado.Prestamo = prestamo;
            historialSolicitado.Prestamo.IdPrestamo = idPrestamo;
            historialSolicitado.EstadoPrestamo = estadoSolicitado;


            HistorialEstadoPrestamoDatos historialDatos = new HistorialEstadoPrestamoDatos();
            historialDatos.agregar(historialSolicitado);

            return idPrestamo;
        }
        

        // Metodo para aprobar un prestamo.
        // Registra el cambio en el historial, genera todas las cuotas, envia mail de confirmación.
        public void aprobar(int idPrestamo, Usuario usuario, string observacion) {
            /*
                - Recibo el prestamo
                - Registro cambio de estado prestamo en historial (aprobado)
                - Registro cambio de estado prestamo en historial (En curso)
                - Genero cuotas
                - Envio mail de confirmación con plan de cuotas
            */

            Prestamo prestamo = prestamoDatos.ObtenerPorId(idPrestamo);

            prestamo.UsuarioAprobador = usuario;
            prestamo.FechaAprobacion = DateTime.Now;
            prestamo.CuotasRestantes = prestamo.CantidadCuotas;


            // Registro cambio de estado a aprobado
            EstadoPrestamo estadoAprobado = new EstadoPrestamo();
            estadoAprobado.IdEstadoPrestamo = 2; // Aprobado
            prestamo.EstadoPrestamo = estadoAprobado;
            prestamoDatos.cambiarEstado(prestamo);

            HistorialEstadoPrestamo historialAprobado = new HistorialEstadoPrestamo();
            historialAprobado.Prestamo = prestamo;
            historialAprobado.EstadoPrestamo = estadoAprobado;
            historialAprobado.Usuario = usuario;
            historialAprobado.Observaciones = observacion;

            HistorialEstadoPrestamoDatos historialDatos = new HistorialEstadoPrestamoDatos();
            historialDatos.agregar(historialAprobado);


            // Registro cambio de estado a En Curso.
            EstadoPrestamo estadoEnCurso = new EstadoPrestamo();
            estadoEnCurso.IdEstadoPrestamo = 4; // En curso
            prestamo.EstadoPrestamo = estadoEnCurso;
            prestamoDatos.cambiarEstado(prestamo);

            HistorialEstadoPrestamo historialEnCurso = new HistorialEstadoPrestamo();
            historialEnCurso.Prestamo = prestamo;
            historialEnCurso.EstadoPrestamo = estadoEnCurso;
            historialEnCurso.Observaciones = "Cambio automatico a En Curso";
            historialDatos.agregar(historialEnCurso);


            //Genero todas las cuotas
            CuotaServicio cuotaServicio = new CuotaServicio();
            CuotaDatos cuotaDatos = new CuotaDatos();

            // Tomo como fecha de vencimiento de la primer cuota el 10 del mes siguiente al siguiente de la aprobacion.
            DateTime fechaBase = new DateTime(
                prestamo.FechaAprobacion.Value.Year,
                prestamo.FechaAprobacion.Value.Month,
                10
            ).AddMonths(2);

            Cuota cuota = cuotaServicio.calcularCuota(prestamo); //Me devuelve una cuota con el monto ya calculado.
            cuota.Prestamo = prestamo;
            cuota.EstadoCuota = new EstadoCuota { IdEstadoCuota = 1 }; //Estado Pendiente

            for (int i = 0; i < prestamo.CantidadCuotas; i++) {
                // Seteo la fecha de vencimiento de cada cuota. Si cae sabado o domingo ajusto el dia para que caiga lunes
                DateTime fechaVencimiento = fechaBase.AddMonths(i);
                if (fechaVencimiento.DayOfWeek == DayOfWeek.Saturday)
                    fechaVencimiento = fechaVencimiento.AddDays(2); //Agrego 2 dias para que sea el lunes sigueinte
                else if (fechaVencimiento.DayOfWeek == DayOfWeek.Sunday)
                    fechaVencimiento = fechaVencimiento.AddDays(1); // Agrego 1 dia para que sea el lunes siguiente.

                cuota.FechaVencimiento = fechaVencimiento;

                cuotaDatos.agregar(cuota);
            }

            // Envio mail
            //MailServicio.enviarMailPrestamoAprobado(prestamo);

        }


        // Metodo para rechazar un prestamo. Registra cambios en el historial, envia mail de rechazo.
        public void rechazar(int idPrestamo, Usuario usuario, string observacion) {
            /*
                - Recibo el prestamo
                - Registro cambio de estado prestamo en historial (rechazado)
                - Envio mail de rechazo
            */

            Prestamo prestamo = prestamoDatos.ObtenerPorId(idPrestamo);

            prestamo.UsuarioAprobador = usuario;
            prestamo.CuotasRestantes = 0;


            // Registro cambio de estado a aprobado
            EstadoPrestamo estadoRechazado = new EstadoPrestamo();
            estadoRechazado.IdEstadoPrestamo = 3; // Rechazado
            prestamo.EstadoPrestamo = estadoRechazado;
            prestamoDatos.cambiarEstado(prestamo);

            HistorialEstadoPrestamo historialRechazado = new HistorialEstadoPrestamo();
            historialRechazado.Prestamo = prestamo;
            historialRechazado.EstadoPrestamo = estadoRechazado;
            historialRechazado.Usuario = usuario;
            historialRechazado.Observaciones = observacion;

            HistorialEstadoPrestamoDatos historialDatos = new HistorialEstadoPrestamoDatos();
            historialDatos.agregar(historialRechazado);

            // Envio mail
            //MailServicio.enviarMailPrestamoRechazado(prestamo, observacion);

        }

        private void calcularInteres(Prestamo prestamo) {
            // -Llamar a TasaInteresDatos para consultar la tasa correspondiente al producto y cuotas solicitado.
            // -Calcular interes total del prestamo solicitado y agregarlo al prestamo
            TasaInteresDatos tasaInteresDatos = new TasaInteresDatos();
            List<TasaInteres> listaIntereses = tasaInteresDatos.Listar();
            foreach (TasaInteres tasa in listaIntereses) {
                if (tasa.ProductoPrestamo.IdProducto == prestamo.ProductoPrestamo.IdProducto) {
                    if (prestamo.CantidadCuotas >= tasa.CuotasDesde && prestamo.CantidadCuotas <= tasa.CuotasHasta) {
                        prestamo.InteresTotal = prestamo.Monto * (tasa.TasaMensual);
                    }
                }
            }
        }



        // 1. Descontar una cuota a cuotasRestantes del prestamo en cuestion.
        // 2. Si solo le queda la ultima cuota, la resto y paso el prestamo a estado Finalizado (5).
        // 3. Si queda mas de 1, simplemente la resto y lo dejo en estado En Curso
        public void descontarCuotaRestante(Prestamo prestamo) {
            if (prestamo.CuotasRestantes == 1) {
                prestamo.CuotasRestantes--;
                finalizar(prestamo);

            } else if (prestamo.CuotasRestantes > 1) {
                prestamo.CuotasRestantes--;
                prestamoDatos.cambiarEstado(prestamo);
            }


        }


        private void finalizar(Prestamo prestamo) {

            // Registro cambio de estado a finalizado
            EstadoPrestamo estadoFinalizado = new EstadoPrestamo();
            estadoFinalizado.IdEstadoPrestamo = 5; // Finalizado
            prestamo.EstadoPrestamo = estadoFinalizado;
            prestamoDatos.cambiarEstado(prestamo);

            // Registro cambio de estado en el historial
            HistorialEstadoPrestamo historialFinalizado = new HistorialEstadoPrestamo();
            historialFinalizado.Prestamo = prestamo;
            historialFinalizado.EstadoPrestamo = estadoFinalizado;
            historialFinalizado.Observaciones = "Cambio automatico a finalizado por haberse abonado todas las cuotas";

            HistorialEstadoPrestamoDatos historialDatos = new HistorialEstadoPrestamoDatos();
            historialDatos.agregar(historialFinalizado);
        }



        public void cancelar(Prestamo prestamo, Usuario usuario, string observacion) {
            // Registro cambio de estado a cancelado
            EstadoPrestamo estadoCancelado = new EstadoPrestamo();
            estadoCancelado.IdEstadoPrestamo = 6; // Cancelado
            prestamo.EstadoPrestamo = estadoCancelado;
            prestamoDatos.cambiarEstado(prestamo);

            // Registro cambio de estado en el historial
            HistorialEstadoPrestamo historialCancelado = new HistorialEstadoPrestamo();
            historialCancelado.Prestamo = prestamo;
            historialCancelado.EstadoPrestamo = estadoCancelado;
            historialCancelado.Usuario = usuario;
            historialCancelado.Observaciones = observacion;

            HistorialEstadoPrestamoDatos historialDatos = new HistorialEstadoPrestamoDatos();
            historialDatos.agregar(historialCancelado);
        }

    }
}
