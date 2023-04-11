using Microsoft.AspNetCore.Mvc;
using SugaryContabilidad_API.DataContext;
using SugaryContabilidad_API.Dto;
using SugaryContabilidad_API.Services;
using SugaryContabilidad_API.Utils;
using SugaryContabilidad_API.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace SugaryContabilidad_API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LoginController : Controller
    {
        private readonly LoginService LS;

        private IConfiguration config;
        public LoginController(LoginService LS, IConfiguration config)
        {
            this.LS = LS;
            this.config = config;
        }

        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate(UsuarioDto userDto)
         {
            var user = await LS.GetUsuario(userDto);

            if (user is null) {
                return BadRequest(new { message = "Credenciales Invalidas." });
            }

            string jwtToken = GenerateToken(user);

            return Ok(new { token = jwtToken });
        }

        private string GenerateToken(Usuario user) {

          var claims = new[]
          {
              new Claim(ClaimTypes.Name, user.NombreUsuario),
              new Claim(ClaimTypes.Email, user.Email),
              new Claim("UsuarioType", user.UsuarioType)
          };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken(
                                claims: claims,
                                expires: DateTime.Now.AddMinutes(60),
                                signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;

        }

    }
}
