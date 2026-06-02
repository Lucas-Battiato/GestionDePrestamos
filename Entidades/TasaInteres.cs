using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades {
    internal class TasaInteres {
        public int IdTasaInteres { get; set; }
        public ProductoPrestamo ProductoPrestamo { get; set; }
        public int CuotasDesde { get; set; }
        public int CuotasHasta { get; set; }
        public double TasaMensual { get; set; }
    }
}
