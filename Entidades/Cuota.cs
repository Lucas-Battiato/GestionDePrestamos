using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades {
    internal class Cuota {
        public int IdCuota { get; set; }
        public Prestamo Prestamo { get; set; }
        public EstadoCuota EstadoCuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime FechaPago { get; set; }
        public double Monto { get; set; }
        public MetodoPago MetodoPago { get; set; }
    }
}
