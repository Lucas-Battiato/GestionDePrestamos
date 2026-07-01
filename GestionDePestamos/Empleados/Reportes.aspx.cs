using Entidades;
using Negocio.Datos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;

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
                lblKpiPrestamosActivos.Text = prestamoDatos.contarEnCurso().ToString();
                lblKpiCuotasVencidas.Text = cuotaDatos.contarVencidas().ToString();

                List<decimal> datos = calcularGanancia();
                lblKpiBalance.Text = datos[2].ToString("N2", new CultureInfo("es-AR")); ; // En posicion 2 tengo el total
                lblKpiBalanceDetalle.Text = $"<strong>Total prestado:</strong> ${datos[0].ToString("N2", new CultureInfo("es-AR"))} | <strong>Total devuelto:</strong> ${datos[1].ToString("N2", new CultureInfo("es-AR"))}"; // En posicion 0 tengo el totalPrestado y en 1 el totalDevuelto
            }


        }

        protected void btnNotificarVencidas_Click(object sender, EventArgs e) {
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

    }
}