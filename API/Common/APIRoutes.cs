namespace API.Common
{
    public class APIRoutes
    {
        public const string Route = "api/[controller]";
        
        //Login
        public const string IniciarSesionGoogle = "IniciarSesionGoogle";
        public const string IniciarSesion = "IniciarSesion";
        public const string GenerarContrasena = "GenerarContrasena";
        public const string ComprobarToken = "ComprobarToken";
        public const string RestablecerContrasena = "RestablecerContrasena";

        //Parametros
        public const string Guardar = "Guardar";

        //Pedidos  > Menu
        public const string MenuIngresar = "Menu/Ingresar";
        public const string ActualizarMenu = "Actualizar";
        public const string ConsultaTieneMenu = "TieneMenu/{IdProveedor}";
        public const string DatosMenu = "DatosMenu/{IdProveedor}";

        // Pedidos > Productos
        public const string ObtenerProductos = "Obtener/{IdProveedor}";
        public const string ActualizarProducto = "Actualizar";
        public const string EstadoProducto = "Estado";
        public const string IngresarProducto = "Ingresar";

        //Proveedor
        public const string APIObtenerProveedores = "/getRestaurantes";
        public const string APIRegistrarProveedores = "/registrar";

        //Historial de pedidos
        public const string ObtenerPedidos = "ObtenerPedidos";

        //Crear Perfil
        public const string AddRol = "AddRol";
        public const string Editar = "Editar";
        public const string GetListEstados = "GetListEstados";
        public const string GetListRoles = "GetListRoles";

        //Bloquear Usuarios
        public const string getAPIObtenerUsuariosBloqueados = "/UsuariosBloqueados";
        public const string getAPIBuscarUsuarioBloqueado = "/BuscarUsuarioBloqueado/{nombreUsuario}";
        public const string getAPIDesbloquearUsuario = "/desbloquearUsuario/{correo}";

        //Usuario
        public const string ObtenerTodos = "ObtenerTodos";
        public const string Buscar = "Buscar";
        public const string Agregar = "Agregar";
        public const string Eliminar = "Eliminar";


    }
}
