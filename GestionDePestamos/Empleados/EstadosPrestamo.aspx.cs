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
    public partial class EstadosPrestamo : System.Web.UI.Page
    {
        EstadoPrestamoDatos estadoPrestamoDatos = new EstadoPrestamoDatos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if ((Usuario)Session["usuario"] == null) {
                    Response.Redirect("~/Inicio.aspx");
                }

                // Si el usuario no es administrador lo mando de nuevo a la pantalla de Empleados
                if (((Usuario)Session["usuario"]).Rol.Descripcion != "Administrador") {
                    Response.Redirect("~/Empleados/Empleados.aspx");
                }

                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            dgvEstadosPrestamo.DataSource = estadoPrestamoDatos.Listar();
            dgvEstadosPrestamo.DataBind();
        }
    }
}