using Microsoft.EntityFrameworkCore;
using SugaryContabilidad_API.DataContext;
using SugaryContabilidad_API.Dto;
using SugaryContabilidad_API.Models;
using SugaryContabilidad_API.Utils;

namespace SugaryContabilidad_API.Services
{
    public class LoginService
    {
        private readonly SugaryContabilidadDBContext SCC;
        public LoginService(SugaryContabilidadDBContext SCC)
        {
            this.SCC = SCC;
        }

        public async Task<Usuario?> GetUsuario(UsuarioDto userDto)
        {

            return await SCC.Usuarios.SingleOrDefaultAsync(x => x.Email == userDto.Email && x.Pwd == userDto.Pwd);

        }
    }
}
