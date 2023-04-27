using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SugaryContabilidad_API.DataContext;
using SugaryContabilidad_API.Models;
using SugaryContabilidad_API.Services;
using SugaryContabilidad_API.Utils;

namespace SugaryContabilidad_API.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class CajaController : Controller
    {
        private readonly SugaryContabilidadDBContext SCC;
        private readonly FacturablesServices FS;
        OperationRequest OR = new OperationRequest();
        public CajaController(SugaryContabilidadDBContext SCC, FacturablesServices FS)
        {
            this.SCC = SCC;
            this.FS = FS;
        }


        [Authorize(Policy = "Admin")]
        [HttpGet("GetCaja")]
        public async Task<IEnumerable<Caja>> GetCaja()
        {
            return await SCC.Cajas.Where(x => x.Cerrada.Equals(false)).ToListAsync();
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("PostCaja")]
        public async Task<IActionResult> PostCaja(Caja caja)
        {
            bool box = SCC.Cajas.Where(x => x.IdCaja == caja.IdCaja).Count().Equals(0);
            if (!box)
            {
                OR.message = HttpResponseText.ExistCaja;
                OR.isSucess = false;
                return NotFound(OR);
            }
            bool cajasSinCerrar = await SCC.Cajas.AnyAsync(x => !x.Cerrada);
            if (cajasSinCerrar)
            {
                OR.message = HttpResponseText.CajasSinCerrar;
                OR.isSucess = false;
                return NotFound(OR);
            }
            caja.NombreCaja = caja.NombreCaja;
            caja.CantidadCajaFijo = caja.CantidadCajaFijo;
            caja.CantidadCajaEditable = caja.CantidadCajaFijo;
            caja.FechaCreacionCaja = DateTime.Now;
            caja.FechaCierreCaja = caja.FechaCierreCaja;
            caja.Cerrada = false;
            OR.message = HttpResponseText.CreateCaja;
            OR.isSucess = true;
            OR.Data = caja;
            SCC.Cajas.Add(caja);
            await SCC.SaveChangesAsync();
            return Ok(OR);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("PutCaja")]
        public async Task<IActionResult> PutCaja(int IdCaja, Caja caja)
        {
            if (IdCaja != caja.IdCaja)
            {
                return BadRequest();
            }
            var ExistCaja = SCC.Cajas.Find(IdCaja);
            if (ExistCaja is null)
            {
                OR.message = HttpResponseText.PutCajaNotFound;
                OR.isSucess = false;
                return NotFound(OR);
            }
            ExistCaja.NombreCaja = caja.NombreCaja;
            //---------------------------------------
            ExistCaja.CantidadCajaFijo = ExistCaja.CantidadCajaFijo;
            ExistCaja.CantidadCajaEditable = ExistCaja.CantidadCajaEditable;
            ExistCaja.FechaCreacionCaja = ExistCaja.FechaCreacionCaja;
            ExistCaja.FechaCierreCaja = ExistCaja.FechaCierreCaja;
            ExistCaja.Cerrada = false;
            await SCC.SaveChangesAsync();
            OR.message = HttpResponseText.PutCaja;
            OR.isSucess = true;
            OR.Data = ExistCaja;
            return Ok(OR);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("PutCajaDelete")]
        public async Task<IActionResult> PutCajaDelete(int IdCaja)
        {
            var ExistCaja = SCC.Cajas.Find(IdCaja);
            if (ExistCaja is null)
            {
                OR.message = HttpResponseText.PutCajaNotFound;
                OR.isSucess = false;
                return NotFound(OR);
            }
            ExistCaja.FechaCierreCaja = DateTime.Now;
            ExistCaja.Cerrada = true;
            await SCC.SaveChangesAsync();
            OR.message = HttpResponseText.PutCajaDelete;
            OR.isSucess = true;
            OR.Data = ExistCaja.NombreCaja;
            return Ok(OR);
        }
    }
}
