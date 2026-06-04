using System;

namespace Entidades
{
    public class Cuota
    {
        public int IdCuota { get; set; }
        public Prestamo Prestamo { get; set; }
        public EstadoCuota EstadoCuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime? FechaPago { get; set; }   // nullable: puede no estar pagada aún
        public decimal Monto { get; set; }
        public MetodoPago MetodoPago { get; set; }
    }
}
