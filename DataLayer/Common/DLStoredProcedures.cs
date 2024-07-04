using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Common
{
    public class DLStoredProcedures
    {
        public const string SP_GetPersonas = "SP_GetPersonas";
        public const string SP_GetCurrentDate = "GET_CURRENTDATE";
        public const string SP_GeneralValidation = "GENERAL_VALIDATION";
        public const string SP_RegistrarMenu = "INSERTAR_MENU";
        public const string SP_ActualizarMenu = "ACTUALIZAR_MENU";
        public const string SP_Buscar_Por_Correo = "SP_Buscar_Por_Correo";
        public const string SP_Desbloquear_Usuario = "SP_Desbloquear_Usuario";
        public const string SP_Obtener_Todos_Usuario_Bloqueados = "SP_Obtener_Todos_Usuario_Bloqueados";
        public const string SP_ObtenerTodosProveedores = "SP_ObtenerTodosProveedores";
        public const string SP_RegistrarProveedor = "SP_RegistrarProveedor";
    }
}
