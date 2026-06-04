using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Negocio.Datos
{
    // Clase de acceso a datos para la entidad MetodoPago.
    // Por ahora solo tiene lectura. Los metodos de escritura se implementan en la Etapa 2.
    public class MetodoPagoDatos
    {
        // Retorna todos los metodos de pago disponibles, ordenados por descripcion.
        public List<MetodoPago> Listar()
        {
            List<MetodoPago> lista = new List<MetodoPago>();

            string sql = "SELECT idMetodoPago, descripcion FROM MetodoPago ORDER BY descripcion";
            DataTable tabla = AccesoDatos.EjecutarConsulta(sql);

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(MapearFila(fila));
            }

            return lista;
        }

        // Busca y retorna un metodo de pago por su ID. Si no existe, retorna null.
        public MetodoPago ObtenerPorId(int idMetodoPago)
        {
            string sql = "SELECT idMetodoPago, descripcion FROM MetodoPago WHERE idMetodoPago = @id";
            SqlParameter[] parametros = { new SqlParameter("@id", idMetodoPago) };

            DataTable tabla = AccesoDatos.EjecutarConsulta(sql, parametros);
            if (tabla.Rows.Count == 0) return null;

            return MapearFila(tabla.Rows[0]);
        }

        // public int Agregar(MetodoPago metodo) { ... }
        // public bool Modificar(MetodoPago metodo) { ... }
        // public bool Eliminar(int idMetodoPago) { ... }

        // Convierte una fila del DataTable en un objeto MetodoPago.
        private MetodoPago MapearFila(DataRow fila)
        {
            return new MetodoPago
            {
                IdMetodoPago = (int)fila["idMetodoPago"],
                Descripcion  = fila["descripcion"].ToString()
            };
        }
    }
}
