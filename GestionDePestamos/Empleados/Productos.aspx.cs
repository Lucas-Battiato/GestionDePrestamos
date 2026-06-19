using Negocio.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionDePestamos.Empleados {
    public partial class Productos : System.Web.UI.Page {

        ProductoPrestamoDatos productoPrestamoDatos = new ProductoPrestamoDatos();
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {

                CargarGrilla();
            }
        }

        private void CargarGrilla() {
            dgvProductos.DataSource = productoPrestamoDatos.Listar();
            dgvProductos.DataBind();

        }
    }
}