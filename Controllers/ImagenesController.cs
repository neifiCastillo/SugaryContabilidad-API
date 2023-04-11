using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SugaryContabilidad_API.Models;
using SugaryContabilidad_API.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using SugaryContabilidad_API.Dto;
using SugaryContabilidad_API.Utils;
using SugaryContabilidad_API.DataContext;

namespace SugaryContabilidad_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class ImagenesController : Controller
    {
        private readonly SugaryContabilidadDBContext SCC;
        private readonly ImagenesService IS;
        OperationRequest OR = new OperationRequest();
        public ImagenesController(ImagenesService IS, SugaryContabilidadDBContext SCC) 
        {
            this.IS = IS;
            this.SCC = SCC;
        }

        //imagenes de usuarios

        [Authorize(Policy = "Admin")]
        [HttpGet("GetImagenesUsuarios")]
        public async Task<IReadOnlyCollection<ImagenesUsuario>> GetImagenesUsuarios()
        {
            return await IS.RunImagenUsuario();
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("GetImagenesUsuariosById")]
        public async Task<IReadOnlyCollection<ImagenesUsuario>> GetImagenesUsuariosById(int IdUsuario)
        {
            return await IS.RunImagenUsuarioById(IdUsuario);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("PostImagenesUsuarios")]
        public async Task<ImagenesUsuario> PostImagenesUsuarios(ImageRequetsDto requets)
        {
            return await IS.CreateImagenUsuario(requets);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("PutImagenesUsuarios")]
        public async Task<ImagenesUsuario> PutImagenUsuario(int IdUsuario, ImageRequetsDto request)
        {
            var result = await IS.UpdateImagenUsuario(IdUsuario, request);
            return result;

        }

        [Authorize(Policy = "Admin")]
        [HttpPut("PutImagenesUsuariosDelete")]
        public async Task<ImagenesUsuario> PutImagenesUsuariosDelete(int IdUsuario)
        {

            var result = await IS.UpdateImagenUsuarioDelete(IdUsuario);
            return result;

        }

        //Imagenes de productos

        [Authorize(Policy = "Admin")]
        [HttpGet("GetImagenesProductos")]
        public async Task<IReadOnlyCollection<ImagenesProducto>> GetImagenesProductos()
        {
            return await IS.RunImagenProductos();
        }

        [Authorize(Policy = "Admin")]
        [HttpGet("GetImagenesProductosById")]
        public async Task<IReadOnlyCollection<ImagenesProducto>> GetImagenesProductosById(int IdProducto)
        {
            return await IS.RunImagenProductosById(IdProducto);
        }

        [Authorize(Policy = "Admin")]
        [HttpPost("PostImagenesProductos")]
        public async Task<ImagenesProducto> PostImagenesProductos(ImageRequetsDto requets)
        {
            return await IS.CreateImagenProducto(requets);
        }

        [Authorize(Policy = "Admin")]
        [HttpPut("PutImagenesProductos")]
        public async Task<ImagenesProducto> PutImagenesProductos(int IdProducto, ImageRequetsDto request)
        {
            var result = await IS.UpdateImagenProducto(IdProducto, request);
            return result;

        }

        [Authorize(Policy = "Admin")]
        [HttpPut("PutImagenesProductosDelete")]
        public async Task<ImagenesProducto> PutImagenesProductosDelete(int IdProducto)
        {

            var result = await IS.UpdateImagenProductoDelete(IdProducto);
            return result;

        }
    }
}
