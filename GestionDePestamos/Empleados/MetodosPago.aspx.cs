using Entidades;
using Negocio.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionDePestamos.Empleados
{
    public partial class MetodosPago : System.Web.UI.Page
    {
        MetodoPagoDatos metodoPagoDatos = new MetodoPagoDatos();

        protected void Page_Load(object sender, EventArgs e)
        {

            if ((Usuario)Session["usuario"] == null) {
                Response.Redirect("~/Inicio.aspx");
            }

            // Si el usuario no es administrador lo mando de nuevo a la pantalla de Empleados
            if (((Usuario)Session["usuario"]).Rol.Descripcion != "Administrador") {
                Response.Redirect("~/Empleados/Empleados.aspx");
            }

            if (!IsPostBack)
            {
                
                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            dgvMetodos.DataSource = metodoPagoDatos.Listar();
            dgvMetodos.DataBind();

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            metodoPagoDatos.guardar(new Entidades.MetodoPago { Descripcion=txtDescripcion.Text });

            txtDescripcion.Text = "";
            CargarGrilla();
        }

        //protected void btnEditar_Click(object sender, EventArgs e) {
        //    Entidades.MetodoPago metodoPago = (Entidades.MetodoPago)dgvMetodos.SelectedValue;


        //    metodoPagoDatos.modificar((Entidades.MetodoPago)dgvMetodos.SelectedValue);
        //}

        protected void btnEliminar_Click(object sender, EventArgs e) {
            LinkButton boton = (LinkButton)sender;
            GridViewRow fila = (GridViewRow)boton.NamingContainer;
            int id = int.Parse(dgvMetodos.DataKeys[fila.RowIndex].Value.ToString()); //Tomo el ID

            Entidades.MetodoPago metodoPago = new Entidades.MetodoPago() { IdMetodoPago = id };

            metodoPagoDatos.eliminar(metodoPago);
            CargarGrilla();
        }
    }
}