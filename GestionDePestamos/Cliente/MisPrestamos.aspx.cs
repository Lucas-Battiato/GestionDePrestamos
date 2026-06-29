using Entidades;
using Entidades.DTOs;
using Negocio.Datos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionDePestamos.Cliente {
    public partial class MisPrestamos : System.Web.UI.Page {

        CuotaDatos cuotaDatos = new CuotaDatos();
        PrestamoDatos prestamoDatos = new PrestamoDatos();

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                if (Session["cliente"] == null) {
                    Response.Redirect("~/Inicio.aspx");
                }
                cargarPrestamos();
            }
        }

        private void cargarPrestamos() {
            Entidades.Cliente cliente = (Entidades.Cliente)Session["cliente"];
            List<Prestamo> listaPrestamos = prestamoDatos.ListarPorCliente(cliente.IdCliente);

            
            List<PrestamoDTO> listaPrestamosSinRechazados = new List<PrestamoDTO>();

            foreach (Prestamo p in listaPrestamos) {
                int estado = p.EstadoPrestamo.IdEstadoPrestamo;
                if (estado == 3) continue; // Los rechazados no los muestro porque nunca llegaron a estar activos.

                listaPrestamosSinRechazados.Add(new PrestamoDTO {
                    IdPrestamo = p.IdPrestamo,
                    NombreProducto = p.ProductoPrestamo.Nombre,
                    Monto = p.Monto.ToString("N2", new CultureInfo("es-AR")),
                    CuotasRestantes = p.CuotasRestantes,
                    EstadoDescripcion = p.EstadoPrestamo.Descripcion,
                    EstadoCssClass = estado == 4 ? "bg-primary" : // En Curso - azul
                                     estado == 1 ? "bg-warning text-dark" : // Solicitado - amariillo
                                     estado == 2 ? "bg-info text-dark" : // Aprobado - celeste
                                     estado == 5 ? "bg-success" : "bg-secondary" //Finalizados- verde
                });
            }

            if (listaPrestamosSinRechazados.Count == 0) {
                pnlSinPrestamos.Visible = true;
                rptPrestamos.Visible = false;
                return;
            }

            rptPrestamos.DataSource = listaPrestamosSinRechazados;
            rptPrestamos.DataBind();
        }

        protected void rptPrestamos_ItemDataBound(object sender, RepeaterItemEventArgs e) {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) {
                PrestamoDTO prestamo = (PrestamoDTO)e.Item.DataItem;
                GridView dgvCuotas = (GridView)e.Item.FindControl("dgvCuotas");

                List<Cuota> cuotas = cuotaDatos.ListarPorPrestamo(prestamo.IdPrestamo);
                List<CuotaDTO> vistaCuotas = new List<CuotaDTO>();

                int numero = 1;
                foreach (Cuota c in cuotas) {
                    vistaCuotas.Add(new CuotaDTO {
                        NumeroCuota = numero,
                        Monto = c.Monto,
                        FechaVencimiento = c.FechaVencimiento,
                        FechaPago = c.FechaPago,
                        MetodoPago = c.MetodoPago?.Descripcion,
                        EstadoDescripcion = c.EstadoCuota.Descripcion,
                        EstadoCssClass = c.EstadoCuota.IdEstadoCuota == 2 ? "bg-success" :
                                         c.EstadoCuota.IdEstadoCuota == 3 ? "bg-danger" : "bg-secondary"
                    });
                    numero++;
                }

                dgvCuotas.DataSource = vistaCuotas;
                dgvCuotas.DataBind();
            }
        }
    }
}