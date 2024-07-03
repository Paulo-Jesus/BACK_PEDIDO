using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace BusinessLayer.Services.Login
{
    public interface ILoginService
    {
        public Task<Response> IniciarSesion(LoginDTO request);
        public Task<Response> GenerarContrasena(string Correo);
        public Task<Response> ComprobarToken(string tokenCuerpo);
        public Task<Response> RestablecerContrasena(string tokenCuerpo, string claveTemporal, string claveNueva);
    }
}
