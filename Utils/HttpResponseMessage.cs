﻿namespace SugaryContabilidad_API.Utils
{
    public class HttpResponseText
    {

        //Productos
        public static readonly string ExistProducto = "Este producto ya existe";
        public static readonly string CreateProducto = "Producto creado con exito!";
        public static readonly string PutProducto = "Producto modificado con exito!";
        public static readonly string PutProductoNotFound = "Este Producto no pudo encontrarse!";
        public static readonly string PutProductoDelete = "Producto eliminado con exito!";

        //EstadoProducto
        public static readonly string ExistEstadoProducto = "Este Estado de producto ya existe";
        public static readonly string CreateEstadoProducto = "Estado de producto creado con exito!";
        public static readonly string PutEstadoNotFound = "Este Estado de producto no pudo encontrarse!";
        public static readonly string PutEstado = "Estado de producto modificado con exito!";
        public static readonly string PutEstadoDelete = "Estado de producto eliminado con exito!";

        //MetodoVenta
        public static readonly string ExistMetodoVenta = "Este Metodo de venta ya existe";
        public static readonly string CreateMetodoVenta = "Metodo de venta creado con exito!";
        public static readonly string PutMetodoNotFound = "Este Metodo de venta no pudo encontrarse!";
        public static readonly string PutMetodo = "Metodo de venta modificado con exito!";
        public static readonly string PutMetodoDelete = "Metodo de vente eliminado con exito!";
        public static readonly string NullCedulaMetodo = "El Campo Cedula es null.";
        public static readonly string validationCedulaMetodo = "No se pudo validar la cédula.";

        //Venta
        public static readonly string ExistVenta = "Esta venta ya existe";
        public static readonly string CreateVenta = "Venta realizada con exito!";
        public static readonly string ExistProductoVenta = "Este producto no existe";
        public static readonly string CantidadProducto = "No se puede vender mas que la cantidad existente del producto";
        public static readonly string PutVentaDelete = "Venta eliminado con exito!";
        public static readonly string VentaEmpty = "No se registro producto que vender!";
        public static readonly string MetodoVentaEmpty = "Debe de seleccionar un metodod de venta!";

        //Deudas
        public static readonly string CreateDeudas = "Deuda creada con exito!";
        public static readonly string ExistDeudas = "Este Deudas ya existe";
        public static readonly string NullCedulaDeudas = "El Campo Cedula es null.";
        public static readonly string validationCedulaDeudas = "No se pudo validar la cédula.";
        public static readonly string PutDeudasNotFound = "Esta Deudas no pudo encontrarse!";
        public static readonly string PutDeudas = "Aporte a deuda realizado con exito!";
        public static readonly string PutDeudaDelete = "Deuda pagada con exito!";
        public static readonly string PutDeudaNopagada = "La deuda aun no se termina de pagar!";
        public static readonly string DeudaPagada = "Esta Deuda ya se pago!";
        public static readonly string AporteMayorADeuda = "El aporte no puede ser mayor a la deuda pendiente";

        //Caja
        public static readonly string ExistCaja = "Esta Caja ya existe";
        public static readonly string CajasSinCerrar = "No se puede crear nueva caja , Debe de cerrar la caja que actualmente esta abierta";
        public static readonly string PutCajaNotFound = "Esta Caja no pudo encontrarse!";
        public static readonly string CreateCaja = "Caja creada con exito!";
        public static readonly string PutCaja = "Caja modificada con exito!";
        public static readonly string PutCajaDelete = "Caja cerrada con exito!";

        //Usuarios
        public static readonly string ExistUsuario = "Este Usuario ya existe";
        public static readonly string CreateUsuario = "Usuario creado con exito!";
        public static readonly string PutUsuarioNotFound = "Este Usuario no pudo encontrarse!";
        public static readonly string PutUsuario = "Usuario modificado con exito!";
        public static readonly string PutUsuarioDelete = "Usuario eliminado con exito!";

        //200OK
        public static readonly string OK = "OK.";
    }
}
