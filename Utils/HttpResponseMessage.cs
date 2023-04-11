namespace SugaryContabilidad_API.Utils
{
    public class HttpResponseText
    {
        //Deudas
        public static readonly string validationCedula = "No se pudo validar la cédula.";
        public static readonly string NullCedula = "El Campo Cedula es null.";
        public static readonly string NoResultCedula = "Cedula ya registrada.";

        //Productos
        public static readonly string ExistProducto = "Este producto ya existe";
        public static readonly string CreateProducto = "Producto creado con exito!";
        public static readonly string PutProducto = "Producto modificado con exito!";
        public static readonly string PutProductoNotFound = "Este producto no pudo encontrarse!";
        public static readonly string PutProductoDelete = "Producto eliminado con exito!";

        //EstadoProducto
        public static readonly string ExistEstadoProducto = "Este Estado de producto ya existe";
        public static readonly string CreateEstadoProducto = "Estado de producto creado con exito!";
        public static readonly string PutEstadoNotFound = "Este Estado de producto no pudo encontrarse!";
        public static readonly string PutEstado = "Estado de producto modificado con exito!";
        public static readonly string PutEstadoDelete = "Estado de producto eliminado con exito!";

        //200OK
        public static readonly string OK = "OK.";
    }
}
