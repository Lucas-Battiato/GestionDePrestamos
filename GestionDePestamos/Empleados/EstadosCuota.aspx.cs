using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio.Datos;

namespace GestionDePestamos.Empleados
{
    public partial class EstadosCuota : System.Web.UI.Page
    {
        EstadoCuotaDatos estadoCuotaDatos = new EstadoCuotaDatos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                CargarGrilla();
            }
        }

        private void CargarGrilla()
        {
            dgvEstadosCuota.DataSource = estadoCuotaDatos.Listar();
            dgvEstadosCuota.DataBind();
        }
    }
}