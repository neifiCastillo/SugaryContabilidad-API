using System;
using System.Collections.Generic;

namespace SugaryContabilidad_API.Models
{
    public partial class Producto
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int CantidadDeProducto { get; set; }
        public string Estado { get; set; } = null!;
        public int PrecioCompra { get; set; }
        public int PrecioVenta { get; set; }
        public DateTime FechaCompra { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public bool Eliminado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
