namespace DataLayer.Common
{
    public class DLMessages
    {
        public const string ConexionExitosa = "Conexion exitosa";
        public const string ListadoDeUsuarios = "Listado de usuarios";
        public const string ListaVacia = "Lista vacia";
        public const string ListadoUsuarios = "Listado de Usuarios";
        public const string NoUsuariosRegistrados = "No hay usuarios registrados";
        public const string NoCoincidenciaBusqueda = "No existen usuarios con los datos ingresados";
        public const string UsuarioAgregado = "Usuario agregado";
        public const string UsuarioNoAgregado = "Usuario no agregado";
        public const string UsuarioNoEncontrado = "Usuario no encontrado";
        public const string IngreseCorreoExistente = "Ingrese un correo existente";
        public const string CuentaInexistente = "Cuenta inexistente";
        public const string ContraseniaTemporalCreada = "Contraseña temporal creada y token creado";
        public const string CorreoActualizado = "Correo Actualizado";
        public const string CorreoEliminado = "Correo Eliminado";
        public const string TokenInvalido = "Token Invalido";
        public const string ClaveTemprotalIncorrecta = "Clave temporal incorrecta";
        public const string CambioClaveExitoso = "Cambio de clave exitoso";
        public const string CorreoRecuperacionEnviado = "Correo de recuperación enviado!";
        public const string NoInicioSesion = "No se pudo iniciar sesión, intente nuevamente.";
        public const string Bienvenido = "Bienvenido";
        public const string SeFueAlLogin = "Se va a el login";
        public const string SeMantiene = "Se mantiene en la pantalla";
        public const string param_Message = "param.Message";
        public const string ListaGeneradaConExito = "Lista Generada con éxito";

        public const string EmpresaAgregada = "Empresa agregada";
        public const string EmpresaNoAgregada = "Empresa no agregada";
        public const string EmpresaEditada = "Empresa actualizada";
        public const string NoCoincidencia = "No existen usuarios con los datos ingresados.";

        public static string EnvioContraseniaTemporal(string contrasenaTemporal, string tokenCuerpo)
        {
            return $"Su contraseña temporal es: {contrasenaTemporal} \n" +
                   $"Visite el siguiente link para continuar con el proceso: http://localhost:4200/restablecer_clave/{tokenCuerpo}";
        }

        // PRODUCTO
        public const string IngresoProducto = "Producto ingresado correctamente";
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

        //Rol
        public const string NoRolsRegistrados = "No hay Rols registrados";
        public const string ListadoDeRols = "No se pudo listar";
        public const string RolAgregado = "Rol Agregado";
        public const string RolNoAgregado = "Rol No Agregado";
        public const string RolEditado = "Rol Editado";
        public const string RolNoEditado = "Rol No Editado";

    }
}
