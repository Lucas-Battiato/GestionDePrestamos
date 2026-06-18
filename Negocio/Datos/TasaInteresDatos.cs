using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Negocio.Datos
{
    // Clase de acceso a datos para la entidad TasaInteres.
    // Por ahora solo tiene lectura. Los metodos de escritura se implementan en la Etapa 2.
    public class TasaInteresDatos
    {
        // Retorna todas las tasas de interes con su producto asociado, ordenadas por producto y tramo de cuotas.
        public List<TasaInteres> Listar()
        {
            List<TasaInteres> lista = new List<TasaInteres>();

            string sql = @"SELECT t.idTasaInteres, t.cuotasDesde, t.cuotasHasta, t.tasaMensual,
                                  t.idProducto, p.nombre AS nombreProducto,
                                  p.descripcion AS descripcionProducto,
                                  p.montoMinimo, p.montoMaximo, p.cuotasMinimas, p.cuotasMaximas
                           FROM TasaInteres t
                           INNER JOIN ProductoPrestamo p ON t.idProducto = p.idProducto
                           ORDER BY p.idProducto, t.cuotasDesde";

            DataTable tabla = AccesoDatos.EjecutarConsulta(sql);

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(MapearFila(fila));
            }

            return lista;
        }

        // Busca y retorna una tasa de interes por su ID con el producto incluido. Si no existe, retorna null.
        public TasaInteres ObtenerPorId(int idTasaInteres)
        {
            string sql = @"SELECT t.idTasaInteres, t.cuotasDesde, t.cuotasHasta, t.tasaMensual,
                                  t.idProducto, p.nombre AS nombreProducto,
                                  p.descripcion AS descripcionProducto,
                                  p.montoMinimo, p.montoMaximo, p.cuotasMinimas, p.cuotasMaximas
                           FROM TasaInteres t
                           INNER JOIN ProductoPrestamo p ON t.idProducto = p.idProducto
                           WHERE t.idTasaInteres = @id";

            SqlParameter[] parametros = { new SqlParameter("@id", idTasaInteres) };
            DataTable tabla = AccesoDatos.EjecutarConsulta(sql, parametros);

            if (tabla.Rows.Count == 0) return null;
            return MapearFila(tabla.Rows[0]);
        }

        // Retorna todas las tasas de interes de un producto especifico, ordenadas por tramo de cuotas.
        // Util para mostrar la tabla de tasas al cliente cuando selecciona un producto.
        public List<TasaInteres> ListarPorProducto(int idProducto)
        {
            List<TasaInteres> lista = new List<TasaInteres>();

            string sql = @"SELECT t.idTasaInteres, t.cuotasDesde, t.cuotasHasta, t.tasaMensual,
                                  t.idProducto, p.nombre AS nombreProducto,
                                  p.descripcion AS descripcionProducto,
                                  p.montoMinimo, p.montoMaximo, p.cuotasMinimas, p.cuotasMaximas
                           FROM TasaInteres t
                           INNER JOIN ProductoPrestamo p ON t.idProducto = p.idProducto
                           WHERE t.idProducto = @idProducto
                           ORDER BY t.cuotasDesde";

            SqlParameter[] parametros = { new SqlParameter("@idProducto", idProducto) };
            DataTable tabla = AccesoDatos.EjecutarConsulta(sql, parametros);

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(MapearFila(fila));
            }

            return lista;
        }

        // public int Agregar(TasaInteres tasa) { ... }
        // public bool Modificar(TasaInteres tasa) { ... }
        // public bool Eliminar(int idTasaInteres) { ... }

        // Convierte una fila del DataTable en un objeto TasaInteres con su ProductoPrestamo anidado.
        private TasaInteres MapearFila(DataRow fila)
        {
            return new TasaInteres
            {
                IdTasaInteres = (int)fila["idTasaInteres"],
                CuotasDesde   = (int)fila["cuotasDesde"],
                CuotasHasta   = (int)fila["cuotasHasta"],
                TasaMensual   = (decimal)fila["tasaMensual"],
                ProductoPrestamo = new ProductoPrestamo
                {
                    IdProducto    = (int)fila["idProducto"],
                    Nombre        = fila["nombreProducto"].ToString(),
                    Descripcion   = fila["descripcionProducto"].ToString(),
                    MontoMinimo   = (decimal)fila["montoMinimo"],
                    MontoMaximo   = (decimal)fila["montoMaximo"],
                    CuotasMinimas = (int)fila["cuotasMinimas"],
                    CuotasMaximas = (int)fila["cuotasMaximas"]
                }
            };
        }


        // Metodo para agregar una tasa de interes a la DB
        public int agregar(TasaInteres tasaInteres) {
            String sql = @"INSERT INTO TASAINTERES (idProducto, cuotasDesde, cuotasHasta, tasaMensual)
                                    VALUES (@idProducto, @cuotasDesde, @cuotasHasta, @tasaMensual);";

            try {
                SqlParameter[] parametros = {
                    new SqlParameter("@idProducto", tasaInteres.ProductoPrestamo.IdProducto),
                    new SqlParameter("@cuotasDesde", tasaInteres.CuotasDesde),
                    new SqlParameter("@cuotasHasta", tasaInteres.CuotasHasta),
                    new SqlParameter("@tasaMensual", tasaInteres.TasaMensual)
                };

                return AccesoDatos.EjecutarComandoConId(sql, parametros);


            } catch (Exception ex) {

                throw ex;
            }

        }

        // Metodo para actualizar una tasa de interes en la DB
        public bool actualizar(TasaInteres tasaInteres) {
            String sql = @"UPDATE TASAINTERES
                                    SET idProducto = @idProducto,
                                        cuotasDesde = @cuotasDesde,
                                        cuotasHasta = @cuotasHasta,
                                        tasaMensual = @tasaMensual
                                WHERE idTasaInteres = @idTasaInteres";

            try {
                SqlParameter[] parametros = {
                    new SqlParameter("@idProducto", tasaInteres.ProductoPrestamo.IdProducto),
                    new SqlParameter("@idCliente", tasaInteres.CuotasDesde),
                    new SqlParameter("@idUsuarioAprobador", tasaInteres.CuotasHasta),
                    new SqlParameter("@monto", tasaInteres.TasaMensual)
                };

                if (AccesoDatos.EjecutarComando(sql, parametros) != 0) return true;
                else return false;

            } catch (Exception ex) {

                throw ex;
            }

        }


        // Metodo para eliminar una tasa de interes de la DB
        public bool eliminar(TasaInteres tasaInteres) {
            String sql = @"DELETE FROM TASAINTERES WHERE idTasaInteres = @idTasaInteres";

            try {
                SqlParameter[] parametros = {
                    new SqlParameter("@idTasaInteres", tasaInteres.IdTasaInteres)
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
