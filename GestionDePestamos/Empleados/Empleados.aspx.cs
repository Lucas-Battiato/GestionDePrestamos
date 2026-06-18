using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionDePestamos.Empleados
{
    public partial class Empleados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["usuario"] == null) {
                    Response.Redirect("~/Inicio.aspx");
                }

                lblNombreUsuario.Text = ((Usuario)Session["usuario"]).Username;

                ConfigurarPermisos();
            }
        }

        private void ConfigurarPermisos()
        {
            // Simulamos que leemos la base de datos y sabemos el rol.
            // (Cambiar "Operador" por "Admin" para ver como aparece el menu de abajo)
            string rolUsuario = "Admin";

            // Mostramos el rol en la pantalla para que sepas con que cuenta estas ingresando
            lblRolActual.Text = rolUsuario;

            
            if (rolUsuario != "Admin")
            {
                
                pnlAdministrador.Visible = false;
            }
            else
            {
            
                pnlAdministrador.Visible = true;
            }
        }
    }
}