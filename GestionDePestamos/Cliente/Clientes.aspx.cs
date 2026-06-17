using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using GestionDePestamos.MasterPages;

namespace GestionDePestamos.Cliente
{
    public partial class Clientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                if (Session["cliente"] == null) {
                    Response.Redirect("~/Inicio.aspx");
                }
                
                lblNombreCliente.Text = ((Entidades.Cliente) Session["cliente"]).Username;

            }

        }
    }
}