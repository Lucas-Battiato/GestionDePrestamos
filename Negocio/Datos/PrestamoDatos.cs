using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;

namespace Negocio.Datos
{
    // Clase de acceso a datos para la entidad Prestamo.
    // Por ahora solo tiene lectura. Los metodos de escritura se implementan en la Etapa 3 (core).
    public class PrestamoDatos
    {
        // Retorna todos los prestamos con producto, cliente, estado y usuario aprobador incluidos.
        // El usuario aprobador puede ser null si el prestamo todavia no fue evaluado.
        public List<Prestamo> Listar()
        {
            List<Prestamo> lista = new List<Prestamo>();

            string sql = @"SELECT p.idPrestamo, p.monto, p.interesTotal,
                                  p.cantidadCuotas, p.cuotasRestantes,
                                  p.fechaAprobacion, p.fechaUltimaActualizacion,
                                  pr.idProducto, pr.nombre AS nombreProducto,
                                  pr.descripcion AS descripcionProducto,
                                  pr.montoMinimo, pr.montoMaximo,
                                  pr.cuotasMinimas, pr.cuotasMaximas,
                                  c.idCliente, c.username AS usernameCliente,
                                  c.email, c.telefono, c.direccion,
                                  ep.idEstadoPrestamo, ep.descripcion AS descripcionEstado,
                                  p.idUsuarioAprobador,
                                  u.username AS usernameAprobador
                           FROM Prestamo p
                           INNER JOIN ProductoPrestamo pr ON p.idProducto       = pr.idProducto
                           INNER JOIN Cliente          c  ON p.idCliente        = c.idCliente
                           INNER JOIN EstadoPrestamo   ep ON p.idEstadoPrestamo = ep.idEstadoPrestamo
                           LEFT  JOIN Usuario          u  ON p.idUsuarioAprobador = u.idUsuario
                           ORDER BY p.fechaUltimaActualizacion DESC";

            DataTable tabla = AccesoDatos.EjecutarConsulta(sql);

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(MapearFila(fila));
            }

            return lista;
        }

        // Busca y retorna un prestamo por su ID con todas sus relaciones incluidas. Si no existe, retorna null.
        public Prestamo ObtenerPorId(int idPrestamo)
        {
            string sql = @"SELECT p.idPrestamo, p.monto, p.interesTotal,
                                  p.cantidadCuotas, p.cuotasRestantes,
                                  p.fechaAprobacion, p.fechaUltimaActualizacion,
                                  pr.idProducto, pr.nombre AS nombreProducto,
                                  pr.descripcion AS descripcionProducto,
                                  pr.montoMinimo, pr.montoMaximo,
                                  pr.cuotasMinimas, pr.cuotasMaximas,
                                  c.idCliente, c.username AS usernameCliente,
                                  c.email, c.telefono, c.direccion,
                                  ep.idEstadoPrestamo, ep.descripcion AS descripcionEstado,
                                  p.idUsuarioAprobador,
                                  u.username AS usernameAprobador
                           FROM Prestamo p
                           INNER JOIN ProductoPrestamo pr ON p.idProducto       = pr.idProducto
                           INNER JOIN Cliente          c  ON p.idCliente        = c.idCliente
                           INNER JOIN EstadoPrestamo   ep ON p.idEstadoPrestamo = ep.idEstadoPrestamo
                           LEFT  JOIN Usuario          u  ON p.idUsuarioAprobador = u.idUsuario
                           WHERE p.idPrestamo = @id";

            SqlParameter[] parametros = { new SqlParameter("@id", idPrestamo) };
            DataTable tabla = AccesoDatos.EjecutarConsulta(sql, parametros);

            if (tabla.Rows.Count == 0) return null;
            return MapearFila(tabla.Rows[0]);
        }

        // Retorna todos los prestamos de un cliente especifico, ordenados por fecha de actualizacion.
        // Usado para mostrar al cliente solo sus propios prestamos.
        public List<Prestamo> ListarPorCliente(int idCliente)
        {
            List<Prestamo> lista = new List<Prestamo>();

            string sql = @"SELECT p.idPrestamo, p.monto, p.interesTotal,
                                  p.cantidadCuotas, p.cuotasRestantes,
                                  p.fechaAprobacion, p.fechaUltimaActualizacion,
                                  pr.idProducto, pr.nombre AS nombreProducto,
                                  pr.descripcion AS descripcionProducto,
                                  pr.montoMinimo, pr.montoMaximo,
                                  pr.cuotasMinimas, pr.cuotasMaximas,
                                  c.idCliente, c.username AS usernameCliente,
                                  c.email, c.telefono, c.direccion,
                                  ep.idEstadoPrestamo, ep.descripcion AS descripcionEstado,
                                  p.idUsuarioAprobador,
                                  u.username AS usernameAprobador
                           FROM Prestamo p
                           INNER JOIN ProductoPrestamo pr ON p.idProducto       = pr.idProducto
                           INNER JOIN Cliente          c  ON p.idCliente        = c.idCliente
                           INNER JOIN EstadoPrestamo   ep ON p.idEstadoPrestamo = ep.idEstadoPrestamo
                           LEFT  JOIN Usuario          u  ON p.idUsuarioAprobador = u.idUsuario
                           WHERE p.idCliente = @idCliente
                           ORDER BY p.fechaUltimaActualizacion DESC";

            SqlParameter[] parametros = { new SqlParameter("@idCliente", idCliente) };
            DataTable tabla = AccesoDatos.EjecutarConsulta(sql, parametros);

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(MapearFila(fila));
            }

            return lista;
        }

        // public int Agregar(Prestamo prestamo) { ... }
        // public bool Modificar(Prestamo prestamo) { ... }
        // public bool CambiarEstado(int idPrestamo, int idEstado) { ... }

        // Convierte una fila del DataTable en un objeto Prestamo con todas sus relaciones anidadas.
        // El usuario aprobador se carga solo si existe en la base de datos.
        private Prestamo MapearFila(DataRow fila)
        {
            Usuario aprobador = null;
            if (fila["idUsuarioAprobador"] != DBNull.Value)
            {
                aprobador = new Usuario
                {
                    IdUsuario = (int)fila["idUsuarioAprobador"],
                    Username  = fila["usernameAprobador"].ToString()
                };
            }

            return new Prestamo
            {
                IdPrestamo               = (int)fila["idPrestamo"],
                Monto                    = (decimal)fila["monto"],
                InteresTotal             = (decimal)fila["interesTotal"],
                CantidadCuotas          = (short)fila["cantidadCuotas"],
                CuotasRestantes         = (short)fila["cuotasRestantes"],
                FechaAprobacion         = fila["fechaAprobacion"] != DBNull.Value
                                            ? (DateTime?)fila["fechaAprobacion"] : null,
                FechaUltimaActualizacion = (DateTime)fila["fechaUltimaActualizacion"],
                UsuarioAprobador        = aprobador,
                ProductoPrestamo = new ProductoPrestamo
                {
                    IdProducto    = (int)fila["idProducto"],
                    Nombre        = fila["nombreProducto"].ToString(),
                    Descripcion   = fila["descripcionProducto"].ToString(),
                    MontoMinimo   = (decimal)fila["montoMinimo"],
                    MontoMaximo   = (decimal)fila["montoMaximo"],
                    CuotasMinimas = (int)fila["cuotasMinimas"],
                    CuotasMaximas = (int)fila["cuotasMaximas"]
                },
                Cliente = new Cliente
                {
                    IdCliente = (int)fila["idCliente"],
                    Username  = fila["usernameCliente"].ToString(),
                    Email     = fila["email"].ToString(),
                    Telefono  = fila["telefono"].ToString(),
                    Direccion = fila["direccion"].ToString()
                },
                EstadoPrestamo = new EstadoPrestamo
                {
                    IdEstadoPrestamo = (int)fila["idEstadoPrestamo"],
                    Descripcion      = fila["descripcionEstado"].ToString()
                }
            };
        }
    }
}
