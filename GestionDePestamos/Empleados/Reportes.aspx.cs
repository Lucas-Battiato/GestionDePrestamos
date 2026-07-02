using Entidades;
using Entidades.DTOs;
using Negocio.Datos;
using Newtonsoft.Json.Linq;
using SendGrid.Helpers.Mail;
using Servicios;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionDePestamos.Empleados {
    public partial class Reportes : System.Web.UI.Page {
        PrestamoDatos prestamoDatos = new PrestamoDatos();
        CuotaDatos cuotaDatos = new CuotaDatos();
        

        protected void Page_Load(object sender, EventArgs e) {

            if ((Usuario)Session["usuario"] == null) {
                Response.Redirect("~/Inicio.aspx");
            }

            // Si el usuario no es administrador lo mando de nuevo a la pantalla de Empleados
            if (((Usuario)Session["usuario"]).Rol.Descripcion != "Administrador") {
                Response.Redirect("~/Empleados/Empleados.aspx");
            }

            if (!IsPostBack) {
                cargarDatos();
                lblResultadoNotificacion.Visible = false;
            }

            if (IsPostBack) {
                string tab = hfTabActiva.Value;
                if (!string.IsNullOrEmpty(tab)) {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "tab",
                        $"var t = document.querySelector('[data-bs-target=\"#{tab}\"]'); if(t) new bootstrap.Tab(t).show();", true);
                }
            }


        }




        private List<decimal> calcularGanancia() {
            // Calculo monto total prestado
            List<Prestamo> listaPrestamos = prestamoDatos.Listar();
            decimal montoTotalPrestado = 0;
            listaPrestamos.ForEach(p => montoTotalPrestado += p.Monto);

            //Calculo monto total devuelto.
            List<Cuota> listaCuotas = cuotaDatos.Listar();
            decimal montoTotalDevuelto = 0;
            listaCuotas.ForEach(c => {
                if (c.EstadoCuota.Descripcion == "Pagada") {
                    montoTotalDevuelto += c.Monto;
                }
            });

            // Calculo ganancias/perdidas
            decimal total = montoTotalPrestado - montoTotalDevuelto;

            List<decimal> datos = new List<decimal>();
            datos.Add(montoTotalPrestado);
            datos.Add(montoTotalDevuelto);
            datos.Add(total);

            return datos;
        }


        private void cargarDatos() {

            // Cargo datos en Cards de KPIs
            lblKpiPrestamosActivos.Text = prestamoDatos.contarEnCurso().ToString();
            lblKpiCuotasVencidas.Text = cuotaDatos.contarVencidas().ToString();

            List<decimal> datos = calcularGanancia();
            lblKpiBalance.Text = "$" + datos[2].ToString("N2", new CultureInfo("es-AR")); ; // En posicion 2 tengo el total
            lblKpiBalanceDetalle.Text = $"<strong>Total prestado:</strong> ${datos[0].ToString("N2", new CultureInfo("es-AR"))} | <strong>Total devuelto:</strong> ${datos[1].ToString("N2", new CultureInfo("es-AR"))}"; // En posicion 0 tengo el totalPrestado y en 1 el totalDevuelto



            // Cargo datos en Tab PrestamosActivos
            List<Prestamo> prestamosEnCurso = prestamoDatos.Listar().FindAll(p => p.EstadoPrestamo.Descripcion == "En Curso"); // Filtro prestamos En Curso

            List<PrestamoActivoDTO> prestamosActivosDTOs = new List<PrestamoActivoDTO>();
            prestamosEnCurso.ForEach(p => {
                prestamosActivosDTOs.Add(new PrestamoActivoDTO {
                    IdPrestamo = p.IdPrestamo,
                    UsernameCliente = p.Cliente.Username,
                    NombreProducto = p.ProductoPrestamo.Nombre,
                    Monto = p.Monto,
                    InteresTotal = p.InteresTotal,
                    CuotasTotales = p.CantidadCuotas,
                    CuotasRestantes = p.CuotasRestantes,
                    FechaAprobacion = p.FechaAprobacion,
                    AprobadoPor = p.UsuarioAprobador.Username
                });
            });

            dgvPrestamosActivos.DataSource = prestamosActivosDTOs;
            dgvPrestamosActivos.DataBind();



            // Cargo datos en Tab CuotasVencidas
            dgvCuotasVencidas.DataSource = cargarCuotasVencidas();
            dgvCuotasVencidas.DataBind();
        }


        private List<CuotaVencidaDTO> cargarCuotasVencidas() {
            List<Cuota> cuotasVencidas = cuotaDatos.Listar().FindAll(c => c.EstadoCuota.Descripcion == "Vencida"); // Filtro cuotas vencidas

            List<CuotaVencidaDTO> cuotasVencidasDTOs = new List<CuotaVencidaDTO>();
            cuotasVencidas.ForEach(c => {
                cuotasVencidasDTOs.Add(new CuotaVencidaDTO {
                    IdCuota = c.IdCuota,
                    IdPrestamo = c.Prestamo.IdPrestamo,
                    UsernameCliente = prestamoDatos.ObtenerPorId(c.Prestamo.IdPrestamo).Cliente.Username,
                    Monto = c.Monto,
                    FechaVencimiento = c.FechaVencimiento,
                    DiasVencida = (DateTime.Today - c.FechaVencimiento).Days
                });
            });

            return cuotasVencidasDTOs;

        }



        protected void btnNotificarVencidas_Click(object sender, EventArgs e) {
            hfTabActiva.Value = "tabVencidas";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "modal", "new bootstrap.Modal(document.getElementById('modalConfirmarNotificacion')).show();", true);
        }

        protected void btnConfirmarNotificacion_Click(object sender, EventArgs e) {
            // Genero un listado de cuotasVencidasDTO para tener los ID y poder generar un listado de cuotas (no DTO). El MailServicio necesita el objeto Cuota, no el DTO.
            List<CuotaVencidaDTO> listaCuotasVencidasDTO = cargarCuotasVencidas();
            List<Cuota> listaCuotasVencidas = new List<Cuota>();

            listaCuotasVencidasDTO.ForEach(cuotaDTO => {
                listaCuotasVencidas.Add(cuotaDatos.ObtenerPorId(cuotaDTO.IdCuota));
            });

            CuotaServicio cuotaServicio = new CuotaServicio();
            int correosEnviados = cuotaServicio.envioMailsVencimientos(listaCuotasVencidas);

            lblResultadoNotificacion.Text = $"Se enviaron {correosEnviados} avisos de vencimiento";
            lblResultadoNotificacion.Visible = true;
        }
    }
}