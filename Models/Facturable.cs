using System;
using System.Collections.Generic;

namespace SugaryContabilidad_API.Models
{
    public partial class Facturable
    {
        public int IdFactura { get; set; }
        public string TicketFactura { get; set; } = null!;
        public string CategoriaFactura { get; set; } = null!;
        public int CantidadFactura { get; set; }
        public DateTime FechaFactura { get; set; }
        public int? IdProducto { get; set; }
        public string? NombreProducto { get; set; }
        public int? IdVenta { get; set; }
        public string? TicketVenta { get; set; }
        public int? IdDeuda { get; set; }
        public string? CedulaDeudor { get; set; }
        public string? NombreDeuda { get; set; }
        public int? Aportado { get; set; }
        public int CantidadDeudaFijo { get; set; }
        public DateTime? FechaAporte { get; set; }
    }
}
