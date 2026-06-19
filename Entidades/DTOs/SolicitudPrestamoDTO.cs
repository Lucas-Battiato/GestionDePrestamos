using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTOs {
    public class SolicitudPrestamoDTO {
        public int IdPrestamo { get; set; }
        public string UsernameCliente { get; set; }
        public string NombreProducto { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public decimal MontoSolicitado { get; set; }
        public decimal MontoADevolver { get; set; }
        public decimal GananciaEstimada { get; set; }
        public string DetalleCuotas { get; set; }
    }
}
