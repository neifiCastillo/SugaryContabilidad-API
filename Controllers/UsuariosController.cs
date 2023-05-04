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
    public class UsuariosController : Controller
    {
        private readonly SugaryContabilidadDBContext SCC;
        OperationRequest OR = new OperationRequest();
        public UsuariosController(SugaryContabilidadDBContext SCC)
        {
            this.SCC = SCC;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("GetUsuarios")]
        public async Task<IEnumerable<Usuario>> GetUsuarios()
        {
            return await SCC.Usuarios.Where(x => x.Eliminado.Equals(false)).ToListAsync();
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("GetUsuariosId")]
        public async Task<IEnumerable<Usuario>> GetUsuariosId(string NombreUsuario)
        {
            return await SCC.Usuarios.Where(x => x.NombreUsuario == NombreUsuario && x.Eliminado.Equals(false)).ToListAsync();
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("PostUsuarios")]
        public async Task<IActionResult> PostUsuarios(Usuario usuario)
        {
            bool user = SCC.Usuarios.Where(x => x.IdUsuario == usuario.IdUsuario).Count().Equals(0);
            if (!user)
            {
                OR.message = HttpResponseText.ExistUsuario;
                OR.isSucess = false;
                return NotFound(OR);
            }
            bool email = SCC.Usuarios.Where(x => x.NombreUsuario == usuario.NombreUsuario || x.Email == usuario.Email ).Count().Equals(0);
            if (!email)
            {
                OR.message = HttpResponseText.ExistUsuario;
                OR.isSucess = false;
                return NotFound(OR);
            }
            usuario.NombreUsuario = usuario.NombreUsuario;
            usuario.Pwd = usuario.Pwd;
            usuario.Email = usuario.Email;
            usuario.UsuarioType = usuario.UsuarioType;
            usuario.RegFeha = DateTime.Now;
            usuario.Eliminado = false;
            SCC.Usuarios.Add(usuario);
            OR.message = HttpResponseText.CreateUsuario;
            OR.isSucess = true;
            OR.Data = usuario;
            await SCC.SaveChangesAsync();
            return Ok(OR);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("PutUsuarios")]
        public async Task<IActionResult> PutUsuarios(int IdUsuario, Usuario usuario)
        {
            if (IdUsuario != usuario.IdUsuario)
            {
                return BadRequest();
            }
            var ExistUsuario = SCC.Usuarios.Find(IdUsuario);
            if (ExistUsuario is null)
            {
                OR.message = HttpResponseText.PutUsuarioNotFound;
                OR.isSucess = false;
                return NotFound(OR);
            }
            ExistUsuario.NombreUsuario = usuario.NombreUsuario;
            ExistUsuario.Pwd = usuario.Pwd;
            ExistUsuario.Email = usuario.Email;
            ExistUsuario.UsuarioType = usuario.UsuarioType;
            ExistUsuario.RegFeha = ExistUsuario.RegFeha;
            ExistUsuario.Eliminado = false;
            await SCC.SaveChangesAsync();
            OR.message = HttpResponseText.PutUsuario;
            OR.isSucess = true;
            OR.Data = ExistUsuario;
            return Ok(OR);
        }


        [Authorize(Policy = "Admin")]
        [HttpPut("PutUsuariosDelete")]
        public async Task<IActionResult> PutUsuariosDelete(string Email)
        {
            var ExistUsuario = await SCC.Usuarios.Where(x => x.Email == Email).FirstOrDefaultAsync();
            if (ExistUsuario is null)
            {
                OR.message = HttpResponseText.PutUsuarioNotFound;
                OR.isSucess = false;
                return NotFound(OR);
            }
            ExistUsuario.Eliminado = true;
            await SCC.SaveChangesAsync();
            OR.message = HttpResponseText.PutUsuarioDelete;
            OR.isSucess = true;
            OR.Data = ExistUsuario;
            return Ok(OR);
        }
    }
}
