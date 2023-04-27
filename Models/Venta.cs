using System;
using System.Collections.Generic;

namespace SugaryContabilidad_API.Models
{
    public partial class Venta
    {
        public int IdVenta { get; set; }
        public string TicketVenta { get; set; } = null!;
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = null!;
        public int CantidadProductoVendido { get; set; }
        public int PrecioVenta { get; set; }
        public string? MetodoVenta { get; set; }
        public string EstadoProducto { get; set; } = null!;
        public int CantidadPrecio { get; set; }
        public DateTime FechaVenta { get; set; }
        public bool Status { get; set; }
    }
}
