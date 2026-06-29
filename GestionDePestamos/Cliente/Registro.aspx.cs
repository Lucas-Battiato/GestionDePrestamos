using Entidades;
using Negocio.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionDePestamos.Cliente {
    public partial class Registro : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {


        }

        protected void btnRegistrar_Click(object sender, EventArgs e) {
            Entidades.Cliente cliente = new Entidades.Cliente();
            ClienteDatos clienteDatos = new ClienteDatos();
            bool flagValidacion = true;

            // Valido nombre de usuario
            if (txtUsername.Text.Trim() != "") {

                if (clienteDatos.ObtenerPorUsername(txtUsername.Text) == null) {
                    cliente.Username = txtUsername.Text.Trim();
                    lblErrorUsername.CssClass = "text-success small";
                    lblErrorUsername.Text = "Buenas noticias! Nombre de usuario disponible.";

                } else {
                    lblErrorUsername.CssClass = "text-danger small";
                    lblErrorUsername.Text = "El nombre de usuario ya se encuentra registrado";
                    flagValidacion = false;
                }

            } else {
                lblErrorUsername.CssClass = "text-danger small";
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



            // Valido email
            if (System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) {

                if (clienteDatos.ObtenerPorEmail(txtEmail.Text) == null) {
                    cliente.Email = txtEmail.Text.Trim();
                    lblErrorEmail.CssClass = "text-success small";
                    lblErrorEmail.Text = "Buenas noticias! Correo electronico disponible.";

                } else {
                    lblErrorEmail.CssClass = "text-danger small";
                    lblErrorEmail.Text = "El correo electronico ya se encuentra registrado";
                    flagValidacion = false;
                }

            } else {
                lblErrorEmail.CssClass = "text-danger small";
                lblErrorEmail.Text = "Formato de correo electrónico incorrecto";
                flagValidacion = false;
            }



            // Valido Telefono
            if (txtTelefono.Text.Trim() != "") {

                if (int.TryParse(txtTelefono.Text.Trim(), out int tel)) {
                    cliente.Telefono = tel.ToString();
                    lblErrorTelefono.Text = "";

                } else {
                    lblErrorTelefono.Text = "Ingrese solo numeros";
                    flagValidacion = false;
                }

            } else {
                lblErrorTelefono.Text = "Debe ingresar una telefono";
                flagValidacion = false;
            }



            // Valido Dirección
            if (txtDireccion.Text.Trim() != "") {
                cliente.Direccion = txtDireccion.Text.Trim();
                lblErrorDireccion.Text = "";

            } else {
                lblErrorDireccion.Text = "Debe ingresar una dirección";
                flagValidacion = false;
            }


            if (flagValidacion) {
                int idClienteGenerado = clienteDatos.Agregar(cliente);
            
                if (idClienteGenerado != null) {
                    Session.Add("cliente", clienteDatos.ObtenerPorId(idClienteGenerado));
                    Response.Redirect("~/Cliente/Clientes.aspx");
                }
            }
            

        }
    }
}