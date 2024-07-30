namespace BusinessLayer.Common
{
    public class BLRows
    {

        /*COLUMNAS TABLA USUARIO*/
        public const string IdUsuario = "IdUsuario";
        public const string Cedula = "Cedula";
        public const string NombreU = "Nombre";
        public const string CorreoU = "Correo";
        public const string TelefonoU = "Telefono";
        public const string DireccionU = "Direccion";
        public const string ContrasenaU = "Contrasena";
        public const string IdEmpresa = "IdEmpresa";

        /*VARIABLES SP_TABLA USUARIO*/
        public const string colUsernameU = "@UsernameU";
        public const string colClaveU = "@Clave";



        /*COLUMNAS TABLA PROVEEDOR*/
        public const string IdProveedor = "IdProveedor";
        public const string RUC = "RUC";
        public const string Nombre = "Nombre";
        public const string Correo = "Correo";
        public const string Telefono = "Telefono";
        public const string Direccion = "Direccion";
        public const string Contrasena = "Contrasena";
        public const string Logotipo = "Logotipo";
        public const string IdRol = "IdRol";
        public const string IdEstado = "IdEstado";

        /*COLUMNAS TABLA ROL*/
        public const string RolNombre = "Nombre";

        /*COLUMNAS TABLA ESTADO*/
        public const string EstadoNombre = "Nombre";

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

        Comandos Com = new Comandos();

        public object parametros()
        {
            Com.NombreRolquery = "@Nombre";
            Com.IdEstadoQuery = "@IdEstado";
            ; return Com;
        }

        public object parametrosAddRol()
        {
            Com.QueryConsulta = "SELECT COUNT(*) FROM Rol WHERE Nombre = @Nombre";
            Com.QueryStoredProcedure = "usp_crearRol";
            Com.Message = "Perfil Registrado \n";
            Com.MensajeError = "El Perfil que desea crear ya se encuentra registrado \n";
            Com.MensajeError2 = "No se pudo agregar el Perfil deseado \n";
            ; return Com;
        }
        public object parametrosUpdateRolQuery()
        {

            Com.QueryStoredProcedure = "sp_updateRol";
            Com.Message = "Comando exitoso";
            Com.MensajeError = "No se puedo Actualizar el estado del Perfil \n";
            Com.MensajeError2 = "El perfil que desea actualizar no Existe";
            ; return Com;
        }

        public object parametrosPedidos()
        {
            Com.QueryStoredProcedure = "sp_consultarPedidos";
            Com.ParametroQuery = "d";
            Com.Message = "Consulta Exitosa \n";
            Com.MensajeError = "Nose pudo obtener el historial de pedidos \n";
            ; return Com;
        }
    }

    public class Comandos
    {
        public string? QueryStoredProcedure { get; set; }
        public string? QueryConsulta { get; set; }
        public string? MensajeError { get; set; }
        public string? MensajeError2 { get; set; }
        public string? Message { get; set; }
        public string? NombreRolquery { get; set; }
        public string? IdEstadoQuery { get; set; }
        public string? ParametroQuery { get; set; }


    }
}
