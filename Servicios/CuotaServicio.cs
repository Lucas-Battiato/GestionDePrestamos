using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Negocio.Datos;

namespace Servicios {
    public class CuotaServicio {
        Cuota cuota = new Cuota();
        CuotaDatos cuotaDatos = new CuotaDatos();

        //Metodo para calcular el monto final que tendra cada cuota a partir de los datos del prestamo solicitado.
        public Cuota calcularCuota(Prestamo prestamo) {

            decimal totalAPagar = prestamo.Monto + prestamo.InteresTotal;
            cuota.Monto = Math.Floor((totalAPagar / prestamo.CantidadCuotas) * 100) / 100; // Asumo como perdida los centavos que pueda dar de diferencia cuando la division no sea exacta

            return cuota;
        }


        public void pagarCuota(Cuota cuota, MetodoPago metodoPago) {
            // Pagar cuota
            cuotaDatos.registrarPago(cuota, metodoPago);

            // Descontar una cuota a cuotasRestantes del prestamo en cuestion.
            // Si solo le queda la ultima cuota, la resto y paso el prestamo a estado Finalizado (5).
            // Si queda mas de 1, simplemente la resto y lo dejo en estado En Curso
            PrestamoDatos prestamoDatos = new PrestamoDatos();
            Prestamo prestamo = prestamoDatos.ObtenerPorId(cuota.Prestamo.IdPrestamo);

            if (prestamo.CuotasRestantes == 1) {
                prestamo.CuotasRestantes--;
                prestamo.EstadoPrestamo = new EstadoPrestamo { IdEstadoPrestamo = 5, Descripcion = "Finalizado" };

            } else if (prestamo.CuotasRestantes > 1) {
                prestamo.CuotasRestantes--;
            }

            prestamoDatos.cambiarEstado(prestamo);

        }
    }
}
