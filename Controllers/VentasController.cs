using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SugaryContabilidad_API.DataContext;
using SugaryContabilidad_API.Models;
using SugaryContabilidad_API.Utils;

namespace SugaryContabilidad_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class VentasController : Controller
    {
        private readonly SugaryContabilidadDBContext SCC;
        OperationRequest OR = new OperationRequest();
        public VentasController(SugaryContabilidadDBContext SCC)
        {
            this.SCC = SCC;
        }


        [Authorize(Policy = "Admin")]
        [HttpGet("GetVentas")]
        public async Task<IEnumerable<Venta>> GetVentas()
        {
            return await SCC.Ventas.Where(x => x.Status.Equals(true)).ToListAsync();
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("GetVentasById")]
        public async Task<ActionResult<Venta>> GetVentasById(string TicketVenta)
        {
            var Venta = await SCC.Ventas.Where(x => x.TicketVenta == TicketVenta && x.Status.Equals(true)).ToListAsync();
            if (Venta is null)
            {
                return NotFound();
            }
            return Ok(Venta);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("PostVentas")]
        public async Task<IActionResult> PostVentas(List<Venta> VentasList)
        {
            int maxTicketVenta = 0;
            if (!SCC.Ventas.Any()) { maxTicketVenta = 1; } else { maxTicketVenta = SCC.Ventas.Max(x => x.IdVenta + 1);}
            foreach (Venta Ventas in VentasList)
            {
                bool venta = SCC.Ventas.Where(x => x.TicketVenta == Ventas.TicketVenta).Count().Equals(0);
                if (!venta)
                {
                    OR.message = HttpResponseText.ExistVenta;
                    OR.isSucess = false;
                    return NotFound(OR);
                }
                ////////////////
                var Productos = await SCC.Productos.FirstOrDefaultAsync(x => x.IdProducto == Ventas.IdProducto);
                if (Productos is null)
                {
                    OR.message = HttpResponseText.ExistProductoVenta;
                    OR.isSucess = false;
                    return NotFound(OR);
                }
                else if (Ventas.CantidadProductoVendido > Productos.CantidadDeProducto)
                {
                    OR.message = HttpResponseText.CantidadProducto;
                    OR.isSucess = false;
                    return NotFound(OR);
                }
                else
                {
                    int CantidadProductoUpdate = Productos.CantidadDeProducto - Ventas.CantidadProductoVendido;
                    await UpdateCantidadProducto(Productos.IdProducto, CantidadProductoUpdate);
                }
                //////////////////////
                Ventas.TicketVenta = "SCV"+maxTicketVenta;
                Ventas.Status = true;
                Ventas.CantidadPrecio = Ventas.CantidadProductoVendido * Ventas.PrecioVenta;
                Ventas.FechaVenta = DateTime.Now;
                SCC.Ventas.Add(Ventas);
            }
            await SCC.SaveChangesAsync();
            OR.message = HttpResponseText.CreateVenta;
            OR.isSucess = true;
            OR.Data = VentasList;
            return Ok(OR);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("PutVentasDelete")]
        public async Task<IActionResult> PutVentasDelete(string TicketVenta)
        {
            var ExistVenta = SCC.Ventas.Where(x => x.TicketVenta == TicketVenta);
            if (!ExistVenta.Any())
            {
                OR.message = HttpResponseText.ExistProductoVenta;
                OR.isSucess = false;
                return NotFound(OR);
            }
            foreach (var venta in ExistVenta)
            {
                venta.Status = false;
            }
            await SCC.SaveChangesAsync();
            OR.message = HttpResponseText.PutVentaDelete;
            OR.isSucess = true;
            OR.Data = TicketVenta;
            return Ok(OR);
        }
        private async Task<IActionResult> UpdateCantidadProducto(int IdProducto, int CantidadProductoUpdate)
        {
            var producto = SCC.Productos.Find(IdProducto);
            if (producto == null)
            {
                OR.message = HttpResponseText.ExistProductoVenta;
                OR.isSucess = false;
                return NotFound(OR);
            }
            producto.CantidadDeProducto = CantidadProductoUpdate;
            await SCC.SaveChangesAsync();
            return Ok();
        }
    }
}
