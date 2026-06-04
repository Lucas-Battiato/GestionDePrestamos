using System;

namespace Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public Rol Rol { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Activo { get; set; }
    }
}
