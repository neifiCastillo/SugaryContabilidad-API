using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class ProductosController : Controller
    {
        private readonly SugaryContabilidadDBContext SCC;
        OperationRequest OR = new OperationRequest();
        public ProductosController(SugaryContabilidadDBContext SCC )
        {
            this.SCC = SCC;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("GetProductos")]
        public async Task<IEnumerable<Producto>> GetProductos()
        {
            return await SCC.Productos.Where(x => x.Eliminado.Equals(false)).ToListAsync();
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("GetProductosById")]
        public async Task<ActionResult<Producto>> GetProductosById(int IdProducto)
        {
            var product = await SCC.Productos.Where(x => x.IdProducto == IdProducto && x.Eliminado.Equals(false)).ToListAsync();
            if (product is null) {
               return NotFound();
            }
            return Ok(product);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("PostProductos")]
        public async Task<IActionResult> PostProductos(Producto Producto)
        {
           bool product = SCC.Productos.Where(x => x.NombreProducto == Producto.NombreProducto).Count().Equals(0);
            if (!product)
            {
                OR.message = HttpResponseText.ExistProducto;
                OR.isSucess = false;
                return NotFound(OR);
            }
            SCC.Productos.Add(Producto);
            Producto.Eliminado = false;
            Producto.FechaCreacion = DateTime.Now;
            OR.message = HttpResponseText.CreateProducto;
            OR.isSucess = true;
            OR.Data = Producto;
            await SCC.SaveChangesAsync();
            //return CreatedAtAction(nameof(GetProductosById), new {id = Producto.IdProducto }, OR.Data);
            return Ok(OR);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("PutProductos")]
        public async Task<IActionResult> PutProductos(int IdProducto , Producto Producto)
        {
            if (IdProducto != Producto.IdProducto)
            {
                return BadRequest();
            }
            var ExistProduct = SCC.Productos.Find(IdProducto);
            if (ExistProduct is null)
            {
                OR.message = HttpResponseText.PutProductoNotFound;
                OR.isSucess = false;
                return NotFound(OR);
            }

            ExistProduct.NombreProducto = Producto.NombreProducto;
            ExistProduct.Descripcion = Producto.Descripcion;
            ExistProduct.CantidadDeProducto = Producto.CantidadDeProducto;
            ExistProduct.Estado = Producto.Estado;
            ExistProduct.PrecioCompra = Producto.PrecioCompra;
            ExistProduct.PrecioVenta = Producto.PrecioVenta;
            ExistProduct.FechaCompra = Producto.FechaCompra;
            ExistProduct.FechaVencimiento = Producto.FechaVencimiento;
            ExistProduct.Eliminado = false;
            ExistProduct.FechaCreacion = Producto.FechaCreacion;

            await SCC.SaveChangesAsync();
            OR.message = HttpResponseText.PutProducto;
            OR.isSucess = true;
            OR.Data = ExistProduct;
            return Ok(OR);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("PutProductosDelete")]
        public async Task<IActionResult> PutProductosDelete(int IdProducto)
        {
            var ExistProduct = SCC.Productos.Find(IdProducto);
            if (ExistProduct is null)
            {
                OR.message = HttpResponseText.PutProductoNotFound;
                OR.isSucess = false;
                return NotFound(OR);
            }
            ExistProduct.Eliminado = true;
            await SCC.SaveChangesAsync();
            OR.message = HttpResponseText.PutProductoDelete;
            OR.isSucess = true;
            OR.Data = ExistProduct.NombreProducto;
            return Ok(OR);
        }
    }
}
