using Azure.Core;
using DataLayer.Common;
using DataLayer.Database;
using DataLayer.Utilities;
using EntitiLayer.Models.Entities;
using EntityLayer.Models.DTO;
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
            var usuarioExiste = await _context.Usuarios.Where(
                    u => u.Cedula == request.Usuario && u.Contrasena == _utility.encriptarPass(request.Contrasena)
                ).FirstOrDefaultAsync();

            if (usuarioExiste == null)
            {
                response.Code = ResponseType.Error;
                response.Message = "No se pudo iniciar sesión, intente nuevamente.";
                response.Data = new { isSuccess = false, token = "" };

                return response;
            }

            response.Code = ResponseType.Success;
            response.Message = "Bienvenido!";
            response.Data = new { isSuccess = true, token = _utility.generarJWT(usuarioExiste) };
 
            return response;
        }
    }
}
