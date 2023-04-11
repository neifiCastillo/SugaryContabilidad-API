using System;
using System.Collections.Generic;

namespace SugaryContabilidad_API.Models
{
    public partial class ImagenesUsuario
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string? Path { get; set; }
        public bool Status { get; set; }
    }
}
