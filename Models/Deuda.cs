using System;
using System.Collections.Generic;

namespace SugaryContabilidad_API.Models
{
    public partial class Deuda
    {
        public int IdDeuda { get; set; }
        public string TicketDeuda { get; set; } = null!;
        public string? CedulaDeudor { get; set; }
        public string? NombreDeudor { get; set; }
        public string? DescripcionDeuda { get; set; }
        public int CantidadDeudaFijo { get; set; }
        public int CantidadDeudaEditable { get; set; }
        public DateTime FechaInicioDeuda { get; set; }
        public DateTime? FechaFinalDeuda { get; set; }
        public DateTime? FechaAporte { get; set; }
        public int? Aportado { get; set; }
        public bool Pagada { get; set; }
    }
}
