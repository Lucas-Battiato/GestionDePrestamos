using Entidades;
using Negocio.Datos;
using Servicios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionDePestamos.Cliente {
    public partial class SolicitarPrestamo : System.Web.UI.Page {
        ProductoPrestamoDatos productosDatos = new ProductoPrestamoDatos();
        ProductoPrestamo productoSeleccionado;
        ProductoPrestamoDatos datos = new ProductoPrestamoDatos();
        PrestamoServicio prestamoServicio = new PrestamoServicio();
        Prestamo prestamoSimulado;


        protected void Page_Load(object sender, EventArgs e) {
            
            if (!IsPostBack) {

                if (Session["cliente"] == null) {
                    Response.Redirect("~/Inicio.aspx");
                }

                ddlProducto.DataSource = productosDatos.Listar();
                ddlProducto.DataValueField = "IdProducto";
                ddlProducto.DataTextField = "Nombre";
                ddlProducto.DataBind();

                productoSeleccionado = datos.ObtenerPorId(int.Parse(ddlProducto.SelectedValue));
                actualizarAtributoMonto(productoSeleccionado);
                cargarCuotas(productoSeleccionado);
            }

        }


        protected void btnCalcular_Click(object sender, EventArgs e) {
            productoSeleccionado = datos.ObtenerPorId(int.Parse(ddlProducto.SelectedValue));
            
            // Si no se ingresa un monto, llamo al Modal del .aspx
            if (txtMonto.Text.Trim() == "") {
                modalBody.Text = "El monto no puede estar vacio.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "modal", "new bootstrap.Modal(document.getElementById('modalError')).show();", true);
                return;
            }

            // Si ingresa un valor negativo o algun caracter no numerico llamo al modal2. Reemplazo , con . para evitar errores con decimales.
            if (!decimal.TryParse(txtMonto.Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal monto) || monto <= 0) {
                modalBody.Text = "Por favor, ingrese solo numeros positivos en el monto.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "modal", "new bootstrap.Modal(document.getElementById('modalError')).show();", true);
                return;
            }

            if (monto < productoSeleccionado.MontoMinimo || monto > productoSeleccionado.MontoMaximo) {
                modalBody.Text = "El monto ingresado se encuentre fuera de los margenes para el producto seleccionado.";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "modal", "new bootstrap.Modal(document.getElementById('modalError')).show();", true);
                return;
            }

            
            CuotaServicio cuotaServicio = new CuotaServicio();
            Entidades.Cliente clienteAux = (Entidades.Cliente)Session["cliente"];
            prestamoSimulado = prestamoServicio.simular(productoSeleccionado, monto, int.Parse(ddlCuotas.SelectedValue), (Entidades.Cliente)Session["cliente"]);
            Session.Add("prestamoSimulado", prestamoSimulado);

            lblMontoResumen.Text = $"${prestamoSimulado.Monto.ToString("N2", new CultureInfo("es-AR"))}";
            lblInteresResumen.Text = $"${prestamoSimulado.InteresTotal.ToString("N2", new CultureInfo("es-AR"))}";
            lblValorCuota.Text = $"${cuotaServicio.calcularCuota(prestamoSimulado).Monto.ToString("N2", new CultureInfo("es-AR"))}";
            lblCantCuotasResumen.Text = prestamoSimulado.CantidadCuotas.ToString();
            lblTotalDevolver.Text = $"${(cuotaServicio.calcularCuota(prestamoSimulado).Monto * prestamoSimulado.CantidadCuotas).ToString("N2", new CultureInfo("es-AR"))}";

            divRecibo.Visible = true;
            btnConfirmar.Visible = true;

        }

        protected void btnConfirmar_Click(object sender, EventArgs e) {
            
            if (!fuRecibo.HasFile || fuRecibo.PostedFile.ContentType != "application/pdf") {
                lblErrorRecibo.Text = "Debe adjuntar su recibo de sueldo en formato PDF.";
                lblErrorRecibo.Visible = true;
                return;
            }

            int idPrestamo = prestamoServicio.generar((Prestamo)Session["prestamoSimulado"]);

            // Defino la ruta de los recibos y creo la carpeta si no existe
            string rutaRecibo = Server.MapPath($"~/ArchivosSistema/Recibos/recibo_Prestamo{idPrestamo}.pdf");
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(rutaRecibo));
            fuRecibo.SaveAs(rutaRecibo);

            Response.Redirect("~/Cliente/MisPrestamos.aspx");

        }

        protected void ddlProducto_SelectedIndexChanged(object sender, EventArgs e) {
            productoSeleccionado = productosDatos.ObtenerPorId(int.Parse(ddlProducto.SelectedValue));
            cargarCuotas(productoSeleccionado);
            actualizarAtributoMonto(productoSeleccionado);
        }


        // Metodo para actualizar el placeholder y los minimos y maximos de "txtMonto" cada vez que se cambia el producto en el ddl
        private void actualizarAtributoMonto(ProductoPrestamo producto) {

            txtMonto.Attributes["min"] = ((int)producto.MontoMinimo).ToString();
            txtMonto.Attributes["max"] = ((int)producto.MontoMaximo).ToString();
            txtMonto.Attributes["placeholder"] = $"Min: {producto.MontoMinimo} - Max: {producto.MontoMaximo}";
        }


        // Metodo para cargar el ddl de cuotas dependiendo del producto seleccionado
        private void cargarCuotas(ProductoPrestamo producto) {
            ddlCuotas.Items.Clear();
            int cuota = producto.CuotasMinimas;
            while (cuota <= producto.CuotasMaximas) {
                ddlCuotas.Items.Add(new ListItem($"{cuota} Cuotas fijas", cuota.ToString()));
                cuota *= 2;
            }

            // Si el maximo no se cargo (por no ser multiplo de dos) lo seteo a mano
            if (ddlCuotas.Items.FindByValue(producto.CuotasMaximas.ToString()) == null)
                ddlCuotas.Items.Add(new ListItem($"{producto.CuotasMaximas} Cuotas fijas", producto.CuotasMaximas.ToString()));

            ddlCuotas.ClearSelection();
        }
    }
}