using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

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

            string sql = "SELECT idMetodoPago, descripcion FROM MetodoPago;";
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

        public int guardar(MetodoPago metodoPago) {
            String sql = @"INSERT INTO MetodoPago (descripcion) VALUES (@descripcion);";

            try {
                SqlParameter[] parametros = {
                    new SqlParameter("@descripcion", metodoPago.Descripcion)
                };

                return AccesoDatos.EjecutarComandoConId(sql, parametros);


            } catch (Exception ex) {

                throw ex;
            }
        }

        public bool eliminar(MetodoPago metodoPago) {
            String sql = @"DELETE FROM MetodoPago WHERE idMetodoPago = @idMetodoPago";

            try {
                SqlParameter[] parametros = {
                    new SqlParameter("@idMetodoPago", metodoPago.IdMetodoPago)
                };

                if (AccesoDatos.EjecutarComando(sql, parametros) > 0) {
                    return true;
                }

                return false;

            } catch (Exception ex) {

                throw ex;
            }

        }

        public bool modificar(MetodoPago metodoPago) {
            String sql = @"UPDATE MetodoPago SET Descripcion=@descripcion WHERE idMetodoPago = @idMetodoPago";

            try {
                SqlParameter[] parametros = {
                    new SqlParameter("@idMetodoPago", metodoPago.IdMetodoPago),
                    new SqlParameter("@descripcion", metodoPago.Descripcion)
                };

                if (AccesoDatos.EjecutarComando(sql, parametros) > 0) {
                    return true;
                }

                return false;

            } catch (Exception ex) {

                throw ex;
            }
        }
    }
}
