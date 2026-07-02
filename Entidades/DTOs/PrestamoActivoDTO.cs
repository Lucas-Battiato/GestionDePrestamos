using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTOs {
    public class PrestamoActivoDTO {
        public int IdPrestamo { get; set; }
        public string UsernameCliente { get; set; }
        public string NombreProducto { get; set; }
        public decimal Monto { get; set; }
        public decimal InteresTotal { get; set; }
        public int CuotasTotales { get; set; }
        public int CuotasRestantes { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string AprobadoPor { get; set; }
    }
}
