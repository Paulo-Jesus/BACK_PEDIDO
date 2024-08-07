using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace BusinessLayer.Services.Seguridad.Login
{
    public interface ILoginServicelg
    {
        public Task<Response> IniciarSesion(LoginDTO request);

        public Task<Response> IniciarSesionGoogle(LoginDTO request);
    }
}
