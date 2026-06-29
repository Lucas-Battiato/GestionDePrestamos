using Entidades;
using Negocio.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionDePestamos {
    public partial class LoginEmpleado : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void btnIngresar_Click(object sender, EventArgs e) {
            Usuario usuario = new Usuario();
            UsuarioDatos usuarioDatos = new UsuarioDatos();
            
            bool flagValidacion = true;

            // Valido nombre de usuario
            if (txtUsuario.Text.Trim() != "") {
                usuario.Username = txtUsuario.Text.Trim();
                lblErrorUsername.Text = "";

            } else {
                lblErrorUsername.Text = "Debe ingresar un nombre de usuario";
                flagValidacion = false;
            }


            // Valido contraseña
            if (txtPassword.Text.Trim() != "") {
                usuario.Password = txtPassword.Text.Trim();
                lblErrorPassword.Text = "";

            } else {
                lblErrorPassword.Text = "Debe ingresar una contraseña";
                flagValidacion = false;
            }

            if (flagValidacion) {
                Usuario usuarioValidado = usuarioDatos.BuscarPorUsername(usuario.Username);

                if (usuarioValidado != null && usuarioValidado.Password.Equals(usuario.Password)) {
                    Session.Add("usuario", usuarioValidado);
                    Response.Redirect("~/Empleados/Empleados.aspx");

                } else {
                    lblErrorLogin.Text = "Usuario o contraseña incorrecta. Intente de nuevo";
                }

            }

        }
    }
}