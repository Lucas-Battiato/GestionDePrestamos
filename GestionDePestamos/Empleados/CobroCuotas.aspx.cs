using Entidades;
using Entidades.DTOs;
using Negocio.Datos;
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
            listaPrestamos.Clear();

            // Chequeo si es un ID de prestamo
            if (int.TryParse(txtBusqueda.Text, out int idPrestamo)) {
                listaPrestamos.Add(prestamoDatos.ObtenerPorId(idPrestamo));


                // Si no es un ID de prestamo, entonces es un nombre de usuario
            } else {
                if (txtBusqueda.Text.Trim() != "") {
                    ClienteDatos clienteDatos = new ClienteDatos();
                    Entidades.Cliente cliente = clienteDatos.ObtenerPorUsername(txtBusqueda.Text);

                    if (cliente != null) {
                        listaPrestamos = prestamoDatos.ListarPorCliente(cliente.IdCliente);
                    }
                }
            }

            // Si encontre un prestamo con ese ID o nombre de usuario, habilito el panel de prestamo. Sino lo informo con el lblSinResultado
            // Cargo las cuotas del prestamo encontrado
            if (listaPrestamos.Count != 0 && listaPrestamos[0] != null) {
                lblIdPrestamo.Text = listaPrestamos[0].IdPrestamo.ToString();
                lblCliente.Text = listaPrestamos[0].Cliente.Username;
                lblProducto.Text = listaPrestamos[0].ProductoPrestamo.Nombre;
                lblMontoTotal.Text = $"${listaPrestamos[0].Monto.ToString("N2", new CultureInfo("es-AR"))}";
                lblCuotasRestantes.Text = listaPrestamos[0].CuotasRestantes.ToString();

                lblSinResultados.Visible = false;
                pnlPrestamo.Visible = true;

                
                CuotaDatos cuotaDatos = new CuotaDatos();
                List<Cuota> cuotas = cuotaDatos.ListarPorPrestamo(idPrestamo);
                List<CuotaDTO> cuotasVista = new List<CuotaDTO>();

                int numeroCuota = 1;
                foreach (Cuota cuota in cuotas) {
                    cuotasVista.Add(new CuotaDTO {
                        IdCuota = cuota.IdCuota,
                        NumeroCuota = numeroCuota,
                        Monto = cuota.Monto,
                        FechaVencimiento = cuota.FechaVencimiento,
                        EstadoDescripcion = cuota.EstadoCuota.Descripcion,
                        EstadoCssClass = cuota.EstadoCuota.IdEstadoCuota == 2 ? "bg-success" : (cuota.EstadoCuota.IdEstadoCuota == 3 ? "bg-danger" : "bg-secondary"),
                        PuedeRegistrarPago = cuota.EstadoCuota.IdEstadoCuota != 2 // no pagada
                    });
                    numeroCuota++;
                }

                dgvCuotas.DataSource = cuotasVista;
                dgvCuotas.DataBind();

            } else {
                pnlPrestamo.Visible = false;
                lblSinResultados.Visible = true;
            }

        }

        protected void btnRegistrarPago_Click(object sender, EventArgs e) {
        }

        protected void btnConfirmarPago_Click(object sender, EventArgs e) {
        }
    }
}