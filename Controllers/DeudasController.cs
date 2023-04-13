using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Schema;
using SugaryContabilidad_API.DataContext;
using SugaryContabilidad_API.Models;
using SugaryContabilidad_API.Utils;

namespace SugaryContabilidad_API.Controllers
{

    //[Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class DeudasController : Controller
    {
        private readonly SugaryContabilidadDBContext SCC;
        OperationRequest OR = new OperationRequest();
        ValidationCedula VC = new ValidationCedula();
        public DeudasController(SugaryContabilidadDBContext SCC)
        {
            this.SCC = SCC;
        }

        //[Authorize(Policy = "Admin")]
        [HttpGet("GetDeudas")]
        public async Task<IEnumerable<Deuda>> GetDeudas()
        {
            return await SCC.Deudas.Where(x => x.Pagada.Equals(false) && x.FechaFinalDeuda.Equals(null) && x.FechaAporte.Equals(null) && x.Aportado.Equals(null)).ToListAsync();
        }

        //[Authorize(Policy = "Admin")]
       [HttpGet("GetDeudasById")]
        public async Task<IEnumerable<Deuda>> GetDeudasById(string NombreDeudor)
        {
           return await SCC.Deudas.Where(x => x.NombreDeudor == NombreDeudor && x.Pagada.Equals(false) && x.FechaFinalDeuda.Equals(null) && x.FechaAporte.Equals(null) && x.Aportado.Equals(null)).ToListAsync();
        }
        //[Authorize(Policy = "Admin")]
        [HttpPost("PostDeudas")]
        public async Task<IActionResult> PostDeudas(Deuda Deuda)
        {
            int maxTicketDeuda = 0;
            if (!SCC.Deudas.Any()){maxTicketDeuda = 1; } else { maxTicketDeuda = SCC.Deudas.Max(x => x.IdDeuda + 1);}
            bool deuda = SCC.Deudas.Where(x => x.TicketDeuda == Deuda.TicketDeuda).Count().Equals(0);
            if (!deuda)
            {
                OR.message = HttpResponseText.ExistDeudas;
                OR.isSucess = false;
                return NotFound(OR);
            }
            if (string.IsNullOrEmpty(Deuda.CedulaDeudor))
            {

                OR.message = HttpResponseText.NullCedulaDeudas;
                OR.isSucess = false;
                return NotFound(OR);
            }
            bool Cedula = VC.ValidaCedula(Deuda.CedulaDeudor);
            if (!Cedula)
            {
                OR.message = HttpResponseText.validationCedulaDeudas;
                OR.isSucess = false;
                return NotFound(OR);
            }
            Deuda.TicketDeuda = "SCD"+maxTicketDeuda;
            Deuda.FechaInicioDeuda = DateTime.Now;
            Deuda.CantidadDeudaEditable = Deuda.CantidadDeudaFijo;
            Deuda.Pagada = false;
            SCC.Deudas.Add(Deuda);
            OR.message = HttpResponseText.CreateDeudas;
            OR.isSucess = true;
            OR.Data = Deuda;
            await SCC.SaveChangesAsync();
            return Ok(OR);
        }

        //[Authorize(Policy = "Admin")]
        [HttpPost("PostDeudasAporte")]
        public async Task<IActionResult> PostDeudasAporte(Deuda Deudas)
        {
            var deudas = await SCC.Deudas.Where(x => x.TicketDeuda == Deudas.TicketDeuda).OrderByDescending(x => x.IdDeuda).FirstOrDefaultAsync();
            if (deudas is null)
            {
                OR.message = HttpResponseText.PutDeudasNotFound;
                OR.isSucess = false;
                return NotFound(OR);
            }
            if (deudas.CantidadDeudaEditable == 0)
            {
                OR.message = HttpResponseText.DeudaPagada;
                OR.isSucess = false;
                return NotFound(OR);
            }
            if (Deudas.Aportado > deudas.CantidadDeudaEditable)
            {
                OR.message = HttpResponseText.AporteMayorADeuda;
                OR.isSucess = false;
                return NotFound(OR);
            }
            deudas.IdDeuda = 0;
            deudas.FechaAporte = DateTime.Now;
            deudas.Aportado = Deudas.Aportado;
            int CantidadDeudaEditableUpdate = (deudas.CantidadDeudaEditable) - (Deudas.Aportado ?? 0);
            deudas.CantidadDeudaEditable = CantidadDeudaEditableUpdate;
            deudas.Pagada = false;
            SCC.Deudas.Add(deudas);
            await SCC.SaveChangesAsync();
            OR.message = HttpResponseText.PutDeudas;
            OR.isSucess = true;
            OR.Data = deudas;
            return Ok(OR);
        }

    //[Authorize(Policy = "Admin")]
    [HttpPut("PutDeudasDelete")]
        public async Task<IActionResult> PutDeudasDelete(string TicketDeuda)
        {
            var deudas = await SCC.Deudas.Where(x => x.TicketDeuda == TicketDeuda).OrderByDescending(x => x.IdDeuda).FirstOrDefaultAsync();
            if (deudas is null)
            {
                OR.message = HttpResponseText.PutDeudasNotFound;
                OR.isSucess = false;
                return NotFound(OR);
            }
            else if (deudas.CantidadDeudaEditable != 0){

                OR.message = HttpResponseText.PutDeudaNopagada;
                OR.isSucess = false;
                return NotFound(OR);
            }
            var deudasPagadas = SCC.Deudas.Where(x => x.TicketDeuda == deudas.TicketDeuda);
            foreach (var deuda in deudasPagadas)
            {
                deuda.Pagada = true;
                deuda.FechaFinalDeuda = DateTime.Now;
            }
            await SCC.SaveChangesAsync();
            OR.message = HttpResponseText.PutDeudaDelete;
            OR.isSucess = true;
            OR.Data = TicketDeuda;
            return Ok(OR);
        }
    }
}
