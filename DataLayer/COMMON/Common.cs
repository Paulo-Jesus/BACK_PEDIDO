using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.COMMON
{
    public class Common
    {
        /*NOMBRE CONEXION*/
        public const string nombreConexion = "BD_PEDIDO";

        /*PROCEDIMIENTOS ALMACENADOS*/
        public const string SP_ObtenerTodosBloqueados = "SP_Obtener_Todos_Usuario_Bloqueados";
        public const string SP_Buscar_NombreUsuario = "SP_Buscar_Por_Usuario";
        public const string SP_Desbloquear_Usuario = "SP_Desbloquear_Usuario";
        public const string SP_ValidarLogin = "SP_Validar_Login";

        /*RUTAS API*/
        public const string generalRoute = "api";
        public const string getAPIObtenerUsuariosBloqueados = "/UsuariosBloqueados";
        public const string getAPIBuscarUsuarioBloqueado = "/BuscarUsuarioBloqueado/{nombreUsuario}";
        public const string getAPIDesbloquearUsuario = "/desbloquearUsuario/{nombreUsuario}";
        public const string getAPIValidarLogin = "/validarLogin";

        /*NOMBRE COLUMNAS*/
        public const string nombre = "Nombre";
        public const string clave = "Clave";
        public const string nombreUsuario = "NombreUsuario";
        public const string estadoId = "Estado_IdEstado";

        /*NOMBRE PARAMETROS*/
        public const string nombreUsuarioColumna = "@Nombre_Usuario";
        public const string claveColumna = "@Clave";

        /*MENSAJES*/
        public const string msjUserUnBlock = "Usuario Desbloqueado";
        public const string msjUserNoUnBlock = "Ocurrio un error al desloquear el usuario";
        public const string msjLoginValido = "Datos correctos";
        public const string msjLoginNoValido = "Datos incorrectos";


        /*CORS*/
        public const string corsName = "AllowAnyConnection";
    }
}
