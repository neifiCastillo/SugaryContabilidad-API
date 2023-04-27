using Microsoft.EntityFrameworkCore;
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
            var caja = await SCC.Cajas.FirstOrDefaultAsync(x => x.Cerrada == false);
            if (caja == null)
            {
                throw new ArgumentException("No se encontró ninguna caja abierta.");
            }
            // Actualizar la cantidad de la caja
            caja.CantidadCajaEditable = caja.CantidadCajaEditable + factura.CantidadFactura;
            var facturaVenta = new Facturable
            {
                IdCaja = caja.IdCaja,
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
                VentaEliminada = false
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
            var caja = await SCC.Cajas.FirstOrDefaultAsync(x => x.Cerrada == false);
            if (caja == null)
            {
                throw new ArgumentException("No se encontró ninguna caja abierta.");
            }
            var facturaDeuda = new Facturable
            {
                IdCaja = caja.IdCaja,
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
            await SCC.Facturables.AddAsync(facturaDeuda);
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
            var caja = await SCC.Cajas.FirstOrDefaultAsync(x => x.Cerrada == false);
            if (caja == null)
            {
                throw new ArgumentException("No se encontró ninguna caja abierta.");
            }
            // Actualizar la cantidad de la caja
            caja.CantidadCajaEditable = caja.CantidadCajaEditable + (factura.Aportado ?? 0);
            var facturaDeudaAporte = new Facturable
            {
                IdCaja = caja.IdCaja,
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
            await SCC.Facturables.AddAsync(facturaDeudaAporte);
            await SCC.SaveChangesAsync();
            return factura;
        }

        public async Task<Facturable> DeleteCantdadVendidaVenta(string TicketVenta)
        {
            var factura = await SCC.Facturables.FirstOrDefaultAsync(x => x.TicketVenta == TicketVenta);
            if (factura == null)
            {
                throw new ArgumentException("No se encontró esta factura.");
            }
            var caja = await SCC.Cajas.FirstOrDefaultAsync(x => x.Cerrada == false);
            if (caja == null)
            {
                throw new ArgumentException("No se encontró ninguna caja abierta.");
            }
            // Actualizar la cantidad de la caja
            factura.VentaEliminada = true;
            caja.CantidadCajaEditable = caja.CantidadCajaEditable - factura.CantidadFactura;
            await SCC.SaveChangesAsync();
            return factura;
        }
    }
}
