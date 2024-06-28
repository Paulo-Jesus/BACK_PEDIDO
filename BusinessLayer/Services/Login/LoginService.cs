using DataLayer.Repositories.Login;
using EntityLayer.Models.DTO;
using EntityLayer.Models.Entities;
using EntityLayer.Responses;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Login
{
    public class LoginService : ILoginService
    {

        private readonly ILoginRepository _loginRepository;
        private Response response = new();

        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task<Response> IniciarSesion(LoginDTO request)
        {
            response = await _loginRepository.IniciarSesion(request);
            return response;
        }

        public async Task<Response> GenerarContrasena(string Correo)
        {
            response = await _loginRepository.GenerarContrasena(Correo);
            return response;
        }
    }
}
