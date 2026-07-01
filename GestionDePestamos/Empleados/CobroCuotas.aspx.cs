using Entidades;
using Entidades.DTOs;
using Negocio.Datos;
using Servicios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionDePestamos.Empleados {
    public partial class CobroCuotas : System.Web.UI.Page {

        Prestamo prestamo = new Prestamo();
        List<Prestamo> listaPrestamos = new List<Prestamo>();
        PrestamoDatos prestamoDatos = new PrestamoDatos();

        CuotaDatos cuotaDatos = new CuotaDatos();
        CuotaServicio cuotaServicio = new CuotaServicio();

        protected void Page_Load(object sender, EventArgs e) {
            if ((Usuario)Session["usuario"] == null) {
                Response.Redirect("~/Inicio.aspx");
            }

            if (!IsPostBack) {
                lblIdPrestamo.Text = "";
                lblCliente.Text = "";
                lblProducto.Text = "";
                lblMontoTotal.Text = "";
                lblCuotasRestantes.Text = "";
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e) {
            Prestamo prestamo = null;
            listaPrestamos.Clear();

            // Chequeo si es un ID de prestamo
            if (int.TryParse(txtBusqueda.Text, out int idPrestamo)) {
                //listaPrestamos.Add(prestamoDatos.ObtenerPorId(idPrestamo));
                prestamo = prestamoDatos.ObtenerPorId(idPrestamo);

                // Si no es un ID de prestamo, entonces es un nombre de usuario
            } else {
                if (txtBusqueda.Text.Trim() != "") {
                    ClienteDatos clienteDatos = new ClienteDatos();
                    Entidades.Cliente cliente = clienteDatos.ObtenerPorUsername(txtBusqueda.Text);

                    if (cliente != null) {
                        //listaPrestamos = prestamoDatos.ListarPorCliente(cliente.IdCliente);
                        prestamo = prestamoDatos.buscarEnCursoPorCliente(cliente);
                    }
                }
            }

            // Si encontre un prestamo con ese ID o nombre de usuario, habilito el panel de prestamo. Sino lo informo con el lblSinResultado
            // Cargo las cuotas del prestamo encontrado
            if (prestamo != null) {

                lblSinResultados.Visible = false;
                pnlPrestamo.Visible = true;

                hfIdPrestamoActual.Value = prestamo.IdPrestamo.ToString();

                cargarDatosPrestamo();
                cargarGrilla();

            } else {
                pnlPrestamo.Visible = false;
                lblSinResultados.Visible = true;
            }

        }

        protected void btnRegistrarPago_Click(object sender, EventArgs e) {
            LinkButton button = (LinkButton)sender;
            GridViewRow fila = (GridViewRow)button.NamingContainer;
            int id = int.Parse(dgvCuotas.DataKeys[fila.RowIndex].Value.ToString()); //Tomo el ID de la cuota seleccionada

            Cuota cuota = new Cuota();
            cuota = cuotaDatos.ObtenerPorId(id);

            hfIdCuota.Value = id.ToString();

            lblModalNumeroCuota.Text = (fila.RowIndex + 1).ToString();
            lblModalMontoCuota.Text = cuota.Monto.ToString("N2", new CultureInfo("es-AR"));

            MetodoPagoDatos metodoPagoDatos = new MetodoPagoDatos();
            ddlMetodoPago.DataSource = metodoPagoDatos.Listar();
            ddlMetodoPago.DataValueField = "IdMetodoPago";
            ddlMetodoPago.DataTextField = "Descripcion";
            ddlMetodoPago.DataBind();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "modal", "new bootstrap.Modal(document.getElementById('modalPago')).show();", true);
        }

        protected void btnConfirmarPago_Click(object sender, EventArgs e) {
            MetodoPagoDatos metodoPagoDatos = new MetodoPagoDatos();
            MetodoPago metodoPagoSeleccionado = metodoPagoDatos.ObtenerPorId(int.Parse(ddlMetodoPago.SelectedValue));

            Cuota cuota = cuotaDatos.ObtenerPorId(int.Parse(hfIdCuota.Value));

            cuotaServicio.pagarCuota(cuota, metodoPagoSeleccionado); // Al pagar la cuota, el metodo tambien valida si fue la ultima y en ese caso pasa el prestamo a finalizado.
            cargarDatosPrestamo();
            cargarGrilla();
        }



        protected void btnCancelarPrestamo_Click(object sender, EventArgs e) {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "modal", "new bootstrap.Modal(document.getElementById('modalCancelar')).show();", true);
        }


        protected void btnConfirmarCancelacion_Click(object sender, EventArgs e) {
            if (txtmotivocancelacion.Text != "") {
                Prestamo prestamo = prestamoDatos.ObtenerPorId(int.Parse(hfIdPrestamoActual.Value));
                Usuario usuario = (Usuario)Session["usuario"];
                string observacion = txtmotivocancelacion.Text;

                PrestamoServicio prestamoServicio = new PrestamoServicio();
                prestamoServicio.cancelar(prestamo, usuario, observacion);

                CuotaServicio cuotaServicio = new CuotaServicio();
                cuotaServicio.cancelarCuotasPendientesPorPrestamo(prestamo);

                cargarDatosPrestamo();
                cargarGrilla();
            }
        }


        private void cargarGrilla() {
            int idPrestamo = int.Parse(hfIdPrestamoActual.Value);
            List<Cuota> cuotas = cuotaDatos.ListarPorPrestamo(idPrestamo);
            List<CuotaDTO> cuotasVista = new List<CuotaDTO>();

            int numeroCuota = 1;
            foreach (Cuota c in cuotas) {
                cuotasVista.Add(new CuotaDTO {
                    IdCuota = c.IdCuota,
                    NumeroCuota = numeroCuota,
                    Monto = c.Monto,
                    FechaVencimiento = c.FechaVencimiento,
                    EstadoDescripcion = c.EstadoCuota.Descripcion,
                    EstadoCssClass = c.EstadoCuota.IdEstadoCuota == 2 ? "bg-success" : (c.EstadoCuota.IdEstadoCuota == 3) ? "bg-danger" : (c.EstadoCuota.IdEstadoCuota == 4) ? "bg-dark" : "bg-secondary",
                    PuedeRegistrarPago = c.EstadoCuota.IdEstadoCuota == 1 || c.EstadoCuota.IdEstadoCuota == 3
                });
                numeroCuota++;
            }

            dgvCuotas.DataSource = cuotasVista;
            dgvCuotas.DataBind();
        }



        private void cargarDatosPrestamo() {
            int idPrestamo = int.Parse(hfIdPrestamoActual.Value);
            Prestamo prestamo = prestamoDatos.ObtenerPorId(idPrestamo);

            lblIdPrestamo.Text = prestamo.IdPrestamo.ToString();
            lblCliente.Text = prestamo.Cliente.Username;
            lblProducto.Text = prestamo.ProductoPrestamo.Nombre;
            lblMontoTotal.Text = $"${prestamo.Monto.ToString("N2", new CultureInfo("es-AR"))}";
            lblCuotasRestantes.Text = prestamo.CuotasRestantes.ToString();
            lblEstadoPrestamo.Text = prestamo.EstadoPrestamo.Descripcion;

            if ( ((Usuario)Session["usuario"]).Rol.Descripcion == "Administrador" && prestamo.EstadoPrestamo.Descripcion == "En Curso") {
                btnCancelarPrestamo.Visible = true;

            } else btnCancelarPrestamo.Visible = false;
        }
    }
}