using Entidades;
using Entidades.DTOs;
using Negocio.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GestionDePestamos.Empleados {
    public partial class Usuarios : System.Web.UI.Page {
        UsuarioDatos usuarioDatos = new UsuarioDatos();

        protected void Page_Load(object sender, EventArgs e) {
            if ((Usuario)Session["usuario"] == null) {
                Response.Redirect("~/Inicio.aspx");
            }

            // Si el usuario no es administrador lo mando de nuevo a la pantalla de Empleados
            if ( ((Usuario)Session["usuario"]).Rol.Descripcion != "Administrador" ) {
                Response.Redirect("~/Empleados/Empleados.aspx");
            }

            if (!IsPostBack) {

                cargarGrilla();
            }
        }

        //protected void btnBuscar_Click(object sender, EventArgs e) {

        //}

        //protected void btnLimpiar_Click(object sender, EventArgs e) {

        //}

        private void cargarGrilla() {
            List<Usuario> usuarios = usuarioDatos.Listar();
            List<UsuarioDTO> usuariosDTO = new List<UsuarioDTO>();

            foreach (Usuario usuario in usuarios) {
                usuariosDTO.Add(new UsuarioDTO {
                    IdUsuario = usuario.IdUsuario,
                    Username = usuario.Username,
                    Rol = usuario.Rol.Descripcion,
                    Estado = usuario.Activo ? "Activo" : "Inactivo",
                    EstadoCssClass = usuario.Activo ? "bg-success" : "bg-danger"
                });
            }

            dgvUsuarios.DataSource = usuariosDTO;
            dgvUsuarios.DataBind();
        }
    }
}