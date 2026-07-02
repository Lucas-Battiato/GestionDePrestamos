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
using Newtonsoft.Json;
using System.Linq;

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

                hfTabActiva.Value = "tabBalance";
                string tab = hfTabActiva.Value;
                if (!string.IsNullOrEmpty(tab)) {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "tab",
                        $"var t = document.querySelector('[data-bs-target=\"#{tab}\"]'); if(t) new bootstrap.Tab(t).show();", true);
                }

            }

            if (IsPostBack) {
                hfTabActiva.Value = "tabVencidas";
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



            // Cargo datos en Tab Balance
                // Cargo widget de Prestamos
            List<Prestamo> listaPrestamos = prestamoDatos.Listar();
            List<Prestamo> solicitados = listaPrestamos.FindAll(p => p.EstadoPrestamo.Descripcion == "Solicitado");
            List<Prestamo> aprobados = listaPrestamos.FindAll(p => p.EstadoPrestamo.Descripcion == "Aprobado");
            List<Prestamo> rechazados = listaPrestamos.FindAll(p => p.EstadoPrestamo.Descripcion == "Rechazado");
            List<Prestamo> enCurso = listaPrestamos.FindAll(p => p.EstadoPrestamo.Descripcion == "En Curso");
            List<Prestamo> finalizados = listaPrestamos.FindAll(p => p.EstadoPrestamo.Descripcion == "Finalizado");
            List<Prestamo> cancelados = listaPrestamos.FindAll(p => p.EstadoPrestamo.Descripcion == "Cancelado");

            hfDatosPrestamos.Value = JsonConvert.SerializeObject(new {
                labels = new[] { "Solicitado", "Aprobado", "Rechazado", "En Curso", "Finalizado", "Cancelado" },
                data = new[] { solicitados.Count(), aprobados.Count(), rechazados.Count(), enCurso.Count(), finalizados.Count(), cancelados.Count() }
            });


                // Cargo widget de Cuotas
            List<Cuota> listaCuotas = cuotaDatos.Listar();
            List<Cuota> pendientes = listaCuotas.FindAll(c => c.EstadoCuota.Descripcion == "Pendiente");
            List<Cuota> vencidas = listaCuotas.FindAll(c => c.EstadoCuota.Descripcion == "Vencida");
            List<Cuota> pagadas = listaCuotas.FindAll(c => c.EstadoCuota.Descripcion == "Pagada");
            List<Cuota> canceladas = listaCuotas.FindAll(c => c.EstadoCuota.Descripcion == "Cancelada");

            hfDatosCuotas.Value = JsonConvert.SerializeObject(new {
                labels = new[] { "Pendientes", "Vencidas", "Pagadas", "Canceladas" },
                data = new[] { pendientes.Count(), vencidas.Count(), pagadas.Count(), canceladas.Count() }
            });


                // Cargo widget de Balance
            int solicitadosPersonal = listaPrestamos.Count(p => p.ProductoPrestamo.Nombre == "Prestamo Personal" && p.EstadoPrestamo.Descripcion == "Solicitado");
            int aprobadosPersonal = listaPrestamos.Count(p => p.ProductoPrestamo.Nombre == "Prestamo Personal" && p.EstadoPrestamo.Descripcion == "Aprobado");
            int rechazadosPersonal = listaPrestamos.Count(p => p.ProductoPrestamo.Nombre == "Prestamo Personal" && p.EstadoPrestamo.Descripcion == "Rechazado");
            int enCursoPersonal = listaPrestamos.Count(p => p.ProductoPrestamo.Nombre == "Prestamo Personal" && p.EstadoPrestamo.Descripcion == "En Curso");
            int finalizadosPersonal = listaPrestamos.Count(p => p.ProductoPrestamo.Nombre == "Prestamo Personal" && p.EstadoPrestamo.Descripcion == "Finalizado");
            int canceladosPersonal = listaPrestamos.Count(p => p.ProductoPrestamo.Nombre == "Prestamo Personal" && p.EstadoPrestamo.Descripcion == "Cancelado");

            int solicitadosPrendario = listaPrestamos.Count(p => p.ProductoPrestamo.Nombre == "Prestamo Prendario" && p.EstadoPrestamo.Descripcion == "Solicitado");
            int aprobadosPrendario = listaPrestamos.Count(p => p.ProductoPrestamo.Nombre == "Prestamo Prendario" && p.EstadoPrestamo.Descripcion == "Aprobado");
            int rechazadosPrendario = listaPrestamos.Count(p => p.ProductoPrestamo.Nombre == "Prestamo Prendario" && p.EstadoPrestamo.Descripcion == "Rechazado");
            int enCursoPrendario = listaPrestamos.Count(p => p.ProductoPrestamo.Nombre == "Prestamo Prendario" && p.EstadoPrestamo.Descripcion == "En Curso");
            int finalizadosPrendario = listaPrestamos.Count(p => p.ProductoPrestamo.Nombre == "Prestamo Prendario" && p.EstadoPrestamo.Descripcion == "Finalizado");
            int canceladosPrendario = listaPrestamos.Count(p => p.ProductoPrestamo.Nombre == "Prestamo Prendario" && p.EstadoPrestamo.Descripcion == "Cancelado");

            int solicitadosHipotecario = listaPrestamos.Count(p => p.ProductoPrestamo.Nombre == "Prestamo Hipotecario" && p.EstadoPrestamo.Descripcion == "Solicitado");
            int aprobadosHipotecario = listaPrestamos.Count(p => p.ProductoPrestamo.Nombre == "Prestamo Hipotecario" && p.EstadoPrestamo.Descripcion == "Aprobado");
            int rechazadosHipotecario = listaPrestamos.Count(p => p.ProductoPrestamo.Nombre == "Prestamo Hipotecario" && p.EstadoPrestamo.Descripcion == "Rechazado");
            int enCursoHipotecario = listaPrestamos.Count(p => p.ProductoPrestamo.Nombre == "Prestamo Hipotecario" && p.EstadoPrestamo.Descripcion == "En Curso");
            int finalizadosHipotecario = listaPrestamos.Count(p => p.ProductoPrestamo.Nombre == "Prestamo Hipotecario" && p.EstadoPrestamo.Descripcion == "Finalizado");
            int canceladosHipotecario = listaPrestamos.Count(p => p.ProductoPrestamo.Nombre == "Prestamo Hipotecario" && p.EstadoPrestamo.Descripcion == "Cancelado");


            hfDatosBarras.Value = JsonConvert.SerializeObject(new {
                labels = new[] { "Personal", "Prendario", "Hipotecario" },
                datasets = new[] {
                        new { label = "Solicitado",  backgroundColor = "#0dcaf0", data = new[] { solicitadosPersonal,  solicitadosPrendario,  solicitadosHipotecario  } },
                        new { label = "Aprobado",    backgroundColor = "#0d6efd", data = new[] { aprobadosPersonal,    aprobadosPrendario,    aprobadosHipotecario    } },
                        new { label = "Rechazado",   backgroundColor = "#dc3545", data = new[] { rechazadosPersonal,   rechazadosPrendario,   rechazadosHipotecario   } },
                        new { label = "En Curso",    backgroundColor = "#6f42c1", data = new[] { enCursoPersonal,      enCursoPrendario,      enCursoHipotecario      } },
                        new { label = "Finalizado",  backgroundColor = "#198754", data = new[] { finalizadosPersonal,  finalizadosPrendario,  finalizadosHipotecario  } },
                        new { label = "Cancelado",   backgroundColor = "#6c757d", data = new[] { canceladosPersonal,   canceladosPrendario,   canceladosHipotecario   } }
    }
            });

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