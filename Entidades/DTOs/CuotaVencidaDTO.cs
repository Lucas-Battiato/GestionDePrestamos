using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTOs {
    public class CuotaVencidaDTO {
        public int IdCuota { get; set; }
        public int IdPrestamo { get; set; }
        public string UsernameCliente { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int DiasVencida { get; set; }
    }
}
