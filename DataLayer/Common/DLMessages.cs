using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Common
{
    public class DLMessages
    {
        public const string ConexionExitosa = "Conexion Exitosa";
        public const string ListadoUsuarios = "Listado de Usuarios";
        public const string ListaVacia = "Lista Vacia";
        public const string NoUsuariosRegistrados = "No hay usuarios registrados.";
        public const string NoCoincidencia = "No existen usuarios con los datos ingresados.";

        /*PROVEEDORES*/
        public const string Msj_Registro_Exito = "Datos registrados correctamente.";

        /*LOGIN*/
        public const string Msj_Login_Exito = "Login exitoso";
        public const string Msj_Login_Fallo = "Fallo al iniciar sesion";

        /*DESBLOQUEO CUENTA*/
        public const string Msj_Usuario_Unblock = "Cuenta Desbloqueada";
        public const string Msj_Usuario_block = "No se pudo desbloquar el usuario seleccionado";
    
    }
}
