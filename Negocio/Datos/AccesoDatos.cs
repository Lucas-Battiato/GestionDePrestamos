using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Negocio.Datos
{
    // Clase de ayuda para centralizar el acceso a la base de datos.
    // Todas las clases de datos del sistema usan estos metodos para conectarse y ejecutar consultas.
    // La cadena de conexion se lee desde el Web.config con la clave "GestionPrestamosDB".
    internal static class AccesoDatos
    {
        // Abre y devuelve una conexion a la base de datos lista para usar.
        // El codigo que la llama es responsable de cerrarla (usar dentro de un bloque using).
        public static SqlConnection ObtenerConexion()
        {
            string cadena = ConfigurationManager.ConnectionStrings["GestionPrestamosDB"].ConnectionString;
            SqlConnection conexion = new SqlConnection(cadena);
            conexion.Open();
            return conexion;
        }

        // Ejecuta una consulta SELECT sin parametros y devuelve los resultados en un DataTable.
        public static DataTable EjecutarConsulta(string sql)
        {
            DataTable tabla = new DataTable();
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion))
            {
                adaptador.Fill(tabla);
            }
            return tabla;
        }

        // Ejecuta una consulta SELECT con parametros y devuelve los resultados en un DataTable.
        // Usar siempre este metodo cuando la consulta incluya valores ingresados por el usuario,
        // para evitar inyeccion SQL.
        public static DataTable EjecutarConsulta(string sql, SqlParameter[] parametros)
        {
            DataTable tabla = new DataTable();
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddRange(parametros);
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    adaptador.Fill(tabla);
                }
            }
            return tabla;
        }

        // Ejecuta un INSERT, UPDATE o DELETE sin parametros y devuelve cuantas filas fueron afectadas.
        public static int EjecutarComando(string sql) {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(sql, conexion)) {
                return comando.ExecuteNonQuery();
            }
        }

        // Ejecuta un INSERT, UPDATE o DELETE con parametros y devuelve cuantas filas fueron afectadas.
        public static int EjecutarComando(string sql, SqlParameter[] parametros)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddRange(parametros);
                return comando.ExecuteNonQuery();
            }
        }

        // Ejecuta un INSERT y devuelve el ID generado automaticamente por la base de datos (IDENTITY).
        // Util para obtener el ID del registro recien creado sin hacer una consulta extra.
        public static int EjecutarComandoConId(string sql, SqlParameter[] parametros)
        {
            using (SqlConnection conexion = ObtenerConexion())
            using (SqlCommand comando = new SqlCommand(sql + "; SELECT SCOPE_IDENTITY();", conexion))
            {
                comando.Parameters.AddRange(parametros);
                object resultado = comando.ExecuteScalar();
                return resultado != null ? (int)(decimal)resultado : 0;
            }
        }
    }
}
