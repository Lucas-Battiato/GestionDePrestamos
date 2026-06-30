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


        // Pagar cuota y descontar cuota restante por medio de PrestamoServicio
        public void pagarCuota(Cuota cuota, MetodoPago metodoPago) {
            cuotaDatos.registrarPago(cuota, metodoPago);

            
            PrestamoDatos prestamoDatos = new PrestamoDatos();
            Prestamo prestamo = prestamoDatos.ObtenerPorId(cuota.Prestamo.IdPrestamo);

            PrestamoServicio prestamoServicio = new PrestamoServicio();
            prestamoServicio.descontarCuotaRestante(prestamo);

        }
    }
}
