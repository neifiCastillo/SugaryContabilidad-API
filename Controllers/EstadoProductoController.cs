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
    public class EstadoProductoController : Controller
    {
        private readonly SugaryContabilidadDBContext SCC;
        OperationRequest OR = new OperationRequest();
        public EstadoProductoController(SugaryContabilidadDBContext SCC)
        {
            this.SCC = SCC;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("GetEstadoProducto")]
        public async Task<IEnumerable<EstadoProducto>> GetEstadoProducto()
        {
            return await SCC.EstadoProductos.Where(x => x.Eliminado.Equals(false)).ToListAsync();
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("GetEstadoProductoById")]
        public async Task<ActionResult<EstadoProducto>> GetEstadoProductoById(int IdEstado)
        {
            var Estado = await SCC.EstadoProductos.Where(x => x.IdEstado == IdEstado && x.Eliminado.Equals(false)).ToListAsync();
            if (Estado is null)
            {
                return NotFound();
            }
            return Ok(Estado);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("PostEstadoProducto")]
        public async Task<IActionResult> PostEstadoProducto(EstadoProducto EstadoProducto)
        {
            bool Estado = SCC.EstadoProductos.Where(x => x.Estado == EstadoProducto.Estado).Count().Equals(0);
            if (!Estado)
            {
                OR.message = HttpResponseText.ExistEstadoProducto;
                OR.isSucess = false;
                return NotFound(OR);
            }
            SCC.EstadoProductos.Add(EstadoProducto);
            EstadoProducto.Eliminado = false;
            OR.message = HttpResponseText.CreateEstadoProducto;
            OR.isSucess = true;
            OR.Data = EstadoProducto;
            await SCC.SaveChangesAsync();
            return Ok(OR);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("PutEstadoProducto")]
        public async Task<IActionResult> PutEstadoProducto(int IdEstado, EstadoProducto EstadoProducto)
        {
            if (IdEstado != EstadoProducto.IdEstado)
            {
                return BadRequest();
            }
            var ExistEstado = SCC.EstadoProductos.Find(IdEstado);
            if (ExistEstado is null)
            {
                OR.message = HttpResponseText.PutEstadoNotFound;
                OR.isSucess = false;
                return NotFound(OR);
            }
            ExistEstado.Estado = EstadoProducto.Estado;
            await SCC.SaveChangesAsync();
            OR.message = HttpResponseText.PutEstado;
            OR.isSucess = true;
            OR.Data = ExistEstado;
            return Ok(OR);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("PutEstadoProductoDelete")]
        public async Task<IActionResult> PutEstadoProductoDelete(int IdEstado)
        {
            var ExistEstado = SCC.EstadoProductos.Find(IdEstado);
            if (ExistEstado is null)
            {
                OR.message = HttpResponseText.PutEstadoNotFound;
                OR.isSucess = false;
                return NotFound(OR);
            }
            ExistEstado.Eliminado = true;
            await SCC.SaveChangesAsync();
            OR.message = HttpResponseText.PutEstadoDelete;
            OR.isSucess = true;
            OR.Data = ExistEstado.Estado;
            return Ok(OR);
        }
    }
}
