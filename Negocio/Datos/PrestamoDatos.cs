using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Entidades;
using Entidades.DTOs;

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


        public List<SolicitudPrestamoDTO> listarSolicitados() {
            try {
                List<SolicitudPrestamoDTO> lista = new List<SolicitudPrestamoDTO>();

                string sql = @"SELECT p.idPrestamo, p.idCliente, c.username, pr.Nombre, p.fechaUltimaActualizacion,
                        p.monto, p.monto + p.interesTotal as MontoADevolver, p.interesTotal as GananciaEstimada,
                        p.cantidadCuotas
                    FROM Prestamo p
                    INNER JOIN ProductoPrestamo pr ON p.idProducto = pr.idProducto
                    INNER JOIN Cliente c ON p.idCliente = c.idCliente
                    WHERE p.idEstadoPrestamo = 1
                    ORDER BY p.fechaUltimaActualizacion DESC";

                DataTable tabla = AccesoDatos.EjecutarConsulta(sql);

                foreach (DataRow fila in tabla.Rows) {
                    lista.Add(new SolicitudPrestamoDTO {
                        IdPrestamo = (int)fila["idPrestamo"],
                        IdCliente = (int)fila["idCliente"],
                        UsernameCliente = fila["username"].ToString(),
                        NombreProducto = fila["Nombre"].ToString(),
                        FechaSolicitud = (DateTime)fila["fechaUltimaActualizacion"],
                        MontoSolicitado = (decimal)fila["monto"],
                        MontoADevolver = (decimal)fila["MontoADevolver"],
                        GananciaEstimada = (decimal)fila["GananciaEstimada"],
                        DetalleCuotas = fila["cantidadCuotas"].ToString() + " cuotas"
                    });
                }

                return lista;

            } catch (Exception ex) {
                throw ex;
            }
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

        // Agrego un nuevo prestamo con usuario aprobador nulo, fecha de aprobación nula y estado 'Solicitado' (1) y devuelvo el ID del prestamo creado.
        public int agregar(Prestamo prestamo) {
            String sql = @"INSERT INTO PRESTAMO (idProducto, idCliente, idUsuarioAprobador, monto, interesTotal, cantidadCuotas, cuotasRestantes, fechaAprobacion, fechaUltimaActualizacion, idEstadoPrestamo)
                                    VALUES (@idProducto, @idCliente, null, @monto, @interesTotal, @cantidadCuotas, @cuotasRestantes, null, GETDATE(), 1);";

            try {
                SqlParameter[] parametros = {
                    new SqlParameter("@idProducto", prestamo.ProductoPrestamo.IdProducto),
                    new SqlParameter("@idCliente", prestamo.Cliente.IdCliente),
                    new SqlParameter("@monto", prestamo.Monto),
                    new SqlParameter("@interesTotal", prestamo.InteresTotal),
                    new SqlParameter("@cantidadCuotas", prestamo.CantidadCuotas),
                    new SqlParameter("@cuotasRestantes", prestamo.CuotasRestantes)
                };
            
                return AccesoDatos.EjecutarComandoConId(sql, parametros);


            } catch (Exception ex) {

                throw ex;
            }

        }

        // Cambio estado de un prestamo y devuelvo true si se afecto una fila. Si ningún prestamo fue afectado, devuelvo false.
        public bool cambiarEstado(Prestamo prestamo) {
            String sql = @"UPDATE PRESTAMO
                                    SET idProducto = @idProducto,
                                        idCliente = @idCliente,
                                        idUsuarioAprobador = @idUsuarioAprobador,
                                        monto = @monto,
                                        interesTotal = @interesTotal,
                                        cantidadCuotas = @cantidadCuotas,
                                        cuotasRestantes = @cuotasRestantes,
                                        fechaAprobacion = @fechaAprobacion,
                                        fechaUltimaActualizacion = GETDATE(),
                                        idEstadoPrestamo = @idEstadoPrestamo
                                WHERE idPrestamo = @idPrestamo";

            try {
                SqlParameter[] parametros = {
                    new SqlParameter("@idProducto", prestamo.ProductoPrestamo.IdProducto),
                    new SqlParameter("@idCliente", prestamo.Cliente.IdCliente),
                    new SqlParameter("@idUsuarioAprobador", prestamo.UsuarioAprobador.IdUsuario),
                    new SqlParameter("@monto", prestamo.Monto),
                    new SqlParameter("@interesTotal", prestamo.InteresTotal),
                    new SqlParameter("@cantidadCuotas", prestamo.CantidadCuotas),
                    new SqlParameter("@cuotasRestantes", prestamo.CuotasRestantes),
                    new SqlParameter("@fechaAprobacion", prestamo.FechaAprobacion),
                    new SqlParameter("@idEstadoPrestamo", prestamo.EstadoPrestamo.IdEstadoPrestamo),
                    new SqlParameter("@idPrestamo", prestamo.IdPrestamo)
                };

                if (AccesoDatos.EjecutarComando(sql, parametros) != 0) return true;
                    else return false;

            } catch (Exception ex) {

                throw ex;
            }

        }
    }
}
