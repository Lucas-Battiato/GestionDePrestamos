using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionDePestamos.Cliente
{
    public partial class SolicitarPrestamo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["UsuarioId"] == null)
                {
                    Response.Redirect("~/Inicio.aspx");
                }
            }
        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
 
            btnConfirmar.Visible = true;
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/Cliente/MisPrestamos.aspx");
        }
    }
}