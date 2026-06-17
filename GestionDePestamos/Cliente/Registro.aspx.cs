using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;

namespace GestionDePestamos.Cliente {
    public partial class Registro : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void btnRegistrar_Click(object sender, EventArgs e) {
            Entidades.Cliente cliente = new Entidades.Cliente();

            cliente.Username = txtUsername.Text;
            cliente.Password = txtPassword.Text;
            cliente.Email = txtEmail.Text;
            cliente.Direccion = txtDireccion.Text;
            cliente.Telefono = txtTelefono.Text;

            // Seguir cuando Ricky implemente metodo Agregar() en ClienteDatos.
        }
    }
}