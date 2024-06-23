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
        public const string nombreConexion = "BD_PEDIDO";//CAMBIAR 

        /*PROCEDIMIENTOS ALMACENADOS*/
        public const string SP_ObtenerTodosBloqueados = "SP_Obtener_Todos_Usuario_Bloqueados";
        public const string SP_Buscar_NombreUsuario = "SP_Buscar_Por_Usuario";
        public const string SP_Desbloquear_Usuario = "SP_Desbloquear_Usuario";
        public const string SP_ValidarLogin = "SP_Validar_Login";
        public const string SP_ObtenerTodosRestaurantes = "SP_ObtenerTodosRestaurantes";
        public const string SP_RegistrarRestaurante = "SP_RegistrarRestaurante";

        /*RUTAS API*/
        public const string generalRoute = "api";
        public const string getAPIObtenerUsuariosBloqueados = "/UsuariosBloqueados";
        public const string getAPIBuscarUsuarioBloqueado = "/BuscarUsuarioBloqueado/{nombreUsuario}";
        public const string getAPIDesbloquearUsuario = "/desbloquearUsuario/{nombreUsuario}";
        public const string getAPIValidarLogin = "/validarLogin";

        /*NOMBRE COLUMNAS TABLA USUARIO*/
        public const string nombre = "Nombre";
        public const string clave = "Clave";
        public const string nombreUsuario = "NombreUsuario";
        public const string estadoId = "Estado_IdEstado";

        /*NOMBRE COLUMNAS TABLA RESTAURANTE*/
        public const string ruc = "RUC";
        public const string nombreR = "Nombre";
        public const string correoR = "Correo";
        public const string telefono = "Telefono";
        public const string direccion = "Direccion";
        public const string username = "Username";
        public const string logotipo = "Logotipo";
        public const string claveR = "Contrasena";
        public const string idRol = "IdRol";
        public const string idEstado = "IdEstado";

        /*NOMBRE PARAMETROS USUARIOS*/
        public const string nombreUsuarioColumna = "@Nombre_Usuario";
        public const string claveColumna = "@Clave";

        /*NOMBRE PARAMETROS RESTAURANTE*/
        public const string  colRuc= "@RUC";
        public const string  colNombreR= "@Nombre";
        public const string  colCorreoR= "@Correo";
        public const string  colTelefonoR= "@Telefono";
        public const string  colDireccionR= "@Direccion";
        public const string  colUsernameR= "@Username";
        public const string  colLogotipoR= "@Logotipo";
        public const string  colClaveR= "@Contrasena";
        public const string  colIdRolR= "@IdRol";
        public const string  colIdEstado= "@IdEstado";

        /*COLUMNA ESTADO y ROL VALUE*/
        public const int rolAdmin = 1;
        public const int rolUsuario = 2;
        public const int rolProveedor = 3;

        public const int idEstadoActivo = 1;
        public const int idEstadoNoActivo = 2;
        public const int idEstadoBloqueado = 3;

        /*MENSAJES*/
        public const string msjUserUnBlock = "Usuario Desbloqueado";
        public const string msjUserNoUnBlock = "Ocurrio un error al desloquear el usuario";
        public const string msjLoginValido = "Datos correctos";
        public const string msjLoginNoValido = "Datos incorrectos";
        public const string msjExito = "Operacion Exitosa";

        /*CORS*/
        public const string corsName = "AllowAnyConnection";
    }
}
