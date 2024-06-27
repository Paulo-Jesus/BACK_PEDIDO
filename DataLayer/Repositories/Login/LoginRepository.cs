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
        private readonly Response response = new();

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
                        u => u.Correo == request.Correo && u.Contrasena == _utility.encriptarContrasena(request.Contrasena)
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
                response.Data = new { issuccess = true, token = _utility.generarJWT(Rol, Nombre) };

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

        //public async Task<Response> RecuperarContrasena(string Correo)
        //{
        //    var correoExiste = await _context.Cuenta.Where(
        //            u => u.Correo == Correo
        //        ).FirstOrDefaultAsync();

        //    if (correoExiste == null)
        //    {
        //        response.Code = ResponseType.Error;
        //        response.Message = "Ingrese un correo existente.";
        //        response.Data = null;

        //        return response;
        //    }

        //    response.Code = ResponseType.Success;
        //    response.Message = "Correo de recuperación enviado!";
        //    response.Data = null;

        //    return response;
        //}
    }
}
