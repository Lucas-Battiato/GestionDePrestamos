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
        Usuario usuario = new Usuario();
        UsuarioDTO usuarioVista = new UsuarioDTO();

        protected void Page_Load(object sender, EventArgs e) {
            if ((Usuario)Session["usuario"] == null) {
                Response.Redirect("~/Inicio.aspx");
            }

            // Si el usuario no es administrador lo mando de nuevo a la pantalla de Empleados
            if (((Usuario)Session["usuario"]).Rol.Descripcion != "Administrador") {
                Response.Redirect("~/Empleados/Empleados.aspx");
            }

            if (!IsPostBack) {
                lblContaseña.Text = "";

                cargarGrilla();
            }
        }



        protected void btnEditar_Click(object sender, EventArgs e) {
            LinkButton button = (LinkButton)sender;
            GridViewRow fila = (GridViewRow)button.NamingContainer;
            int idUsuario = int.Parse(dgvUsuarios.DataKeys[fila.RowIndex].Value.ToString());

            usuario = usuarioDatos.ObtenerPorId(idUsuario);
            usuarioVista.IdUsuario = usuario.IdUsuario;
            usuarioVista.Username = usuario.Username;
            usuarioVista.Rol = usuario.Rol.Descripcion;
            usuarioVista.Estado = usuario.Activo ? "Activo" : "Inactivo";

            cargarModal(usuarioVista);
        }

        protected void btnToggleActivo_Click(object sender, EventArgs e) {
            LinkButton button = (LinkButton)sender;
            GridViewRow fila = (GridViewRow)button.NamingContainer;
            int idUsuario = int.Parse(dgvUsuarios.DataKeys[fila.RowIndex].Value.ToString());

            string estadoUsuario = (usuarioDatos.ObtenerPorId(idUsuario).Activo) ? "Activo" : "Inactivo";

            if (estadoUsuario == "Activo") usuarioDatos.desactivar(idUsuario);
            else usuarioDatos.activar(idUsuario);

            cargarGrilla();
        }

        protected void btnGuardar_Click(object sender, EventArgs e) {
            // Si el hidden field esta vacio es porque estoy cargando un nuevo usuario.
            if (hfIdUsuario.Value == "") {
                bool flagValidacion = true;

                // Valido nombre de usuario
                if (txtUsername.Text.Trim() != "") {
                    txtUsername.Style.Remove("Border-color");
                    flagValidacion = true;

                } else {
                    txtUsername.Style.Add("Border-color", "red");
                    flagValidacion = false;
                }

                // Valido contraseña
                if (txtPassword.Text.Trim() != "") {
                    txtPassword.Style.Remove("Border-color");
                    flagValidacion = true;

                } else {
                    txtPassword.Style.Add("Border-color", "red");
                    flagValidacion = false;
                }


                if (flagValidacion) {
                    Usuario nuevoUsuario = new Usuario();
                    nuevoUsuario.Username = txtUsername.Text.Trim();
                    nuevoUsuario.Password = txtPassword.Text.Trim();
                    nuevoUsuario.Rol = new Rol { IdRol = int.Parse(ddlRol.SelectedValue), Descripcion = ddlRol.SelectedItem.Text };
                    nuevoUsuario.Activo = true;

                    usuarioDatos.Agregar(nuevoUsuario);

                }


            // Si el hidden filed tiene valor es porque estoy modificadndo un usuario existente.
            } else if (hfIdUsuario.Value != "") {
                bool flagValidacion = true;

                // Valido nombre de usuario
                if (txtUsername.Text.Trim() != "") {
                    txtUsername.Style.Remove("Border-color");
                    flagValidacion = true;

                } else {
                    txtUsername.Style.Add("Border-color", "red");
                    flagValidacion = false;
                }

                if (flagValidacion) {
                    Usuario usuarioModificado = new Usuario();
                    usuarioModificado.IdUsuario = int.Parse(hfIdUsuario.Value);
                    usuarioModificado.Username = txtUsername.Text.Trim();
                    // Si se dejo el campo de password vacio, dejo la contraseña actual. Sino pongo la que se haya ingresado.
                    usuarioModificado.Password = (txtPassword.Text.Trim() == "") ? usuarioDatos.ObtenerPorId(int.Parse(hfIdUsuario.Value)).Password : txtPassword.Text.Trim();
                    usuarioModificado.Rol = new Rol { IdRol = int.Parse(ddlRol.SelectedValue), Descripcion = ddlRol.SelectedItem.Text };
                    // Mantengo el mismo estado.
                    usuarioModificado.Activo = usuarioDatos.ObtenerPorId(int.Parse(hfIdUsuario.Value)).Activo;
                    usuarioDatos.Modificar(usuarioModificado);

                }
            }

            cargarGrilla();
        }

        protected void btnNuevo_Click(object sender, EventArgs e) {
            cargarModal();
        }


        protected void txtFiltro_TextChanged(object sender, EventArgs e) {
            List<Usuario> listaUsuarios = usuarioDatos.Listar();
            List<UsuarioDTO> listaUsuariosDTO = new List<UsuarioDTO>();

            listaUsuarios = listaUsuarios.FindAll(usuario => usuario.IdUsuario.ToString().ToUpper().Contains(txtFiltro.Text.ToUpper())
                                           || usuario.Username.ToUpper().Contains(txtFiltro.Text.ToUpper())
                                           || usuario.Rol.Descripcion.ToUpper().Contains(txtFiltro.Text.ToUpper())
                                           || (usuario.Activo ? "Activo" : "Inactivo").ToUpper().Contains(txtFiltro.Text.ToUpper()));

            listaUsuarios.ForEach(usuario => {
                listaUsuariosDTO.Add(new UsuarioDTO {
                    IdUsuario = usuario.IdUsuario,
                    Username = usuario.Username,
                    Rol = usuario.Rol.Descripcion,
                    Estado = (usuario.Activo ? "Activo" : "Inactivo"),
                    EstadoCssClass = usuario.Activo ? "bg-success" : "bg-danger",
                    TextoBotonToggle = usuario.Activo ? "Desactivar" : "Activar",
                    TogglecCssClass = usuario.Activo ? "btn btn-sm btn-outline-danger" : "btn btn-sm btn-outline-success"
                });
            });


            dgvUsuarios.DataSource = listaUsuariosDTO;
            dgvUsuarios.DataBind();

        }



        private void cargarGrilla() {
            List<Usuario> usuarios = usuarioDatos.Listar();
            List<UsuarioDTO> usuariosDTO = new List<UsuarioDTO>();

            foreach (Usuario usuario in usuarios) {
                usuariosDTO.Add(new UsuarioDTO {
                    IdUsuario = usuario.IdUsuario,
                    Username = usuario.Username,
                    Rol = usuario.Rol.Descripcion,
                    Estado = usuario.Activo ? "Activo" : "Inactivo",
                    EstadoCssClass = usuario.Activo ? "bg-success" : "bg-danger",
                    TextoBotonToggle = usuario.Activo ? "Desactivar" : "Activar",
                    TogglecCssClass = usuario.Activo ? "btn btn-sm btn-outline-danger" : "btn btn-sm btn-outline-success"
                });
            }

            dgvUsuarios.DataSource = usuariosDTO;
            dgvUsuarios.DataBind();
        }


        private void cargarModal() {

            lblModalTitulo.Text = "Nuevo usuario";

            hfIdUsuario.Value = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            lblContaseña.Text = "";

            RolDatos rolDatos = new RolDatos();

            ddlRol.DataSource = rolDatos.Listar();
            ddlRol.DataValueField = "IdRol";
            ddlRol.DataTextField = "Descripcion";
            ddlRol.SelectedIndex = 1;
            ddlRol.DataBind();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "modal", "new bootstrap.Modal(document.getElementById('modalUsuario')).show();", true);

        }


        private void cargarModal(UsuarioDTO usuarioDTO) {

            lblModalTitulo.Text = "Editar usuario";

            hfIdUsuario.Value = usuarioDTO.IdUsuario.ToString();

            txtUsername.Text = usuarioDTO.Username;

            txtPassword.Text = "";
            lblContaseña.Text = "Dejar en blanco para no modificar la contraseña actual al editar.";

            RolDatos rolDatos = new RolDatos();
            ddlRol.DataSource = rolDatos.Listar();
            ddlRol.DataValueField = "IdRol";
            ddlRol.DataTextField = "Descripcion";
            ddlRol.SelectedIndex = (usuarioDTO.Rol == "Administrador" ? 0 : 1);
            ddlRol.DataBind();

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "modal", "new bootstrap.Modal(document.getElementById('modalUsuario')).show();", true);

        }
    }
}