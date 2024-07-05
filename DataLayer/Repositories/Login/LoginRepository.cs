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
                var cuenta = await _context.Cuenta.Where(
                        c => c.Correo == request.Correo && c.Contrasena == _utility.EncriptarContrasena(request.Contrasena)
                    ).FirstOrDefaultAsync();

                if (cuenta == null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = DLMessages.NoInicioSesion;
                    response.Data = null;

                    return response;
                }

                var usuario = await _context.Usuarios.Where(u => u.IdUsuario == cuenta.IdCuenta).FirstOrDefaultAsync();

                if (usuario == null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = "Usuario no encontrado";
                    response.Data = null;

                    return response;
                }

                response.Code = ResponseType.Success;
                response.Message = DLMessages.Bienvenido;
                response.Data = new { token = _utility.GenerarToken(cuenta.IdRol.ToString(), usuario.Nombre) };

            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = null;
            }
            return response;
        }

        public async Task<Response> GenerarContrasena(string Correo)
        {
            try 
            {
                var cuenta = await _context.Cuenta.Where(u => u.Correo == Correo).FirstOrDefaultAsync();

                if (cuenta == null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = "Ingrese un correo existente";
                    response.Data = null;

                    return response;
                }

                response = await EnviarCorreo(Correo, cuenta.IdCuenta);

                //response.Code = ResponseType.Success;
                //response.Message = "Correo existente";
                //response.Data = null;
            }
            catch(Exception ex) 
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = null;
            }

            return response;
        }

        //Enviar Correo
        public async Task<Response> EnviarCorreo(string Correo, int IdCuenta) 
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            try
            {
                var token = await _context.Tokens.Where(u => u.IdCuenta == IdCuenta).FirstOrDefaultAsync();

                var cuenta = await _context.Cuenta.Where(u => u.Correo == Correo).FirstOrDefaultAsync();

                if (cuenta == null) 
                {
                    response.Code = ResponseType.Error;
                    response.Message = "Cuenta inexistente";
                    response.Data = null;
                    
                    return response;
            }

                string textoAleatorio = Utility.GenerarTexto(35);

                string tokenCuerpo = _utility.GenerarToken(textoAleatorio);

                string contrasenaTemporal = Utility.GenerarTexto(10);

                if (token == null)
                {
                    Token tokenDTO = new()
                    {
                        TokenCuerpo = tokenCuerpo,
                        IdCuenta = IdCuenta,
                        FechaExpiracion = DateTime.Now.AddMinutes(5)
                    };
                    _context.Tokens.Add(tokenDTO);
                }
                else
                {
                    token.TokenCuerpo = tokenCuerpo;
                    token.FechaExpiracion = DateTime.Now.AddMinutes(5);
                }

                cuenta.Contrasena = _utility.EncriptarContrasena(contrasenaTemporal);

                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                response.Code = ResponseType.Success;
                response.Message = "Contraseña temporal creada y token creado";
                response.Data = $"Su contraseña temporal es: {contrasenaTemporal} \n" +
                    $"Visite el siguiente link para continuar con el proceso: http://localhost:4200/restablecer_clave/{tokenCuerpo}";
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();

                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = null;
            }
            return response;
        }

        public async Task<Response> ComprobarToken(string tokenCuerpo)
        {
            try
            {
                var token = await _context.Tokens.Where(u => u.TokenCuerpo == tokenCuerpo).FirstOrDefaultAsync();

                if (token == null)
                {

                    response.Code = ResponseType.Error;
                    response.Message = "Se va a el login";
                    response.Data = false;

                    return response;
                }

                response.Code = ResponseType.Success;
                response.Message = "Se mantiene en la pantalla";
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = null;
            }
            return response;
        }

        public async Task<Response> RestablecerContrasena(string tokenCuerpo, string contrasenaTemporal, string contrasenaNueva) 
        {
            using var tx = await _context.Database.BeginTransactionAsync();
            
            try
            {
                var token = await _context.Tokens.Where(u => u.TokenCuerpo == tokenCuerpo).FirstOrDefaultAsync();

                if (token == null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = "Error, token invalido";
                    response.Data = null;

                    return response;
                }

                var cuenta = await _context.Cuenta.Where(u => u.IdCuenta == token.IdCuenta).FirstOrDefaultAsync();
                
                if (cuenta == null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = "Error, cuenta inexistente";
                    response.Data = null;

                    return response;
                }

                if (!cuenta.Contrasena.Equals(_utility.EncriptarContrasena(contrasenaTemporal)))
                {
                    response.Code = ResponseType.Error;
                    response.Message = "Error, clave temporal incorrecta";
                    response.Data = null;

                    return response;
                }

                cuenta.Contrasena = _utility.EncriptarContrasena(contrasenaNueva);
                
                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                response.Code = ResponseType.Success;
                response.Message = "Cambio de clave exitoso";
                response.Data = null;
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();

                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = null;
            }
            return response;
        }
    }
}
