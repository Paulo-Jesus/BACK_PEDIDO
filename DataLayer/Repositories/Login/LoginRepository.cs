using DataLayer.Common;
using DataLayer.Database;
using DataLayer.Utilities;
using EntityLayer.Models.DTO;
using EntityLayer.Models.Entities;
using EntityLayer.Responses;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Login
{
    public class LoginRepository : ILoginRepository
    {
        private readonly PedidosDatabaseContext _context;
        private readonly Utility _utility;
        private Response response = new();

        public LoginRepository(PedidosDatabaseContext context, Utility utility)
        {
            _context = context;
            _utility = utility;
        }

        public async Task<Response> IniciarSesion(LoginDTO request)
        {
            try
            {
                Cuenta Cuenta = await _context.Cuenta.Where(
                        c => c.Correo == request.Correo && c.Contrasena == _utility.encriptarContrasena(request.Contrasena)
                    ).FirstOrDefaultAsync();

                if (Cuenta == null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = DLMessages.NoInicioSesion;
                    response.Data = new { issuccess = false, token = "" };

                    return response;
                }

                string Rol = Cuenta!.IdRol.ToString();

                Usuario usuario = await _context.Usuarios.Where(u => u.IdUsuario == Cuenta.IdCuenta).FirstOrDefaultAsync();

                string Nombre = usuario!.Nombre;

                response.Code = ResponseType.Success;
                response.Message = DLMessages.Bienvenido;
                response.Data = new { issuccess = true, token = _utility.tokenInicioSesion(Rol, Nombre) };

                return response;
            }
            catch (Exception ex)
            {

                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = null;

                return response;
            }
        }

        public async Task<Response> GenerarContrasena(string Correo)
        {
            try 
            {
                Cuenta correoExiste = await _context.Cuenta.Where(u => u.Correo == Correo).FirstOrDefaultAsync();
                
                if (correoExiste == null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = "Ingrese un correo existente";
                    response.Data = false;

                    return response;
                }

                response.Code = ResponseType.Success;
                response.Message = "Correo existente";
                response.Data = true;

                return response;
            }
            catch(Exception ex) 
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = null;

                return response;
            }
        }

        //Enviar Correo
        //public async Task<Response> EnviarCorreo(string Correo)
        //{
        //    Cuenta cuenta = await _context.Cuenta.Where(
        //            u => u.Correo == Correo
        //        ).FirstOrDefaultAsync();

        //    if (cuenta == null)
        //    {
        //        response.Code = ResponseType.Error;
        //        response.Message = "El correo ingresado no existe.";
        //        response.Data = null;

        //        return response;
        //    }

        //    int IdCuenta = cuenta!.IdCuenta;

        //    //CREAMOS Y GUARDAMOS LA NUEVA CONTRASEÑA TEMPORAL
        //    string contrasena = _utility.contrasenaTemporal();
        //    cuenta.Contrasena = _utility.encriptarContrasena(contrasena);
        //    await _context.SaveChangesAsync();

        //    //TABLA TOKEN - ID, TOKEN, IDCUENTA, EXPIRACION
        //    //Verificamos si el usuario ya registró un token o no y si expiró o no 
        //    Token token = await _context.Token.Where(
        //            t => t.IdCuenta == IdCuenta
        //        ).FirstOrDefaultAsync();

        //    string textoAleatorio = _utility.textoAleatorio();
        //    string tokenUrl = _utility.tokenUrlRestablecerContrasena(textoAleatorio);

        //    if (token == null)
        //    {
        //        //insertamos el  token en la tabla tokenUrl
        //    }
        //    else
        //    {
        //        //editamos el token tokenUrl
        //    }

        //    var contrasenaTemporalExiste = await _context.Cuenta.Where(
        //            c => c.Contrasena ==
        //        ).FirstOrDefaultAsync();

        //    return response;
        //}
    }
}
