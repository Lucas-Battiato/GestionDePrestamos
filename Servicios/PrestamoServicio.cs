using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Negocio.Datos;

namespace Servicios {
    public class PrestamoServicio {
        PrestamoDatos prestamoDatos = new PrestamoDatos();

        // Metodo para validar y agregar una nueva solicitud de prestamo (no aprueba, solo lo registra en la DB)
        public int generar(Prestamo prestamo) {
            //Agregar validaciones necesarias
            // Agregar registro en historial de cambiuos en eestado de prestamo.
            return prestamoDatos.agregar(prestamo);
        }
        

        // Metodo para aprobar un prestamo.
        // Registra el cambio en el historial, genera todas las cuotas, envia mail de confirmación, y devuelve true o false dependiendo de si se aprobo correctamente o no.
        public void aprobar(Prestamo prestamo, Usuario usuario, string observacion) {
            /*
                - Recibo el prestamo
                - Registro cambio de estado prestamo en historial (aprobado)
                - Registro cambio de estado prestamo en historial (En curso)
                - Genero cuotas
                Envio mail de confirmación con plan de cuotas
            */

            prestamo.UsuarioAprobador = usuario;
            prestamo.FechaAprobacion = DateTime.Now;
            prestamo.CuotasRestantes = prestamo.CantidadCuotas;


            // Registro cambio de estado a aprobado
            EstadoPrestamo estadoAprobado = new EstadoPrestamo();
            estadoAprobado.IdEstadoPrestamo = 2;
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
            estadoEnCurso.IdEstadoPrestamo = 4;
            prestamo.EstadoPrestamo = estadoEnCurso;
            prestamoDatos.cambiarEstado(prestamo);

            HistorialEstadoPrestamo historialEnCurso = new HistorialEstadoPrestamo();
            historialEnCurso.Prestamo = prestamo;
            historialEnCurso.EstadoPrestamo = estadoEnCurso;
            historialEnCurso.Usuario = usuario;
            historialEnCurso.Observaciones = "Cambio automatico a En Curso";
            historialDatos.agregar(historialEnCurso);


            // Calculo monto de las cuotas
            decimal totalAPagar = prestamo.Monto + prestamo.InteresTotal;
            decimal montoCuota = Math.Floor((totalAPagar / prestamo.CantidadCuotas) * 100) / 100; // Asumo como perdida los centavos que pueda dar de diferencia cuando la division no sea exacta

            // Tomo como fecha de vencimiento de la primer cuota el 10 del mes siguiente al siguiente de la aprobacion.
            DateTime fechaBase = new DateTime(
                prestamo.FechaAprobacion.Value.Year,
                prestamo.FechaAprobacion.Value.Month,
                10
            ).AddMonths(2);

            //Genero todas las cuotas. Para la fecha de vencimiento contemplo que no caiga ni sabado ni domingo (las corro hacia adelante)
            CuotaDatos cuotaDatos = new CuotaDatos();

            for (int i = 0; i < prestamo.CantidadCuotas; i++) {
                Cuota cuota = new Cuota();
                cuota.Prestamo = prestamo;
                cuota.Monto = montoCuota;

                DateTime fechaVencimiento = fechaBase.AddMonths(i);
                if (fechaVencimiento.DayOfWeek == DayOfWeek.Saturday)
                    fechaVencimiento = fechaVencimiento.AddDays(2); //Agrego 2 dias para que sea el lunes sigueinte
                else if (fechaVencimiento.DayOfWeek == DayOfWeek.Sunday)
                    fechaVencimiento = fechaVencimiento.AddDays(1); // Agrego 1 dia para que sea el lunes siguiente.

                cuota.FechaVencimiento = fechaVencimiento;

                cuotaDatos.agregar(cuota);
            }

            // ¡¡AGREGAR LOGICA PARA ENVIO DE MAILS. RE-VER GRABACION DEL AULA VIRTUAL!!
        }


        // Metodo para rechazar un prestamo. Registra cambios en el historial, envia mail de rechazo, y devuelve true o false dependiendo de si se pudieron completar todos los pasos correctamente o no.
        public void rechazar(Prestamo prestamo) {
            /*
                Recibo el prestamo
                Registro cambio de estado prestamo en historial (rechazado)
                Envio mail de rechazo
            */
        }
    }
}
