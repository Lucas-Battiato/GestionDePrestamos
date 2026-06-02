using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades {
    internal class HistorialEstadoPrestamo {
        public int IdHistorial { get; set; }
        public Prestamo Prestamo { get; set; }
        public EstadoPrestamo EstadoPrestamo { get; set; }
        public DateTime FechaCambio { get; set; }
        public Usuario Usuario { get; set; }
        public string Observaciones { get; set; }
    }
}
