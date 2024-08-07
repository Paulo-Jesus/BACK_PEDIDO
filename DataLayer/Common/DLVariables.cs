﻿namespace DataLayer.Common
{
    public class DLVariables
    {
        public const string ConnectionString = "ConnectionString";
        public const string ValidChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        public const string Hexadecimal = "X2";
        public const string Rol = "Rol";
        public const string Nombre = "Nombre";
        public const string Jwt_Key = "Jwt:Key";
        public const string HmacSha256Signature = "HS256";
        public const string Claim = "Claim:";
        public const string Id = "Id";
        public const string SP_ParamType_EMD = "ExisteMenuDia";
        public const string SP_ParamType_IMD = "InfoMenuDia";
        public const string SP_ParamType_IP = "InfoProductos";
        public const string IntValue = "IntValue";
        public const string INTARRAYTYPE = "dbo.INTARRAYTYPE";
        public const string IdMenu = "IdMenu";
        public const string FechaInicio = "FechaInicio";
        public const string FechaFin = "FechaFin";
        public const string IdMenuDetalle = "IdMenuDetalle";
        public const string IdProducto = "IdProducto";
        public const string Descripcion = "Descripcion";
        public const string Precio = "Precio";
        public const string Categoria = "Categoria";
        public const string IdCategoria = "IdCategoria";
        public const string Imagen = "Imagen";
        public const string usp_crearRol = "usp_crearRol";


        /*COLUMNAS TABLA USUARIO*/
        public const string IdUsuario = "IdUsuario";
        public const string Cedula = "Cedula";
        public const string NombreU = "Nombre";
        public const string CorreoU = "Correo";
        public const string TelefonoU = "Telefono";
        public const string DireccionU = "Direccion";
        public const string ContrasenaU = "Contrasena";
        public const string IdEmpresa = "IdEmpresa";

        /*COLUMNAS TABLA PROVEEDOR*/
        public const string IdProveedor = "IdProveedor";
        public const string RUC = "RUC";
        public const string Correo = "Correo";
        public const string Telefono = "Telefono";
        public const string Direccion = "Direccion";
        public const string Contrasena = "Contrasena";
        public const string Logotipo = "Logotipo";
        public const string IdRol = "IdRol";
        public const string IdEstado = "IdEstado";

        /*VARIABLES SP_TABLA PROVEEDOR*/
        public const string colIdProveedor = "@IdProveedor";
        public const string colRUC = "@RUC";
        public const string colNombre = "@Nombre";
        public const string colCorreo = "@Correo";
        public const string colTelefono = "@Telefono";
        public const string colDireccion = "@Direccion";
        public const string colContrasena = "@Contrasena";
        public const string colLogotipo = "@Logotipo";
        public const string colIdRol = "@IdRol";
        public const string colIdEstado = "@IdEstado";

        /*VARIABLES SP_TABLA ROL*/
        public const int idRolAdmin = 1;
        public const int idRolUsuario = 2;
        public const int idRolProveedor = 3;

        public const int idEstadoActivo = 1;
        public const int idEstadoInactivo = 2;
        public const int idEstadoBloqueado = 3;
    }
}
