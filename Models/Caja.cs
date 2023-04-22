using System;
using System.Collections.Generic;

namespace SugaryContabilidad_API.Models
{
    public partial class Caja
    {
        public int IdCaja { get; set; }
        public string NombreCaja { get; set; } = null!;
        public int CantidadCaja { get; set; }
        public bool EstatusCaja { get; set; }
        public DateTime FechaCreacionCaja { get; set; }
        public DateTime FechaCierreCaja { get; set; }
        public bool Cerrada { get; set; }
    }
}
