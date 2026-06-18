using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Negocio.Datos
{
    // Clase de acceso a datos para la entidad EstadoCuota.
    // Por ahora solo tiene lectura. Los metodos de escritura se implementan en la Etapa 2.
    public class EstadoCuotaDatos
    {
        // Retorna todos los estados de cuota disponibles, ordenados por descripcion.
        public List<EstadoCuota> Listar()
        {
            List<EstadoCuota> lista = new List<EstadoCuota>();

            string sql = "SELECT idEstadoCuota, descripcion FROM EstadoCuota";
            DataTable tabla = AccesoDatos.EjecutarConsulta(sql);

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(MapearFila(fila));
            }

            return lista;
        }

        // Busca y retorna un estado de cuota por su ID. Si no existe, retorna null.
        public EstadoCuota ObtenerPorId(int idEstadoCuota)
        {
            string sql = "SELECT idEstadoCuota, descripcion FROM EstadoCuota WHERE idEstadoCuota = @id";
            SqlParameter[] parametros = { new SqlParameter("@id", idEstadoCuota) };

            DataTable tabla = AccesoDatos.EjecutarConsulta(sql, parametros);
            if (tabla.Rows.Count == 0) return null;

            return MapearFila(tabla.Rows[0]);
        }

        // public int Agregar(EstadoCuota estado) { ... }
        // public bool Modificar(EstadoCuota estado) { ... }
        // public bool Eliminar(int idEstadoCuota) { ... }

        // Convierte una fila del DataTable en un objeto EstadoCuota.
        private EstadoCuota MapearFila(DataRow fila)
        {
            return new EstadoCuota
            {
                IdEstadoCuota = (int)fila["idEstadoCuota"],
                Descripcion   = fila["descripcion"].ToString()
            };
        }
    }
}
