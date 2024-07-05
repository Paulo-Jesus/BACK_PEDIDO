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
        public const string IngresoProducto = "Producto ingresado correctamente";

        // PRODUCTO
        public const string ListaProductos = "Lista de Productos";
        public const string ProductoActualizado = "Producto actualizado correctamente.";
        public const string ProductoNoEncontrado = "No se encontro el producto a actualizar.";
        public const string ProductoActivado = "Se ha activado el producto satisfactoriamente.";
        public const string ProductoDesactivado = "Se ha inhabilitado el producto de manera correcta.";

        // MENU
        public const string MenuExiste = "Ya se encuentra creado el menu del dia.";
        public const string MenuCreado = "Menu creado con exito.";
        public const string ErrorMenuCreado = "Ocurrio un error al crear el Menu.";
        public const string MenuActualizado = "El menu se actualizo con exito.";
        public const string ErrorMenuActualizar = "Ocurrio un error al actualizar el producto.";
        public const string SinMenu = "El restaurante no ha creado el menu del dia.";

        // -----

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
