using Entidades;
using Negocio.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionDePestamos.MasterPages
{
    public partial class Cliente : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            
            Session.Clear();
            Session.Abandon();

            Response.Redirect("~/Inicio.aspx");
        }

        protected void btnSolicitarPrestamo_Click(object sender, EventArgs e) {
            PrestamoDatos prestamoDatos = new PrestamoDatos();
            Prestamo prestamoActivo = prestamoDatos.buscarActivosPorCliente((Entidades.Cliente)Session["cliente"]);
            if (prestamoActivo != null) {
                modalBodyMaster.Text = $"Estimado cliente, le informamos que para solicitar un nuevo prestamo primero deberá saldar la deuda pendiente en el credito N°{prestamoActivo.IdPrestamo})";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "modal", "new bootstrap.Modal(document.getElementById('modalErrorMaster')).show();", true);
                return;
            }

            Response.Redirect("~/Cliente/SolicitarPrestamo.aspx");
        }
    }
}