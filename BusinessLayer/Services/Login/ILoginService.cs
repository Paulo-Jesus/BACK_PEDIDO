using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace BusinessLayer.Services.Login
{
    public interface ILoginService
    {
        public Task<Response> IniciarSesion(LoginDTO request);
        public Task<Response> GenerarContrasena(string Correo);
    }
}
