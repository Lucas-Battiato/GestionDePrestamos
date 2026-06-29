using Entidades;
using Negocio.Datos;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Servicios;

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
            dgvSolicitudes.DataSource = prestamoDatos.listarSolicitados();
            dgvSolicitudes.DataBind();

        }
    }
}