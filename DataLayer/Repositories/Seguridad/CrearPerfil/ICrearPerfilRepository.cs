using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace DataLayer.Repositories.Seguridad.CrearPerfil
{
    public interface ICrearPerfilRepository
    {
        public Task<Response> GetListEstados();

        public Task<Response> GetListRoles();

        public Task<Response> AddRol(RolDTO rol);

        public Task<Response> Editar(RolDTO rolDTO);
    }
}
