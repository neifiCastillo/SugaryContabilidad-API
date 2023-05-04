using System;
using System.Collections.Generic;

namespace SugaryContabilidad_API.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Pwd { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UsuarioType { get; set; } = null!;
        public DateTime RegFeha { get; set; }
        public bool? Eliminado { get; set; }
    }
}
