using Entidades;
using Negocio.Datos;
using System;
using System.Globalization;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionDePestamos.Empleados {
    public partial class TasasInteres : System.Web.UI.Page {

        ProductoPrestamoDatos productoPrestamoDatos = new ProductoPrestamoDatos();
        TasaInteresDatos tasaInteresDatos = new TasaInteresDatos();

        protected void Page_Load(object sender, EventArgs e) {

            if ((Usuario)Session["usuario"] == null) {
                Response.Redirect("~/Inicio.aspx");
            }

            // Si el usuario no es administrador lo mando de nuevo a la pantalla de Empleados
            if (((Usuario)Session["usuario"]).Rol.Descripcion != "Administrador") {
                Response.Redirect("~/Empleados/Empleados.aspx");
            }

            if (!IsPostBack) {
                ddlFiltroProducto.DataSource = productoPrestamoDatos.Listar();
                ddlFiltroProducto.DataValueField = "IdProducto";
                ddlFiltroProducto.DataTextField = "Nombre";
                ddlFiltroProducto.DataBind();

                ddlFiltroProducto.Items.Insert(0, new ListItem("Todos los productos", "0"));

                CargarGrilla();

            }

        }

        protected void btnNuevo_Click(object sender, EventArgs e) {
            lblModalTitulo.Text = "Nueva Tasa de Interes";
            hfIdTasa.Value = "0";
            txtCuotasDesde.Text = "";
            txtCuotasHasta.Text = "";
            txtTasaMensual.Text = "";

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "modal",
                "new bootstrap.Modal(document.getElementById('modalTasa')).show();", true);

            ddlProducto.DataSource = productoPrestamoDatos.Listar();
            ddlProducto.DataValueField = "IdProducto";
            ddlProducto.DataTextField = "Nombre";
            ddlProducto.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e) {
            if (validarCamposNuevaTasa()) {
                ProductoPrestamo productoSeleccionado = new ProductoPrestamo { IdProducto = int.Parse(ddlProducto.SelectedValue) };
                int cuotasDesde = int.Parse(txtCuotasDesde.Text);
                int cuotasHasta = int.Parse(txtCuotasHasta.Text);
                decimal tasaMensual = decimal.Parse(txtTasaMensual.Text.Replace(",", "."), CultureInfo.InvariantCulture);

                TasaInteres tasaInteres = new TasaInteres {
                    ProductoPrestamo = productoSeleccionado,
                    CuotasDesde = cuotasDesde,
                    CuotasHasta = cuotasHasta,
                    TasaMensual = tasaMensual
                };

                tasaInteresDatos.agregar(tasaInteres);
                Response.Redirect("~/Empleados/TasasInteres.aspx");
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e) {

        }

        protected void btnEliminar_Click(object sender, EventArgs e) {
            LinkButton boton = (LinkButton)sender;
            GridViewRow fila = (GridViewRow)boton.NamingContainer;
            int id = int.Parse(dgvTasas.DataKeys[fila.RowIndex].Value.ToString()); //Tomo el ID

            TasaInteres tasaInteres = new TasaInteres() { IdTasaInteres = id };

            tasaInteresDatos.eliminar(tasaInteres);
            CargarGrilla();
        }

        protected void ddlFiltroProducto_SelectedIndexChanged(object sender, EventArgs e) {

            int idProductoSeleccionado = int.Parse(ddlFiltroProducto.SelectedValue);
            if (idProductoSeleccionado == 0) {
                CargarGrilla();

            } else {
                dgvTasas.DataSource = tasaInteresDatos.ListarPorProducto(idProductoSeleccionado);
                dgvTasas.DataBind();
            }
        }

        private void CargarGrilla() {
            dgvTasas.DataSource = tasaInteresDatos.Listar();
            dgvTasas.DataBind();

        }


        // Valido los campos del modal de nueva tasa de interes y devuelvo false si alguna validación falla.
        private bool validarCamposNuevaTasa() {
            bool flag = true;

            // Campos vacios
            if (txtCuotasDesde.Text.Trim() == "" || txtCuotasHasta.Text.Trim() == "" || txtTasaMensual.Text.Trim() == "") {
                lblError.Text = "Por favor, complete todos los campos";
                flag = false;
            }

            // Valor negativo o algun caracter no numerico.
            // En txtTasaMensual reemplazo , con . para evitar errores con decimales.
            if (!decimal.TryParse(txtTasaMensual.Text.Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal tasaMensual) || tasaMensual < 0
                    || !int.TryParse(txtCuotasDesde.Text, out int cuotasDesde) || cuotasDesde < 0
                        || !int.TryParse(txtCuotasHasta.Text, out int cuotasHasta) || cuotasHasta < 0) {
                lblError.Text = "Por favor, ingrese solo numeros positivos en la tasaMensual.";
                flag = false;
            }

            return flag;
        }
    }
}