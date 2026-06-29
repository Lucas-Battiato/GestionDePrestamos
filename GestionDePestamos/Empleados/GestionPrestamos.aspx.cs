using Entidades;
using Entidades;
using Entidades.DTOs;
using Negocio.Datos;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionDePestamos.Empleados {
    public partial class GestionPrestamos : System.Web.UI.Page {
        PrestamoDatos prestamoDatos = new PrestamoDatos();

        protected void Page_Load(object sender, EventArgs e) {
            
            if ((Usuario)Session["usuario"] == null) {
                Response.Redirect("~/Inicio.aspx");
            }

            if (!IsPostBack) {
                cargarGrilla();
            }
        }

        protected void btnAprobar_Click(object sender, EventArgs e) {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "modal", "new bootstrap.Modal(document.getElementById('modalDecision')).show();", true);

            LinkButton boton = (LinkButton)sender;
            GridViewRow fila = (GridViewRow)boton.NamingContainer;
            hfIdPrestamo.Value = dgvSolicitudes.DataKeys[fila.RowIndex].Value.ToString(); //Tomo el ID del prestamo
        }

        protected void btnRechazar_Click(object sender, EventArgs e) {
        }

        protected void btnConfirmarDecision_Click(object sender, EventArgs e) {
            PrestamoServicio prestamoServicio = new PrestamoServicio();

            prestamoServicio.aprobar(int.Parse(hfIdPrestamo.Value), (Usuario)Session["usuario"], txtObservacion.Text);

            cargarGrilla();
        }


        protected void btnDescargar_Click(object sender, EventArgs e) {
            LinkButton boton = (LinkButton)sender;
            GridViewRow fila = (GridViewRow)boton.NamingContainer;
            int idPrestamo = int.Parse(dgvSolicitudes.DataKeys[fila.RowIndex].Value.ToString()); // Leo el ID del prestamo desde la fila en donde esta ubicado el boton.

            string ruta = Server.MapPath($"~/ArchivosSistema/Recibos/recibo_Prestamo{idPrestamo}.pdf");

            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", $"attachment; filename=recibo_Prestamo{idPrestamo}.pdf");
            Response.TransmitFile(ruta);
            Response.End();
        }


        private void cargarGrilla() {

            List<SolicitudPrestamoDTO> listaSolicitudes = prestamoDatos.listarSolicitados();

            foreach (SolicitudPrestamoDTO solicitud in listaSolicitudes) {
                decimal puntuacion = obtenerPuntuacionCrediticia(solicitud.IdCliente);
                solicitud.PuntuacionTexto = puntuacion == -1 ? "Sin historial" : $"{puntuacion}%";
                solicitud.PuntuacionCssClass = puntuacion == -1 ? "bg-secondary" :
                                       puntuacion >= 80 ? "bg-success" :
                                       puntuacion >= 50 ? "bg-warning text-dark" : "bg-danger";
            }

            dgvSolicitudes.DataSource = listaSolicitudes;
            dgvSolicitudes.DataBind();

        }

        // Obtengo una puntuación crediticia calculada como creditosFinalizados / creditosTotalesObtenidos
        private decimal obtenerPuntuacionCrediticia(int idCliente) {
            List<Prestamo> historial = prestamoDatos.ListarPorCliente(idCliente);
            int finalizados = historial.Count(p => p.EstadoPrestamo.IdEstadoPrestamo == 5);
            int cancelados = historial.Count(p => p.EstadoPrestamo.IdEstadoPrestamo == 6);
            int total = finalizados + cancelados;
            if (total == 0) return -1;
            return Math.Round((decimal)finalizados / total * 100, 1);
        }
    }
}