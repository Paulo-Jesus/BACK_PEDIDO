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

        /*RUTAS API*/
        public const string generalRoute = "api";
        public const string getAPIObtenerUsuariosBloqueados = "/getUsuariosBloqueados";
        public const string getAPIBuscarUsuarioBloqueado = "/getBuscarUsuarioBloqueado";


        /*NOMBRE COLUMNAS*/
        public const string nombre = "Nombre";
        public const string nombreUsuario = "NombreUsuario";
        public const string estadoId = "Estado_IdEstado";

        /*NOMBRE PARAMETROS*/
        public const string nombreUsuarioColumna = "@Nombre_Usuario";
    }
}
