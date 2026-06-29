using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionDePestamos.Empleados {
    public partial class Empleados : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                if (Session["usuario"] == null) {
                    Response.Redirect("~/Inicio.aspx");
                }

                lblNombreUsuario.Text = ((Usuario)Session["usuario"]).Username;

                ConfigurarPermisos();
            }
        }

        private void ConfigurarPermisos() {

            lblRolActual.Text = ((Usuario)Session["usuario"]).Rol.Descripcion;
            
            // Si el usuario es Operador oculto el panel de admin. Si es Administrador, lo habilito.
            switch (((Usuario)Session["usuario"]).Rol.Descripcion) {
                case "Administrador":
                    pnlAdministrador.Visible = true;
                    break;

                case "Operador":
                    pnlAdministrador.Visible = false;
                    break;
            }
        }
    }
}