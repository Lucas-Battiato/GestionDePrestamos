using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades {
    internal class Usuario {
        public int IdUsuario { get; set; }
        public Rol Rol { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Activo { get; set; }
    }
}
