using System;

namespace Entidades
{
    public class TasaInteres
    {
        public int IdTasaInteres { get; set; }
        public ProductoPrestamo ProductoPrestamo { get; set; }
        public int CuotasDesde { get; set; }
        public int CuotasHasta { get; set; }
        public decimal TasaMensual { get; set; }
    }
}
