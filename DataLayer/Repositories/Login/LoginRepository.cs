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
                var Cuenta = await _context.Cuenta.Where(
                        c => c.Correo == request.Correo && c.Contrasena == _utility.EncriptarContrasena(request.Contrasena)
                    ).FirstOrDefaultAsync();

                if (Cuenta == null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = DLMessages.NoInicioSesion;
                    response.Data = new { issuccess = false, token = "" };

                    return response;
                }

                string Rol = Cuenta!.IdRol.ToString();

                var usuario = await _context.Usuarios.Where(u => u.IdUsuario == Cuenta.IdCuenta).FirstOrDefaultAsync();

                string Nombre = usuario!.Nombre;

                response.Code = ResponseType.Success;
                response.Message = DLMessages.Bienvenido;
                response.Data = new { issuccess = true, token = _utility.TokenInicioSesion(Rol, Nombre) };

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
                var cuentaExiste = await _context.Cuenta.Where(u => u.Correo == Correo).FirstOrDefaultAsync();

                if (cuentaExiste == null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = "Ingrese un correo existente";
                    response.Data = null;

                    return response;
                }

                int IdCuenta = cuentaExiste.IdCuenta;

                response.Code = ResponseType.Success;
                response.Message = "Correo existente";
                response.Data = null;

                await EnviarCorreo(Correo, IdCuenta);

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
        public async Task<Response> EnviarCorreo(string Correo, int IdCuenta) 
        {
            try
            {
                var tokenExiste = await _context.Tokens.Where(u => u.IdCuenta == IdCuenta).FirstOrDefaultAsync();

                var cuentaExiste = await _context.Cuenta.Where(u => u.Correo == Correo).FirstOrDefaultAsync();

                string textoAleatorio = _utility.GenerarTexto(50);

                string tokenUrl = _utility.TokenRestablecerContrasena(textoAleatorio);

                string contrasenaTemporal = _utility.GenerarTexto(10);

                if (tokenExiste == null)
                {
                    Token tokenDTO = new()
                    {
                        TokenCuerpo = tokenUrl,
                        IdCuenta = IdCuenta,
                        FechaExpiracion = DateTime.Now.AddMinutes(5)
                    };
                    _context.Tokens.Add(tokenDTO);
                    _context.SaveChanges();
                }
                else
                {
                    tokenExiste!.TokenCuerpo = tokenUrl;
                    tokenExiste.FechaExpiracion = DateTime.Now.AddMinutes(5);
                    _context.SaveChanges();
                }

                cuentaExiste!.Contrasena = _utility.EncriptarContrasena(contrasenaTemporal);
                _context.SaveChanges();

                response.Code = ResponseType.Success;
                response.Message = "Contraseña temporal creada y token creado";
                response.Data = $"{contrasenaTemporal}, {Correo}, {tokenUrl}";

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

        public async Task<Response> ComprobarToken(string tokenCuerpo)
        {
            try
            {
                var tokenExiste = await _context.Tokens.Where(u => u.TokenCuerpo == tokenCuerpo).FirstOrDefaultAsync();

                if (tokenExiste != null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = "Se mantiene en el login";
                    response.Data = true;

                    return response;
                }

                response.Code = ResponseType.Success;
                response.Message = "Se va a el login";
                response.Data = false;

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

        public async Task<Response> RestablecerContrasena(string tokenCuerpo, string claveTemporal, string claveNueva) 
        {
            try
            {
                var tokenExiste = await _context.Tokens.Where(u => u.TokenCuerpo == tokenCuerpo).FirstOrDefaultAsync();

                if (tokenExiste == null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = "Error, token invalido";
                    response.Data = null;

                    return response;
                }

                int IdCuenta = tokenExiste.IdCuenta;

                var cuentaExiste = await _context.Cuenta.Where(u => u.IdCuenta == IdCuenta).FirstOrDefaultAsync();
                
                if (cuentaExiste == null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = "Error, cuenta inexistente";
                    response.Data = null;

                    return response;
                }

                if(!(cuentaExiste.Contrasena == _utility.EncriptarContrasena(claveTemporal))) 
                {
                    response.Code = ResponseType.Error;
                    response.Message = "Error, clave temporal incorrecta";
                    response.Data = null;

                    return response;
                }

                cuentaExiste.Contrasena = _utility.EncriptarContrasena(claveNueva);
                _context.SaveChanges();

                response.Code = ResponseType.Success;
                response.Message = "Cmabio de clave exitoso";
                response.Data = null;

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
    }
}
