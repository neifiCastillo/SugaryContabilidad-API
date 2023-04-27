using System;
using System.Collections.Generic;

namespace SugaryContabilidad_API.Models
{
    public partial class Facturable
    {
        public int IdFactura { get; set; }
        public int IdCaja { get; set; }
        public string TicketFactura { get; set; } = null!;
        public string CategoriaFactura { get; set; } = null!;
        public int CantidadFactura { get; set; }
        public DateTime FechaFactura { get; set; }
        public string? TicketVenta { get; set; }
        public string? NombreProducto { get; set; }
        public string? CantidadProductoVendido { get; set; }
        public string? PrecioVenta { get; set; }
        public string? MetodoVenta { get; set; }
        public string? EstadoProducto { get; set; }
        public bool? VentaEliminada { get; set; }
        public string? TicketDeuda { get; set; }
        public string? CedulaDeudor { get; set; }
        public string? NombreDeudor { get; set; }
        public int? Aportado { get; set; }
        public int? SumaDeAporte { get; set; }
    }
}
