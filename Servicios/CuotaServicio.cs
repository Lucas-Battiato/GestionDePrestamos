using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Servicios {
    internal class CuotaServicio {

        //Metodo para calcular el monto final que tendra cada cuota a partir de los datos del prestamo solicitado.
        public Cuota calcularCuota(Prestamo prestamo) {
            Cuota cuota = new Cuota();

            decimal totalAPagar = prestamo.Monto + prestamo.InteresTotal;
            cuota.Monto = Math.Floor((totalAPagar / prestamo.CantidadCuotas) * 100) / 100; // Asumo como perdida los centavos que pueda dar de diferencia cuando la division no sea exacta

            return cuota;
        }
    }
}
