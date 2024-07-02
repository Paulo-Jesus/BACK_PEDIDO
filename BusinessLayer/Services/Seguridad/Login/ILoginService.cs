using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace BusinessLayer.Services.Seguridad.Login
{
    public interface ILoginService
    {
        public Task<Response> IniciarSesion(LoginDTO request);

        public bool CompararTokens(string tokenFrontend, string tokenBackend);
    }
}
