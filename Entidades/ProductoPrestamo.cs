using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades {
    internal class ProductoPrestamo {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double MontoMinimo { get; set; }
        public double MontoMaximo { get; set; }
        public int CuotasMinimas { get; set; }
        public int CuotasMaximas { get; set; }
    }
}
