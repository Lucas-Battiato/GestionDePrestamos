using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTOs {
    public class CuotaDTO {
        public int IdCuota { get; set; }
        public int NumeroCuota { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string EstadoDescripcion { get; set; }
        public string EstadoCssClass { get; set; }
        public bool PuedeRegistrarPago { get; set; }
        public DateTime? FechaPago { get; set; }
        public string MetodoPago { get; set; }
    }
}
