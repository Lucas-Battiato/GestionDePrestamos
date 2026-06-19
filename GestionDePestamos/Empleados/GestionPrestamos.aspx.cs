using Entidades;
using Negocio.Datos;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;

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
        }

        protected void btnRechazar_Click(object sender, EventArgs e) {
        }

        protected void btnConfirmarDecision_Click(object sender, EventArgs e) {
        }

        private void cargarGrilla() {
            dgvSolicitudes.DataSource = prestamoDatos.listarSolicitados();
            dgvSolicitudes.DataBind();

        }
    }
}