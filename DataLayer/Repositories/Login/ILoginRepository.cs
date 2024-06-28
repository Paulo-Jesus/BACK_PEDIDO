using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace DataLayer.Repositories.Login
{
    public interface ILoginRepository
    {
        public Task<Response> IniciarSesion(LoginDTO request);

        public Task<Response> GenerarContrasena(string Correo);


    }
}
