namespace BusinessLayer.Common
{
    public class BLRows
    {
        public const string Cedula = "Cedula";
        public const string Nombre = "Nombre";

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
