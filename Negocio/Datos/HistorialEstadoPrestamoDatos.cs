using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Negocio.Datos
{
    // Clase de acceso a datos para la entidad HistorialEstadoPrestamo.
    // Registra cada cambio de estado que sufre un prestamo a lo largo del tiempo.
    // Por ahora solo tiene lectura. Los metodos de escritura se implementan en la Etapa 3 (core).
    public class HistorialEstadoPrestamoDatos
    {
        // Retorna todo el historial de cambios de estado de todos los prestamos, ordenado por fecha descendente.
        public List<HistorialEstadoPrestamo> Listar()
        {
            List<HistorialEstadoPrestamo> lista = new List<HistorialEstadoPrestamo>();

            string sql = @"SELECT h.idHistorial, h.fechaCambio, h.observaciones,
                                  h.idPrestamo,
                                  ep.idEstadoPrestamo, ep.descripcion AS descripcionEstado,
                                  h.idUsuario, u.username AS usernameUsuario
                           FROM HistorialEstadoPrestamo h
                           INNER JOIN EstadoPrestamo ep ON h.idEstadoPrestamo = ep.idEstadoPrestamo
                           LEFT  JOIN Usuario         u  ON h.idUsuario       = u.idUsuario
                           ORDER BY h.fechaCambio DESC";

            DataTable tabla = AccesoDatos.EjecutarConsulta(sql);

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(MapearFila(fila));
            }

            return lista;
        }

        // Retorna el historial de cambios de estado de un prestamo especifico, ordenado por fecha descendente.
        // Usado para mostrar la linea de tiempo de un prestamo.
        public List<HistorialEstadoPrestamo> ListarPorPrestamo(int idPrestamo)
        {
            List<HistorialEstadoPrestamo> lista = new List<HistorialEstadoPrestamo>();

            string sql = @"SELECT h.idHistorial, h.fechaCambio, h.observaciones,
                                  h.idPrestamo,
                                  ep.idEstadoPrestamo, ep.descripcion AS descripcionEstado,
                                  h.idUsuario, u.username AS usernameUsuario
                           FROM HistorialEstadoPrestamo h
                           INNER JOIN EstadoPrestamo ep ON h.idEstadoPrestamo = ep.idEstadoPrestamo
                           LEFT  JOIN Usuario         u  ON h.idUsuario       = u.idUsuario
                           WHERE h.idPrestamo = @idPrestamo
                           ORDER BY h.fechaCambio DESC";

            SqlParameter[] parametros = { new SqlParameter("@idPrestamo", idPrestamo) };
            DataTable tabla = AccesoDatos.EjecutarConsulta(sql, parametros);

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(MapearFila(fila));
            }

            return lista;
        }

        // public int Agregar(HistorialEstadoPrestamo historial) { ... }

        // Convierte una fila del DataTable en un objeto HistorialEstadoPrestamo.
        // El usuario puede ser null si el cambio fue generado automaticamente por el sistema.
        // Del prestamo solo se carga el ID para evitar consultas pesadas.
        private HistorialEstadoPrestamo MapearFila(DataRow fila)
        {
            Usuario usuario = null;
            if (fila["idUsuario"] != DBNull.Value)
            {
                usuario = new Usuario
                {
                    IdUsuario = (int)fila["idUsuario"],
                    Username  = fila["usernameUsuario"].ToString()
                };
            }

            return new HistorialEstadoPrestamo
            {
                IdHistorial   = (int)fila["idHistorial"],
                FechaCambio   = (DateTime)fila["fechaCambio"],
                Observaciones = fila["observaciones"] != DBNull.Value
                                    ? fila["observaciones"].ToString() : null,
                Usuario       = usuario,
                Prestamo      = new Prestamo { IdPrestamo = (int)fila["idPrestamo"] },
                EstadoPrestamo = new EstadoPrestamo
                {
                    IdEstadoPrestamo = (int)fila["idEstadoPrestamo"],
                    Descripcion      = fila["descripcionEstado"].ToString()
                }
            };
        }

        // Metodo para agregar un registro de historial de estados de prestamos. Por cada registro agregado devuelvo el ID generado.
        public int agregar(HistorialEstadoPrestamo historialEstadoPrestamo) {
            String sql = @"INSERT INTO HISTORIALESTADOPRESTAMO (idPrestamo, idEstadoPrestamo, fechaCambio, idUsuario, observaciones)
                                    VALUES (@idPrestamo, @idEstadoPrestamo, GETDATE(), @idUsuario, @observaciones);";

            try {
                SqlParameter[] parametros = {
                    new SqlParameter("@idPrestamo", historialEstadoPrestamo.Prestamo.IdPrestamo),
                    new SqlParameter("@idEstadoPrestamo", historialEstadoPrestamo.EstadoPrestamo.IdEstadoPrestamo),
                    new SqlParameter("@idUsuario", (historialEstadoPrestamo.Usuario != null) ? (Object)historialEstadoPrestamo.Usuario.IdUsuario : DBNull.Value),
                    new SqlParameter("@observaciones", (historialEstadoPrestamo.Observaciones != null) ? (Object)historialEstadoPrestamo.Observaciones : DBNull.Value)
                };

                return AccesoDatos.EjecutarComandoConId(sql, parametros);


            } catch (Exception ex) {

                throw ex;
            }
        }
    }
}
