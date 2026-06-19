using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Negocio.Datos
{
    // Clase de acceso a datos para la entidad Cliente.
    // Por ahora solo tiene lectura. Los metodos de escritura se implementan en la Etapa 2.
    public class ClienteDatos
    {
        // Retorna todos los clientes registrados, ordenados por nombre de usuario.
        public List<Cliente> Listar()
        {
            List<Cliente> lista = new List<Cliente>();

            string sql = @"SELECT idCliente, username, password, email, telefono, direccion
                           FROM Cliente
                           ORDER BY username";

            DataTable tabla = AccesoDatos.EjecutarConsulta(sql);

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(MapearFila(fila));
            }

            return lista;
        }

        // Busca y retorna un cliente por su ID. Si no existe, retorna null.
        public Cliente ObtenerPorId(int idCliente)
        {
            string sql = @"SELECT idCliente, username, password, email, telefono, direccion
                           FROM Cliente
                           WHERE idCliente = @idCliente";

            SqlParameter[] parametros = { new SqlParameter("@idCliente", idCliente) };
            DataTable tabla = AccesoDatos.EjecutarConsulta(sql, parametros);

            if (tabla.Rows.Count == 0) return null;
            return MapearFila(tabla.Rows[0]);
        }


        // Busca y retorna un cliente por su nombre de usuario. Si no existe, retorna null.
        public Cliente ObtenerPorUsername(string username) {
            string sql = @"SELECT idCliente, username, password, email, telefono, direccion
                           FROM Cliente
                           WHERE username = @username";

            SqlParameter[] parametros = { new SqlParameter("@username", username) };
            DataTable tabla = AccesoDatos.EjecutarConsulta(sql, parametros);

            if (tabla.Rows.Count == 0) return null;
            return MapearFila(tabla.Rows[0]);
        }


        // public int Agregar(Cliente cliente) { ... }
        // public bool Modificar(Cliente cliente) { ... }
        // public bool Eliminar(int idCliente) { ... }
        // public Cliente BuscarPorUsername(string username) { ... }
        // public List<Cliente> Filtrar(string termino) { ... }

        // Convierte una fila del DataTable en un objeto Cliente.
        private Cliente MapearFila(DataRow fila)
        {
            return new Cliente
            {
                IdCliente = (int)fila["idCliente"],
                Username  = fila["username"].ToString(),
                Password  = fila["password"].ToString(),
                Email     = fila["email"].ToString(),
                Telefono  = fila["telefono"].ToString(),
                Direccion = fila["direccion"].ToString()
            };
        }
    }
}
