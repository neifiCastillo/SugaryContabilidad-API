using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SugaryContabilidad_API.DataContext;
using SugaryContabilidad_API.Dto;
using SugaryContabilidad_API.Models;
using SugaryContabilidad_API.Utils;



namespace SugaryContabilidad_API.Services
{
    public class ImagenesService
    {

        private readonly SugaryContabilidadDBContext SCC;
        public ImagenesService(SugaryContabilidadDBContext SCC)
        {
            this.SCC = SCC;
        }

        //imagenes de usuarios
        public async Task<IReadOnlyCollection<ImagenesUsuario>> RunImagenUsuario()
        {
            return await SCC.ImagenesUsuarios.Where(x => x.Status.Equals(true)).ToArrayAsync();
        }

        public async Task<IReadOnlyCollection<ImagenesUsuario>> RunImagenUsuarioById(int IdUsuario)
        {
            var usuario = await SCC.ImagenesUsuarios.Where(x => x.IdUsuario == IdUsuario && x.Status.Equals(true)).ToListAsync();
            if (usuario is null)
            {
                throw new ArgumentException("El usuario que se quiere filtrar no existe");
            }
            return usuario;
        }
        public async Task<ImagenesUsuario> CreateImagenUsuario(ImageRequetsDto requets)
        {
            if (requets is null)
            {
                throw new ArgumentException("El objeto requets es nulo");
            }
            bool existUsuario = SCC.Usuarios.Where(x => x.IdUsuario == requets.id).Count().Equals(0);
            if (existUsuario)
            {
                throw new ArgumentException("El usuario no existe en la base de datos");
            }
            bool existImage = SCC.ImagenesUsuarios.Where(x => x.IdUsuario == requets.id).Count().Equals(0);
            if (!existImage)
            {
                throw new ArgumentException("El usuario ya tiene una imagen registrada");
            }
            var image = new ImagenesUsuario() { IdUsuario = requets.id };
            image.Status = true;
            image.Path = await Upload(requets.Base64);
            await SCC.ImagenesUsuarios.AddAsync(image);
            await SCC.SaveChangesAsync();
            return image;

        }
         public async Task<ImagenesUsuario> UpdateImagenUsuario(int IdUsuario, ImageRequetsDto request)
        {
            var ExistUsuario = await SCC.ImagenesUsuarios.FirstOrDefaultAsync(x => x.IdUsuario == IdUsuario);
            if (ExistUsuario == null)
            {
                throw new ArgumentException("El usuario no fue encontrado");
            }
            ExistUsuario.Path = await Upload(request.Base64);
            await SCC.SaveChangesAsync();

            return ExistUsuario;
        }
        public async Task<ImagenesUsuario> UpdateImagenUsuarioDelete (int IdUsuario)
        {
            var ExistUsuario = await SCC.ImagenesUsuarios.FirstOrDefaultAsync(x => x.IdUsuario == IdUsuario);
            if (ExistUsuario is null)
            {
                throw new ArgumentException("La imagen de usuario que se quiere eliminar no existe");
            }
            ExistUsuario.Status = false;
            await SCC.SaveChangesAsync();
            return ExistUsuario;
        }

        //Imagenes de productos
        public async Task<IReadOnlyCollection<ImagenesProducto>> RunImagenProductos()
        {
            return await SCC.ImagenesProductos.Where(x => x.Status.Equals(true)).ToArrayAsync();
        }

        public async Task<IReadOnlyCollection<ImagenesProducto>> RunImagenProductosById(int IdProducto)
        {
            var producto = await SCC.ImagenesProductos.Where(x => x.IdProducto == IdProducto && x.Status.Equals(true)).ToListAsync();
            if (producto is null)
            {
                throw new ArgumentException("El Producto que se quiere filtrar no existe");
            }
            return producto;
        }
        public async Task<ImagenesProducto> CreateImagenProducto(ImageRequetsDto requets)
        {
            if (requets is null)
            {
                throw new ArgumentException("El objeto requets es nulo");
            }
            bool existProducto = SCC.Productos.Where(x => x.IdProducto == requets.id).Count().Equals(0);
            if (existProducto)
            {
                throw new ArgumentException("El Producto no existe en la base de datos");
            }
            bool existImage = SCC.ImagenesProductos.Where(x => x.IdProducto == requets.id).Count().Equals(0);
            if (!existImage)
            {
                throw new ArgumentException("El Producto ya tiene una imagen registrada");
            }
            var image = new ImagenesProducto() { IdProducto = requets.id };
            image.Status = true;
            image.Path = await Upload(requets.Base64);
            await SCC.ImagenesProductos.AddAsync(image);
            await SCC.SaveChangesAsync();
            return image;

        }
        public async Task<ImagenesProducto> UpdateImagenProducto(int IdProducto, ImageRequetsDto request)
        {
            var existProducto = await SCC.ImagenesProductos.FirstOrDefaultAsync(x => x.IdProducto == IdProducto);
            if (existProducto == null)
            {
                throw new ArgumentException("El Producto no fue encontrado");
            }
            existProducto.Path = await Upload(request.Base64);
            await SCC.SaveChangesAsync();

            return existProducto;
        }
        public async Task<ImagenesProducto> UpdateImagenProductoDelete(int IdProducto)
        {
            var existProducto = await SCC.ImagenesProductos.FirstOrDefaultAsync(x => x.IdProducto == IdProducto);
            if (existProducto is null)
            {
                throw new ArgumentException("La imagen de producto que se quiere eliminar no existe");
            }
            existProducto.Status = false;
            await SCC.SaveChangesAsync();
            return existProducto;
        }

        //Cloudnary
        private async Task<string> Upload(string base64)
        {
            var cloudinary = new Cloudinary(new Account("dyu8dtaal", "629731295296445", "sXupLiRgFi4hltSQ-R62retYwb0"));
            cloudinary.Api.Secure = true;
            // Upload
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(Guid.NewGuid().ToString(), new MemoryStream(Convert.FromBase64String(base64)))
            };
            var respuesta = await cloudinary.UploadAsync(uploadParams);
            return respuesta.SecureUrl.AbsoluteUri;
        }
        
    }
}
