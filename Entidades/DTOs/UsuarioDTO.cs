using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.DTOs {
    public class UsuarioDTO {
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string Rol { get; set; }
        public string Estado { get; set; }
        public string EstadoCssClass { get; set; }
    }
}
