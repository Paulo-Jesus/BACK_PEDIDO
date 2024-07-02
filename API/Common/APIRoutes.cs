namespace API.Common
{
    public class APIRoutes
    {
        public const string Route = "api/[controller]";
        public const string getAPIObtenerUsuariosBloqueados = "/UsuariosBloqueados";
        public const string getAPIBuscarUsuarioBloqueado = "/BuscarUsuarioBloqueado/{nombreUsuario}";
        public const string getAPIDesbloquearUsuario = "/desbloquearUsuario/{correo}";
        public const string APIObtenerProveedores = "/getRestaurantes";
        public const string APIRegistrarProveedores = "/registrar";

    }
}
