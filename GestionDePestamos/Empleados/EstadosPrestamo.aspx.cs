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