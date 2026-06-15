using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


    namespace GestionDePestamos.Cliente
    {
        public partial class MisPrestamos : System.Web.UI.Page
        {
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    // Conexion a bd
                }
            }

            protected void dgvMisPrestamos_RowCommand(object sender, GridViewCommandEventArgs e)
            {
            // Verificar que el comando sea el esperado
            if (e.CommandName == "VerDetalle")
                {
                //ID del prestamo seleccionado
                int idPrestamoSeleccionado = Convert.ToInt32(e.CommandArgument);
                // Redireccionar a la pagina de detalle del prestamo, pasando el ID como parametro
                Response.Redirect("~/Cliente/DetallePrestamo.aspx?id=" + idPrestamoSeleccionado);
                }
            }
        }
    }