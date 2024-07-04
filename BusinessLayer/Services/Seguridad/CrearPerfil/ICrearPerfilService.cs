using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace BusinessLayer.Services.Seguridad.CrearPerfil
{
    public interface ICrearPerfilService
    {
        public Task<Response> GetListEstados();

        public Task<Response> GetListRoles();

        public Task<Response> AddRol(RolesDTO rol);

        public Task<Response> Editar(RolesDTO rolDTO);
    }
}
