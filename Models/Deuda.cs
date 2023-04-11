using System;
using System.Collections.Generic;

namespace SugaryContabilidad_API.Models
{
    public partial class Deuda
    {
        public int IdDeuda { get; set; }
        public string CedulaDeudor { get; set; } = null!;
        public string NombreDeuda { get; set; } = null!;
        public int CantidadDeudaFijo { get; set; }
        public int CantidadDeudaEditable { get; set; }
        public DateTime FechaInicioDeuda { get; set; }
        public DateTime? FechaFinalDeuda { get; set; }
        public DateTime? FechaAporte { get; set; }
        public int? Aportado { get; set; }
        public bool Pagada { get; set; }
    }
}
