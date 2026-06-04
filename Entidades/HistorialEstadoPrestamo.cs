using System;

namespace Entidades
{
    public class HistorialEstadoPrestamo
    {
        public int IdHistorial { get; set; }
        public Prestamo Prestamo { get; set; }
        public EstadoPrestamo EstadoPrestamo { get; set; }
        public DateTime FechaCambio { get; set; }
        public Usuario Usuario { get; set; }       // nullable en BD: puede ser cambio automático
        public string Observaciones { get; set; }
    }
}
