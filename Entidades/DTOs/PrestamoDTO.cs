using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTOs {
    public class PrestamoDTO {
        public int IdPrestamo { get; set; }
        public string NombreProducto { get; set; }
        public string Monto { get; set; }
        public int CuotasRestantes { get; set; }
        public string EstadoDescripcion { get; set; }
        public string EstadoCssClass { get; set; }

    }
}
