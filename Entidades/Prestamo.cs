using System;

namespace Entidades
{
    public class Prestamo
    {
        public int IdPrestamo { get; set; }
        public ProductoPrestamo ProductoPrestamo { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario UsuarioAprobador { get; set; }
        public decimal Monto { get; set; }
        public decimal InteresTotal { get; set; }
        public int CantidadCuotas { get; set; }
        public int CuotasRestantes { get; set; }
        public DateTime? FechaAprobacion { get; set; }   // nullable: puede no estar aprobado aún
        public DateTime FechaUltimaActualizacion { get; set; }
        public EstadoPrestamo EstadoPrestamo { get; set; }
    }
}
