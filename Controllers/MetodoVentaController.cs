using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SugaryContabilidad_API.DataContext;
using SugaryContabilidad_API.Models;
using SugaryContabilidad_API.Utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace SugaryContabilidad_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class MetodoVentaController : Controller
    {
        private readonly SugaryContabilidadDBContext SCC;
        OperationRequest OR = new OperationRequest();
        ValidationCedula VC = new ValidationCedula();
        public MetodoVentaController(SugaryContabilidadDBContext SCC)
        {
            this.SCC = SCC;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("GetMetodoVenta")]
        public async Task<IEnumerable<MetodoVentum>> GetMetodoVenta()
        {
            return await SCC.MetodoVenta.Where(x => x.Status.Equals(true)).ToListAsync();
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("GetMetodoVentaById")]
        public async Task<ActionResult<MetodoVentum>> GetMetodoVentaById(int IdMetodo)
        {
            var Metodo = await SCC.MetodoVenta.Where(x => x.IdMetodo == IdMetodo && x.Status.Equals(true)).ToListAsync();
            if (Metodo is null)
            {
                return NotFound();
            }
            return Ok(Metodo);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("PostMetodoVenta")]
        public async Task<IActionResult> PostMetodoVenta(MetodoVentum MetodoVenta)
        {
            bool Metodo = SCC.MetodoVenta.Where(x => x.Metodo == MetodoVenta.Metodo || x.NumeroReferencia == MetodoVenta.NumeroReferencia).Count().Equals(0);
            if (!Metodo)
            {
                OR.message = HttpResponseText.ExistMetodoVenta;
                OR.isSucess = false;
                return NotFound(OR);
            }
            if (string.IsNullOrEmpty(MetodoVenta.CedulaReferencia)) {

                OR.message = HttpResponseText.NullCedulaMetodo;
                OR.isSucess = false;
                return NotFound(OR);
            }
            bool Cedula = VC.ValidaCedula(MetodoVenta.CedulaReferencia);
            if (!Cedula)
            {
                OR.message = HttpResponseText.validationCedulaMetodo;
                OR.isSucess = false;
                return NotFound(OR);
            }
            SCC.MetodoVenta.Add(MetodoVenta);
            MetodoVenta.Status = true;
            OR.message = HttpResponseText.CreateMetodoVenta;
            OR.isSucess = true;
            OR.Data = MetodoVenta;
            await SCC.SaveChangesAsync();
            return Ok(OR);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("PutMetodoVenta")]
        public async Task<IActionResult> PutMetodoVenta(int IdMetodo, MetodoVentum MetodoVenta)
        {
            if (IdMetodo != MetodoVenta.IdMetodo)
            {
                return BadRequest();
            }
            var ExistMetodo = SCC.MetodoVenta.Find(IdMetodo);
            if (ExistMetodo is null)
            {
                OR.message = HttpResponseText.PutMetodoNotFound;
                OR.isSucess = false;
                return NotFound(OR);
            }
            bool Cedula = VC.ValidaCedula(MetodoVenta.CedulaReferencia);
            if (!Cedula)
            {
                OR.message = HttpResponseText.validationCedulaMetodo;
                OR.isSucess = false;
                return NotFound(OR);
            }
            ExistMetodo.Metodo = MetodoVenta.Metodo;
            ExistMetodo.NumeroReferencia = MetodoVenta.NumeroReferencia;
            ExistMetodo.TipoReferencia = MetodoVenta.TipoReferencia;
            ExistMetodo.CedulaReferencia = MetodoVenta.CedulaReferencia;
            ExistMetodo.NombrePerteneceReferencia = MetodoVenta.NombrePerteneceReferencia;
            ExistMetodo.Color = MetodoVenta.Color;
            await SCC.SaveChangesAsync();
            OR.message = HttpResponseText.PutMetodo;
            OR.isSucess = true;
            OR.Data = ExistMetodo;
            return Ok(OR);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("PutMetodoVentaDelete")]
        public async Task<IActionResult> PutMetodoVentaDelete(int IdMetodo)
        {
            var ExistMetodo = SCC.MetodoVenta.Find(IdMetodo);
            if (ExistMetodo is null)
            {
                OR.message = HttpResponseText.PutMetodoNotFound;
                OR.isSucess = false;
                return NotFound(OR);
            }
            ExistMetodo.Status = false;
            await SCC.SaveChangesAsync();
            OR.message = HttpResponseText.PutMetodoDelete;
            OR.isSucess = true;
            OR.Data = ExistMetodo.Metodo;
            return Ok(OR);
        }
    }
}
