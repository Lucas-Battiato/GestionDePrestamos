using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Negocio.Datos
{
    // Clase de acceso a datos para la entidad EstadoPrestamo.
    // Por ahora solo tiene lectura. Los metodos de escritura se implementan en la Etapa 2.
    public class EstadoPrestamoDatos
    {
        // Retorna todos los estados de prestamo disponibles, ordenados por descripcion.
        public List<EstadoPrestamo> Listar()
        {
            List<EstadoPrestamo> lista = new List<EstadoPrestamo>();

            string sql = "SELECT idEstadoPrestamo, descripcion FROM EstadoPrestamo";
            DataTable tabla = AccesoDatos.EjecutarConsulta(sql);

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(MapearFila(fila));
            }

            return lista;
        }

        // Busca y retorna un estado de prestamo por su ID. Si no existe, retorna null.
        public EstadoPrestamo ObtenerPorId(int idEstadoPrestamo)
        {
            string sql = "SELECT idEstadoPrestamo, descripcion FROM EstadoPrestamo WHERE idEstadoPrestamo = @id";
            SqlParameter[] parametros = { new SqlParameter("@id", idEstadoPrestamo) };

            DataTable tabla = AccesoDatos.EjecutarConsulta(sql, parametros);
            if (tabla.Rows.Count == 0) return null;

            return MapearFila(tabla.Rows[0]);
        }

        // public int Agregar(EstadoPrestamo estado) { ... }
        // public bool Modificar(EstadoPrestamo estado) { ... }
        // public bool Eliminar(int idEstadoPrestamo) { ... }

        // Convierte una fila del DataTable en un objeto EstadoPrestamo.
        private EstadoPrestamo MapearFila(DataRow fila)
        {
            return new EstadoPrestamo
            {
                IdEstadoPrestamo = (int)fila["idEstadoPrestamo"],
                Descripcion      = fila["descripcion"].ToString()
            };
        }
    }
}
