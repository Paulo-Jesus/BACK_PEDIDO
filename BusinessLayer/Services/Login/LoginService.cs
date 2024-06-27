using DataLayer.Repositories.Login;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;

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
    }
}
