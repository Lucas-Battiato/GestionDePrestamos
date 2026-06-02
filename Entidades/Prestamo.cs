using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades {
    internal class Prestamo {
        public int IdPrestamo { get; set; }
        public ProductoPrestamo ProductoPrestamo { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario UsuarioAprobador { get; set; }
        public double Monto { get; set; }
        public double InteresTotal { get; set; }
        public int CantidadCuotas { get; set; }
        public int CuotasRestantes { get; set; }
        public DateTime FechaAprobacion { get; set; }
        public DateTime FechaUltimaActualizacion { get; set; }
        public EstadoPrestamo EstadoPrestamo { get; set; }
    }
}
