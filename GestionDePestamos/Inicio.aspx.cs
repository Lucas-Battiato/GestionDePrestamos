using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Negocio.Datos;

namespace GestionDePestamos {
    public partial class Inicio : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void btnIngresar_Click(object sender, EventArgs e) {
            Entidades.Cliente cliente = new Entidades.Cliente();
            ClienteDatos clienteDatos = new ClienteDatos();
            bool flagValidacion = true;

            // Valido nombre de usuario
            if (txtUsuario.Text.Trim() != "") {
                cliente.Username = txtUsuario.Text.Trim();
                lblErrorUsername.Text = "";

            } else {
                lblErrorUsername.Text = "Debe ingresar un nombre de usuario";
                flagValidacion = false;
            }


            // Valido contraseña
            if (txtPassword.Text.Trim() != "") {
                cliente.Password = txtPassword.Text.Trim();
                lblErrorPassword.Text = "";

            } else {
                lblErrorPassword.Text = "Debe ingresar una contraseña";
                flagValidacion = false;
            }

            if (flagValidacion) {
                Entidades.Cliente clienteValidado = clienteDatos.ObtenerPorUsername(cliente.Username);

                if (clienteValidado != null && clienteValidado.Password.Equals(cliente.Password)) {
                    Session.Add("cliente", clienteValidado);
                    Response.Redirect("~/Cliente/Clientes.aspx");

                } else {
                    lblErrorLogin.Text = "Usuario o contraseña incorrecta. Intente de nuevo";
                }

            }

        }
    }
}