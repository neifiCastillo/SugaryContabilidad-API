using System;
using System.Collections.Generic;

namespace SugaryContabilidad_API.Models
{
    public partial class ImagenesProducto
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public string? Path { get; set; }
        public bool Status { get; set; }
    }
}
