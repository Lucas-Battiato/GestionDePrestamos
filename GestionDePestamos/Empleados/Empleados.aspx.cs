using Entidades;
using Negocio.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionDePestamos.Empleados {
    public partial class Empleados : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                if (Session["usuario"] == null) {
                    Response.Redirect("~/Inicio.aspx");
                }

                lblNombreUsuario.Text = ((Usuario)Session["usuario"]).Username;

                ConfigurarPermisos();

                cargarKPIs();
            }
        }

        private void ConfigurarPermisos() {

            lblRolActual.Text = ((Usuario)Session["usuario"]).Rol.Descripcion;
            
            // Si el usuario es Operador oculto el panel de admin. Si es Administrador, lo habilito.
            switch (((Usuario)Session["usuario"]).Rol.Descripcion) {
                case "Administrador":
                    pnlAdministrador.Visible = true;
                    break;

                case "Operador":
                    pnlAdministrador.Visible = false;
                    break;
            }
        }


        private void cargarKPIs() {

            // KpiSolicitudes pendientes de evaluación
            PrestamoDatos prestamoDatos = new PrestamoDatos();
            List<Prestamo> listaPrestamos = prestamoDatos.Listar();

            lblKpiSolicitudes.Text = listaPrestamos.Count(p => p.EstadoPrestamo.Descripcion == "Solicitado").ToString();


            // KpiClientes registrados en el sistema
            ClienteDatos clienteDatos = new ClienteDatos();
            List<Entidades.Cliente> listaClientes = clienteDatos.Listar();

            lblKpiClientes.Text = listaClientes.Count().ToString();


            // KpiAprobados esta semana
            DateTime inicioSemana = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday); // Domingo=0, lunes = 1, etc
            lblKpiAprobados.Text = listaPrestamos.Count(p => p.FechaAprobacion != null && p.FechaAprobacion >= inicioSemana).ToString();

        }
    }
}