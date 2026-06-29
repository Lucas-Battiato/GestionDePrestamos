using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionDePestamos.MasterPages
{
    public partial class Empleado : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConfigurarMenuSegunRol();
            }
        }

        private void ConfigurarMenuSegunRol()
        {
            // Si el usuario es Operador oculto el panel de admin. Si es Administrador, lo habilito.
            switch (((Usuario)Session["usuario"]).Rol.Descripcion) {
                case "Administrador":
                    navItemAdmin.Visible = true;
                    break;

                case "Operador":
                    navItemAdmin.Visible = false;
                    break;
            }

        }
        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {

            Session.Clear();
            Session.Abandon();

            Response.Redirect("~/Inicio.aspx");
        }
    }
}