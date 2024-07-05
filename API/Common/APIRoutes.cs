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

        // Pedidos > Productos
        public const string ObtenerProductos = "Obtener/{IdProveedor}";
        public const string ActualizarProducto = "Actualizar";
        public const string EstadoProducto = "Estado";
        public const string IngresarProducto = "Ingresar";

        // Pedidos > Menu
        public const string IngresarMenu = "Ingresar";
        public const string ActualizarMenu = "Actualizar";
        public const string ConsultaTieneMenu = "TieneMenu/{IdProveedor}";
        public const string DatosMenu = "DatosMenu/{IdProveedor}";
    }
}
