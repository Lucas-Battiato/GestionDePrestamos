using Entidades;
using Negocio.Datos;
using System;
using System.Web.UI;

namespace GestionDePestamos.Empleados {
    public partial class GestionClientes : System.Web.UI.Page {

        ClienteDatos clienteDatos = new ClienteDatos();

        protected void Page_Load(object sender, EventArgs e) {

            if ((Usuario)Session["usuario"] == null) {
                Response.Redirect("~/Inicio.aspx");
            }

            if (!IsPostBack) {

                cargarGrilla();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e) {
        }

        protected void btnLimpiar_Click(object sender, EventArgs e) {
        }

        private void cargarGrilla() {
            dgvClientes.DataSource = clienteDatos.Listar();
            dgvClientes.DataBind();
        }
    }
}