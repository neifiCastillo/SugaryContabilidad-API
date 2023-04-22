using SugaryContabilidad_API.DataContext;
using SugaryContabilidad_API.Dto;
using SugaryContabilidad_API.Models;

namespace SugaryContabilidad_API.Services
{
    public class FacturablesServices
    {
        private readonly SugaryContabilidadDBContext SCC;
        public FacturablesServices(SugaryContabilidadDBContext SCC)
        {
            this.SCC = SCC;
        }
        public async Task<Facturable> CreateFacturaVenta(Facturable factura)
        {
            int maxTickeFactura = 0;
            if (!SCC.Facturables.Any()) { maxTickeFactura = 1; } else { maxTickeFactura = SCC.Facturables.Max(x => x.IdFactura + 1); }
            if (factura is null)
            {
                throw new ArgumentException("La factura es nulo");
            }
            var facturaVenta = new Facturable
            {
                TicketFactura = "SCF" + maxTickeFactura,
                CategoriaFactura = "Venta",
                FechaFactura = DateTime.Now,
                CantidadFactura = factura.CantidadFactura,
                TicketVenta = factura.TicketVenta,
                NombreProducto = factura.NombreProducto,
                CantidadProductoVendido = factura.CantidadProductoVendido,
                PrecioVenta = factura.PrecioVenta,
                MetodoVenta = factura.MetodoVenta,
                EstadoProducto = factura.EstadoProducto,
            };
            await SCC.Facturables.AddAsync(facturaVenta);
            await SCC.SaveChangesAsync();
            return factura;
        }

        public async Task<Facturable> CreateFacturaDeuda(Facturable factura)
        {
            int maxTickeFactura = 0;
            if (!SCC.Facturables.Any()) { maxTickeFactura = 1; } else { maxTickeFactura = SCC.Facturables.Max(x => x.IdFactura + 1); }
            if (factura is null)
            {
                throw new ArgumentException("La factura es nulo");
            }
            var facturaVenta = new Facturable
            {
                TicketFactura = "SCF" + maxTickeFactura,
                CategoriaFactura = "Deuda",
                FechaFactura = DateTime.Now,
                CantidadFactura = factura.CantidadFactura,
                TicketDeuda = factura.TicketDeuda,
                CedulaDeudor = factura.CedulaDeudor,
                NombreDeudor = factura.NombreDeudor,
                Aportado = factura.Aportado,
                SumaDeAporte = factura.SumaDeAporte
            };
            await SCC.Facturables.AddAsync(facturaVenta);
            await SCC.SaveChangesAsync();
            return factura;
        }

        public async Task<Facturable> AporteFacturaDeuda(Facturable factura)
        {
            int maxTickeFactura = 0;
            if (!SCC.Facturables.Any()) { maxTickeFactura = 1; } else { maxTickeFactura = SCC.Facturables.Max(x => x.IdFactura + 1); }
            if (factura is null)
            {
                throw new ArgumentException("La factura es nulo");
            }
            var facturaVenta = new Facturable
            {
                TicketFactura = "SCF" + maxTickeFactura,
                CategoriaFactura = "Deuda Aporte",
                FechaFactura = DateTime.Now,
                CantidadFactura = factura.CantidadFactura,
                TicketDeuda = factura.TicketDeuda,
                CedulaDeudor = factura.CedulaDeudor,
                NombreDeudor = factura.NombreDeudor,
                Aportado = factura.Aportado,
                SumaDeAporte = factura.SumaDeAporte
            };
            await SCC.Facturables.AddAsync(facturaVenta);
            await SCC.SaveChangesAsync();
            return factura;
        }
    }
}
