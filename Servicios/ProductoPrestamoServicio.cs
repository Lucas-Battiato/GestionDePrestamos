using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Negocio.Datos;

namespace Servicios {
    internal class ProductoPrestamoServicio {

        // Metodo para validar el monto solicitado por el usuario.
        public bool validarMonto(decimal montoSolicitado, ProductoPrestamo producto) {
            if (montoSolicitado >= producto.MontoMinimo && montoSolicitado <= producto.MontoMaximo)
                return true;
            else
                return false;
        }
    }
}
