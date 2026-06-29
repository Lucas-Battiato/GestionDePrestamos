using Entidades;
using GestionDePestamos.MasterPages;
using Negocio.Datos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionDePestamos.Cliente
{
    public partial class Clientes : System.Web.UI.Page
    {
        PrestamoDatos prestamoDatos = new PrestamoDatos();
        Prestamo prestamoActivo;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                if (Session["cliente"] == null) {
                    Response.Redirect("~/Inicio.aspx");
                }
                
                lblNombreCliente.Text = ((Entidades.Cliente) Session["cliente"]).Username;

                prestamoActivo = prestamoDatos.buscarActivosPorCliente((Entidades.Cliente)Session["cliente"]);
                if (prestamoActivo != null) {
                    CuotaDatos cuotaDatos = new CuotaDatos();
                    List<Cuota> cuotasCliente = cuotaDatos.ListarPorPrestamo(prestamoActivo.IdPrestamo);

                    foreach(Cuota c in cuotasCliente) {
                        if (c.EstadoCuota.Descripcion == "Pendiente" || c.EstadoCuota.Descripcion == "Vencida") {
                            lblMontoProximo.Text = c.Monto.ToString("N2", new CultureInfo("es-AR"));
                            lblFechaProximo.Text = c.FechaVencimiento.ToLongDateString();
                            break;
                        }
                    }

                    lblCantidadPrestamos.Text = "1";

                    lblAlDia.Text = "";
                }

            }

        }


        protected void btnSolicitarPrestamo_Click(object sender, EventArgs e)
        {
            
            prestamoActivo = prestamoDatos.buscarActivosPorCliente((Entidades.Cliente)Session["cliente"]);
            if (prestamoActivo != null) {
                modalBody.Text = $"Estimado cliente, le informamos que para solicitar un nuevo prestamo primero deberá saldar la deuda pendiente en el credito N°{prestamoActivo.IdPrestamo})";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "modal", "new bootstrap.Modal(document.getElementById('modalError')).show();", true);
                return;
            }

            Response.Redirect("~/Cliente/SolicitarPrestamo.aspx");
        }

    }
}