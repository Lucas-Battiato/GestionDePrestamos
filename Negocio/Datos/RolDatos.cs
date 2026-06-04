using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Negocio.Datos
{
    // Clase de acceso a datos para la entidad Rol.
    // Por ahora solo tiene lectura. Los metodos de escritura se implementan en la Etapa 2.
    public class RolDatos
    {
        // Retorna todos los roles cargados en la base de datos, ordenados por descripcion.
        public List<Rol> Listar()
        {
            List<Rol> lista = new List<Rol>();

            string sql = "SELECT idRol, descripcion FROM Rol ORDER BY descripcion";
            DataTable tabla = AccesoDatos.EjecutarConsulta(sql);

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(MapearFila(fila));
            }

            return lista;
        }

        // Busca y retorna un rol por su ID. Si no existe, retorna null.
        public Rol ObtenerPorId(int idRol)
        {
            string sql = "SELECT idRol, descripcion FROM Rol WHERE idRol = @idRol";
            SqlParameter[] parametros = { new SqlParameter("@idRol", idRol) };

            DataTable tabla = AccesoDatos.EjecutarConsulta(sql, parametros);
            if (tabla.Rows.Count == 0) return null;

            return MapearFila(tabla.Rows[0]);
        }

        // public int Agregar(Rol rol) { ... }
        // public bool Modificar(Rol rol) { ... }
        // public bool Eliminar(int idRol) { ... }

        // Convierte una fila del DataTable en un objeto Rol.
        private Rol MapearFila(DataRow fila)
        {
            return new Rol
            {
                IdRol       = (int)fila["idRol"],
                Descripcion = fila["descripcion"].ToString()
            };
        }
    }
}
