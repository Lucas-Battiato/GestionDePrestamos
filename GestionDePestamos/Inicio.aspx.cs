using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio.Datos;

namespace GestionDePestamos
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            ClienteDatos clienteDatos = new ClienteDatos();
            Entidades.Cliente cliente = new Entidades.Cliente();
            cliente = clienteDatos.ObtenerPorId(1);

            Session.Add("cliente", cliente);
       
            Response.Redirect("~/Cliente/Clientes.aspx");
        }
    }
}