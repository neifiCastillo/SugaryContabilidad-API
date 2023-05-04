using System;
using System.Collections.Generic;

namespace SugaryContabilidad_API.Models
{
    public partial class EstadoProducto
    {
        public int IdEstado { get; set; }
        public string Estado { get; set; } = null!;
        public bool Eliminado { get; set; }
    }
}
