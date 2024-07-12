using DataLayer.Repositories.Login;
using DataLayer.Repositories.Seguridad.Usuarios;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Seguridad.Login
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

        public async Task<Response> IniciarSesionGoogle(LoginDTO request)
        {
            response = await _loginRepository.IniciarSesionGoogle(request);
            return response;
        }
    }
}
