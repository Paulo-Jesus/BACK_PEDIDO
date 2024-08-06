using DataLayer.Repositories.Seguridad.CrearPerfil;
using EntityLayer.Models.DTO;
using EntityLayer.Models.Entities;
using EntityLayer.Responses;

namespace BusinessLayer.Services.Seguridad.CrearPerfil
{
    public class CrearPerfilService : ICrearPerfilService
    {
        private readonly ICrearPerfilRepository _crearPerfilRepository;
        private Response response = new();

        public CrearPerfilService(ICrearPerfilRepository crearPerfilRepository)
        {
            _crearPerfilRepository = crearPerfilRepository;
        }   

<<<<<<< Updated upstream
        public async Task<Response> AddRol(RolesDTO rol)
=======
        public async Task<Response> AddRol(RolDTO rol)
>>>>>>> Stashed changes
        {
            response = await _crearPerfilRepository.AddRol(rol);
            return response;
        }

<<<<<<< Updated upstream
        public async Task<Response> Editar(RolesDTO rolDTO)
=======
        public async Task<Response> Editar(RolDTO rolDTO)
>>>>>>> Stashed changes
        {
            response = await _crearPerfilRepository.Editar(rolDTO);
            return response;
        }

        public async Task<Response> GetListEstados()
        {
            response = await _crearPerfilRepository.GetListEstados();
            return response;
        }

        public async Task<Response> GetListRoles()
        {
            response = await _crearPerfilRepository.GetListRoles();
            return response;
        }
    }
}
