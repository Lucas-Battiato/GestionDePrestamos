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
            // Variable para testear (Cambia a "Operador" para ver como desaparece el menu amarillo)
            string rolUsuario = "Admin";

            if (rolUsuario != "Admin")
            {
                // Apagamos el menu de administracion completo desde el servidor
                navItemAdmin.Visible = false;
            }
            else
            {
                navItemAdmin.Visible = true;
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