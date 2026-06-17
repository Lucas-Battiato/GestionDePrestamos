using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Negocio.Datos
{
    // Clase de acceso a datos para la entidad ProductoPrestamo.
    // Por ahora solo tiene lectura. Los metodos de escritura se implementan en la Etapa 2.
    public class ProductoPrestamoDatos
    {
        // Retorna todos los productos de prestamo disponibles, ordenados por nombre.
        public List<ProductoPrestamo> Listar()
        {
            List<ProductoPrestamo> lista = new List<ProductoPrestamo>();

            string sql = @"SELECT idProducto, nombre, descripcion,
                                  montoMinimo, montoMaximo, cuotasMinimas, cuotasMaximas
                           FROM ProductoPrestamo";

            DataTable tabla = AccesoDatos.EjecutarConsulta(sql);

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(MapearFila(fila));
            }

            return lista;
        }

        // Busca y retorna un producto por su ID. Si no existe, retorna null.
        public ProductoPrestamo ObtenerPorId(int idProducto)
        {
            string sql = @"SELECT idProducto, nombre, descripcion,
                                  montoMinimo, montoMaximo, cuotasMinimas, cuotasMaximas
                           FROM ProductoPrestamo
                           WHERE idProducto = @id";

            SqlParameter[] parametros = { new SqlParameter("@id", idProducto) };
            DataTable tabla = AccesoDatos.EjecutarConsulta(sql, parametros);

            if (tabla.Rows.Count == 0) return null;
            return MapearFila(tabla.Rows[0]);
        }

        // public int Agregar(ProductoPrestamo producto) { ... }
        // public bool Modificar(ProductoPrestamo producto) { ... }
        // public bool Eliminar(int idProducto) { ... }
        // public List<ProductoPrestamo> Filtrar(string nombre) { ... }

        // Convierte una fila del DataTable en un objeto ProductoPrestamo.
        private ProductoPrestamo MapearFila(DataRow fila)
        {
            return new ProductoPrestamo
            {
                IdProducto    = (int)fila["idProducto"],
                Nombre        = fila["nombre"].ToString(),
                Descripcion   = fila["descripcion"].ToString(),
                MontoMinimo   = (decimal)fila["montoMinimo"],
                MontoMaximo   = (decimal)fila["montoMaximo"],
                CuotasMinimas = (int)fila["cuotasMinimas"],
                CuotasMaximas = (int)fila["cuotasMaximas"]
            };
        }
    }
}
