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

        // Registra un nuevo usuario y devuelve el id generado.
        public int Agregar(Usuario usuario)
        {
            string sql = @"INSERT INTO Usuario (idRol, username, password, activo)
                           VALUES (@idRol, @username, @password, @activo)";

            SqlParameter[] parametros = {
                new SqlParameter("@idRol", usuario.Rol.IdRol),
                new SqlParameter("@username", usuario.Username),
                new SqlParameter("@password", usuario.Password),
                new SqlParameter("@activo", usuario.Activo)
            };

            return AccesoDatos.EjecutarComandoConId(sql, parametros);
        }

        // Actualiza los datos de un usuario existente. Devuelve true si se modifico al menos un registro.
        public bool Modificar(Usuario usuario)
        {
            string sql = @"UPDATE Usuario
                           SET idRol = @idRol, username = @username, password = @password,
                               activo = @activo
                           WHERE idUsuario = @idUsuario";

            SqlParameter[] parametros = {
                new SqlParameter("@idRol", usuario.Rol.IdRol),
                new SqlParameter("@username", usuario.Username),
                new SqlParameter("@password", usuario.Password),
                new SqlParameter("@activo", usuario.Activo),
                new SqlParameter("@idUsuario", usuario.IdUsuario)
            };

            return AccesoDatos.EjecutarComando(sql, parametros) > 0;
        }

        // public bool Eliminar(int idUsuario) { ... }

        // Busca y retorna un usuario por su nombre de usuario con su rol incluido. Si no existe, retorna null.
        public Usuario BuscarPorUsername(string username)
        {
            string sql = @"SELECT u.idUsuario, u.username, u.password, u.activo, u.idRol,
                                  r.descripcion AS descripcionRol
                           FROM Usuario u
                           INNER JOIN Rol r ON u.idRol = r.idRol
                           WHERE u.username = @username";

            SqlParameter[] parametros = { new SqlParameter("@username", username) };
            DataTable tabla = AccesoDatos.EjecutarConsulta(sql, parametros);

            if (tabla.Rows.Count == 0) return null;
            return MapearFila(tabla.Rows[0]);
        }

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



        // Activo un usuario por su Id
        public bool activar(int idUsuario) {
            string sql = @"UPDATE Usuario SET activo = 1 WHERE idUsuario = @idUsuario";

            SqlParameter[] parametros = {
                new SqlParameter("@idUsuario", idUsuario)
            };

            return AccesoDatos.EjecutarComando(sql, parametros) > 0;
        }


        // Desactivo un usuario por su Id
        public bool desactivar(int idUsuario) {
            string sql = @"UPDATE Usuario SET activo = 0 WHERE idUsuario = @idUsuario";

            SqlParameter[] parametros = {
                new SqlParameter("@idUsuario", idUsuario)
            };

            return AccesoDatos.EjecutarComando(sql, parametros) > 0;
        }
    }
}
