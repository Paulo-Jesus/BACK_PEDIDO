using DataLayer.Common;
using DataLayer.Database;
using DataLayer.Utilities;
using EntityLayer.Models.DTO;
using Entities = EntityLayer.Models.Entities;
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

        public async Task<Response> IniciarSesionGoogle(LoginDTO request)
        {
            try
            {
                Entities.Cuenta? cuenta = await _context.Cuenta.Where(
                        c => c.Correo == request.Correo).FirstOrDefaultAsync();

                if (cuenta == null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = DLMessages.NoInicioSesion;
                    response.Data = null;

                    return response;
                }

                Entities.Usuario? usuario = await _context.Usuarios.Where(u => u.IdCuenta == cuenta.IdCuenta).FirstOrDefaultAsync();

                Entities.Proveedor? proveedor = await _context.Proveedors.Where(p => p.IdCuenta == cuenta.IdCuenta).FirstOrDefaultAsync();

                if (usuario == null && proveedor == null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = DLMessages.UsuarioNoEncontrado;
                    response.Data = null;

                    return response;
                }

                response.Code = ResponseType.Success;
                response.Message = DLMessages.Bienvenido;
                if (proveedor == null)
                {
                    response.Data = new { token = _utility.GenerarToken(cuenta.IdRol.ToString(), usuario.Nombre, usuario.IdUsuario.ToString()) };
                }
                else
                {
                    response.Data = new { token = _utility.GenerarToken(cuenta.IdRol.ToString(), proveedor.Nombre, proveedor.IdProveedor.ToString()) };
                }
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = null;
            }
            return response;
        }

        public async Task<Response> IniciarSesion(LoginDTO request)
        {
            try
            {
                Entities.Cuenta? cuenta = await _context.Cuenta.Where(
                        c => c.Correo == request.Correo && c.Contrasena == _utility.EncriptarContrasena(request.Contrasena)
                    ).FirstOrDefaultAsync();

                if (cuenta == null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = DLMessages.NoInicioSesion;
                    response.Data = null;

                    return response;
                }

                Entities.Usuario? usuario = await _context.Usuarios.Where(u => u.IdCuenta == cuenta.IdCuenta).FirstOrDefaultAsync();

                Entities.Proveedor? proveedor = await _context.Proveedors.Where(p => p.IdCuenta == cuenta.IdCuenta).FirstOrDefaultAsync();

                if (usuario == null && proveedor == null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = DLMessages.UsuarioNoEncontrado;
                    response.Data = null;

                    return response;
                }

                response.Code = ResponseType.Success;
                response.Message = DLMessages.Bienvenido;
                if (proveedor == null)
                {
                    response.Data = new { token = _utility.GenerarToken(cuenta.IdRol.ToString(), usuario.Nombre, usuario.IdUsuario.ToString()) };
                }
                else
                {
                    response.Data = new { token = _utility.GenerarToken(cuenta.IdRol.ToString(), proveedor.Nombre, proveedor.IdProveedor.ToString()) };
                }
               

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
                Entities.Cuenta? cuenta = await _context.Cuenta.Where(u => u.Correo == Correo).FirstOrDefaultAsync();

                if (cuenta == null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = DLMessages.IngreseCorreoExistente;
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
                Entities.Token? token = await _context.Tokens.Where(u => u.IdCuenta == IdCuenta).FirstOrDefaultAsync();

                Entities.Cuenta? cuenta = await _context.Cuenta.Where(u => u.Correo == Correo).FirstOrDefaultAsync();

                if (cuenta == null) 
                {
                    response.Code = ResponseType.Error;
                    response.Message = DLMessages.CuentaInexistente;
                    response.Data = null;
                    
                    return response;
            }

                string textoAleatorio = Utility.GenerarTexto(35);

                string tokenCuerpo = _utility.GenerarToken(textoAleatorio);

                string contrasenaTemporal = Utility.GenerarTexto(10);

                if (token == null)
                {
                    Entities.Token? tokenDTO = new()
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
                response.Message = DLMessages.ContraseniaTemporalCreada;
                response.Data = DLMessages.EnvioContraseniaTemporal(contrasenaTemporal, tokenCuerpo);
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
                Entities.Token? token = await _context.Tokens.Where(u => u.TokenCuerpo == tokenCuerpo).FirstOrDefaultAsync();

                if (token == null)
                {

                    response.Code = ResponseType.Error;
                    response.Message = DLMessages.SeFueAlLogin;
                    response.Data = false;

                    return response;
                }

                response.Code = ResponseType.Success;
                response.Message = DLMessages.SeMantiene;
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

        public async Task<Response> RecuperarContrasena(string Correo)
        {
            Entities.Cuenta? correoExiste = await _context.Cuenta.Where(
                    u => u.Correo == Correo
                ).FirstOrDefaultAsync();

            if (correoExiste == null)
            {
                response.Code = ResponseType.Error;
                response.Message = DLMessages.IngreseCorreoExistente;
                response.Data = null;

                return response;
            }

            response.Code = ResponseType.Success;
            response.Message = DLMessages.CorreoRecuperacionEnviado;
            response.Data = null;

            return response;
        }

        public async Task<Response> RestablecerContrasena(string tokenCuerpo, string contrasenaTemporal, string contrasenaNueva) 
        {
            using var tx = await _context.Database.BeginTransactionAsync();
            
            try
            {
                Entities.Token? token = await _context.Tokens.Where(u => u.TokenCuerpo == tokenCuerpo).FirstOrDefaultAsync();

                if (token == null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = DLMessages.TokenInvalido;
                    response.Data = null;

                    return response;
                }

                Entities.Cuenta? cuenta = await _context.Cuenta.Where(u => u.IdCuenta == token.IdCuenta).FirstOrDefaultAsync();
                
                if (cuenta == null)
                {
                    response.Code = ResponseType.Error;
                    response.Message = DLMessages.CuentaInexistente;
                    response.Data = null;

                    return response;
                }

                if (!cuenta.Contrasena.Equals(_utility.EncriptarContrasena(contrasenaTemporal)))
                {
                    response.Code = ResponseType.Error;
                    response.Message = DLMessages.ClaveTemprotalIncorrecta;
                    response.Data = null;

                    return response;
                }

                cuenta.Contrasena = _utility.EncriptarContrasena(contrasenaNueva);
                
                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                response.Code = ResponseType.Success;
                response.Message = DLMessages.CambioClaveExitoso;
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
