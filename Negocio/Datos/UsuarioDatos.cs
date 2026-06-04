using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Negocio.Datos
{
    // Clase de acceso a datos para la entidad Usuario.
    // Por ahora solo tiene lectura. Los metodos de escritura se implementan en la Etapa 2.
    public class UsuarioDatos
    {
        // Retorna todos los usuarios con su rol incluido, ordenados por nombre de usuario.
        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();

            string sql = @"SELECT u.idUsuario, u.username, u.password, u.activo, u.idRol,
                                  r.descripcion AS descripcionRol
                           FROM Usuario u
                           INNER JOIN Rol r ON u.idRol = r.idRol
                           ORDER BY u.username";

            DataTable tabla = AccesoDatos.EjecutarConsulta(sql);

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(MapearFila(fila));
            }

            return lista;
        }

        // Busca y retorna un usuario por su ID con su rol incluido. Si no existe, retorna null.
        public Usuario ObtenerPorId(int idUsuario)
        {
            string sql = @"SELECT u.idUsuario, u.username, u.password, u.activo, u.idRol,
                                  r.descripcion AS descripcionRol
                           FROM Usuario u
                           INNER JOIN Rol r ON u.idRol = r.idRol
                           WHERE u.idUsuario = @idUsuario";

            SqlParameter[] parametros = { new SqlParameter("@idUsuario", idUsuario) };
            DataTable tabla = AccesoDatos.EjecutarConsulta(sql, parametros);

            if (tabla.Rows.Count == 0) return null;
            return MapearFila(tabla.Rows[0]);
        }

        // public int Agregar(Usuario usuario) { ... }
        // public bool Modificar(Usuario usuario) { ... }
        // public bool Eliminar(int idUsuario) { ... }
        // public Usuario BuscarPorUsername(string username) { ... }

        // Convierte una fila del DataTable en un objeto Usuario con su Rol anidado.
        private Usuario MapearFila(DataRow fila)
        {
            return new Usuario
            {
                IdUsuario = (int)fila["idUsuario"],
                Username  = fila["username"].ToString(),
                Password  = fila["password"].ToString(),
                Activo    = (bool)fila["activo"],
                Rol = new Rol
                {
                    IdRol       = (int)fila["idRol"],
                    Descripcion = fila["descripcionRol"].ToString()
                }
            };
        }
    }
}
