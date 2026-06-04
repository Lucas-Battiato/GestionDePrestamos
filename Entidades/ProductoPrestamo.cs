using System;

namespace Entidades
{
    public class ProductoPrestamo
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal MontoMinimo { get; set; }
        public decimal MontoMaximo { get; set; }
        public int CuotasMinimas { get; set; }
        public int CuotasMaximas { get; set; }
    }
}
