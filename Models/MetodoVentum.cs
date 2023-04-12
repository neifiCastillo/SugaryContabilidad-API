using System;
using System.Collections.Generic;

namespace SugaryContabilidad_API.Models
{
    public partial class MetodoVentum
    {
        public int IdMetodo { get; set; }
        public string Metodo { get; set; } = null!;
        public string NumeroReferencia { get; set; } = null!;
        public string TipoReferencia { get; set; } = null!;
        public string CedulaReferencia { get; set; } = null!;
        public string? NombrePerteneceReferencia { get; set; }
        public string? Color { get; set; }
        public bool Status { get; set; }
    }
}
