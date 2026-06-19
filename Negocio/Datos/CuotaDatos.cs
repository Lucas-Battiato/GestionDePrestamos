using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Negocio.Datos
{
    // Clase de acceso a datos para la entidad Cuota.
    // Por ahora solo tiene lectura. Los metodos de escritura se implementan en la Etapa 3 (core).
    public class CuotaDatos
    {
        // Retorna todas las cuotas con su estado y metodo de pago incluidos.
        // Del prestamo solo se carga el ID para evitar consultas muy pesadas.
        // Si se necesita el prestamo completo, usar PrestamoDatos.ObtenerPorId().
        public List<Cuota> Listar()
        {
            List<Cuota> lista = new List<Cuota>();

            string sql = @"SELECT cu.idCuota, cu.fechaVencimiento, cu.fechaPago, cu.monto,
                                  ec.idEstadoCuota, ec.descripcion AS descripcionEstadoCuota,
                                  cu.idMetodoPago, mp.descripcion AS descripcionMetodoPago,
                                  cu.idPrestamo
                           FROM Cuota cu
                           INNER JOIN EstadoCuota ec ON cu.idEstadoCuota = ec.idEstadoCuota
                           LEFT  JOIN MetodoPago  mp ON cu.idMetodoPago  = mp.idMetodoPago
                           ORDER BY cu.fechaVencimiento";

            DataTable tabla = AccesoDatos.EjecutarConsulta(sql);

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(MapearFila(fila));
            }

            return lista;
        }

        // Retorna todas las cuotas de un prestamo especifico, ordenadas por fecha de vencimiento.
        // Usado para mostrar el plan de cuotas de un prestamo.
        public List<Cuota> ListarPorPrestamo(int idPrestamo)
        {
            List<Cuota> lista = new List<Cuota>();

            string sql = @"SELECT cu.idCuota, cu.fechaVencimiento, cu.fechaPago, cu.monto,
                                  ec.idEstadoCuota, ec.descripcion AS descripcionEstadoCuota,
                                  cu.idMetodoPago, mp.descripcion AS descripcionMetodoPago,
                                  cu.idPrestamo
                           FROM Cuota cu
                           INNER JOIN EstadoCuota ec ON cu.idEstadoCuota = ec.idEstadoCuota
                           LEFT  JOIN MetodoPago  mp ON cu.idMetodoPago  = mp.idMetodoPago
                           WHERE cu.idPrestamo = @idPrestamo
                           ORDER BY cu.fechaVencimiento";

            SqlParameter[] parametros = { new SqlParameter("@idPrestamo", idPrestamo) };
            DataTable tabla = AccesoDatos.EjecutarConsulta(sql, parametros);

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(MapearFila(fila));
            }

            return lista;
        }

        // Busca y retorna una cuota por su ID. Si no existe, retorna null.
        public Cuota ObtenerPorId(int idCuota)
        {
            string sql = @"SELECT cu.idCuota, cu.fechaVencimiento, cu.fechaPago, cu.monto,
                                  ec.idEstadoCuota, ec.descripcion AS descripcionEstadoCuota,
                                  cu.idMetodoPago, mp.descripcion AS descripcionMetodoPago,
                                  cu.idPrestamo
                           FROM Cuota cu
                           INNER JOIN EstadoCuota ec ON cu.idEstadoCuota = ec.idEstadoCuota
                           LEFT  JOIN MetodoPago  mp ON cu.idMetodoPago  = mp.idMetodoPago
                           WHERE cu.idCuota = @id";

            SqlParameter[] parametros = { new SqlParameter("@id", idCuota) };
            DataTable tabla = AccesoDatos.EjecutarConsulta(sql, parametros);

            if (tabla.Rows.Count == 0) return null;
            return MapearFila(tabla.Rows[0]);
        }

        // public int Agregar(Cuota cuota) { ... }
        // public bool Modificar(Cuota cuota) { ... }
        // public bool RegistrarPago(int idCuota, int idMetodoPago, DateTime fechaPago) { ... }

        // Convierte una fila del DataTable en un objeto Cuota con su estado y metodo de pago anidados.
        // El metodo de pago puede ser null si la cuota todavia no fue pagada.
        private Cuota MapearFila(DataRow fila)
        {
            MetodoPago metodo = null;
            if (fila["idMetodoPago"] != DBNull.Value)
            {
                metodo = new MetodoPago
                {
                    IdMetodoPago = (int)fila["idMetodoPago"],
                    Descripcion  = fila["descripcionMetodoPago"].ToString()
                };
            }

            return new Cuota
            {
                IdCuota          = (int)fila["idCuota"],
                FechaVencimiento = (DateTime)fila["fechaVencimiento"],
                FechaPago        = fila["fechaPago"] != DBNull.Value
                                     ? (DateTime?)fila["fechaPago"] : null,
                Monto            = (decimal)fila["monto"],
                MetodoPago       = metodo,
                EstadoCuota = new EstadoCuota
                {
                    IdEstadoCuota = (int)fila["idEstadoCuota"],
                    Descripcion   = fila["descripcionEstadoCuota"].ToString()
                },
                Prestamo = new Prestamo { IdPrestamo = (int)fila["idPrestamo"] }
            };
        }

        // Metodo para agregar una cuota. Por cada cuota agregada devuelvo el ID generado.
        public int agregar(Cuota cuota) {
            String sql = @"INSERT INTO CUOTA (idPrestamo, idEstadoCuota, fechaVencimiento, fechaPago, monto, idMetodoPago)
                                    VALUES (@idPrestamo, 1, @fechaVencimiento, null, @monto, null);";

            try {
                SqlParameter[] parametros = {
                    new SqlParameter("@idPrestamo", cuota.Prestamo.IdPrestamo),
                    new SqlParameter("@fechaVencimiento", cuota.FechaVencimiento),
                    new SqlParameter("@monto", cuota.Monto)
                };

                return AccesoDatos.EjecutarComandoConId(sql, parametros);


            } catch (Exception ex) {

                throw ex;
            }
        }

        // Registro el pago de una cuota.
        // Recibe la cuota en a ser marcada como pagada y el metodo de pago elegido.
        // Devuelvo true si se registro correctamente el pago, y false si no hubo ningun registro actualizado en la DB.
        public bool registrarPago(Cuota cuota, MetodoPago metodoPago) {
            string sql = @"UPDATE CUOTA
                                SET idEstadoCuota = 2,
                                    fechaPago = GETDATE(),
                                    idMetodoPago = @idMetodoPago
                            WHERE idCuota = @idCuota";

            try {
                SqlParameter[] parametros = {
                    new SqlParameter("@idMetodoPago", metodoPago.IdMetodoPago),
                    new SqlParameter("@idCuota", cuota.IdCuota)
                };

                if (AccesoDatos.EjecutarComando(sql, parametros) != 0) return true;
                    else return false;

            } catch (Exception ex) {

                throw ex;
            }
        }

        // Metodo para registrar como Vencida a todas las cuotas que tengan fecha de vencimiento anterior al dia actual.
        public int marcarVencidas() {
            string sql = @"UPDATE CUOTA
                                SET idEstadoCuota = 3
                            WHERE GETDATE() > fechaVencimiento";

            try {
                return AccesoDatos.EjecutarComando(sql);

            } catch (Exception ex) {

                throw ex;
            }
        }
    }
}
