using BusinessLayer.Services.Interfaces;
using DataLayer.Repositories.Interfaces;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace BusinessLayer.Services
{
    public class SeguridadService : ISeguridadService
    {
        private readonly ISeguridadRepository _seguridadRepository;
        private Response response = new();

        public SeguridadService(ISeguridadRepository seguridadRepository)
        {
            _seguridadRepository = seguridadRepository;
        }

        public async Task<Response> UsuariosObtener()
        {
            response = await _seguridadRepository.UsuariosObtener();
            return response;
        }

        public async Task<Response> UsuariosAgregar(UsuarioDTO usuarioDTO)
        {
            response = await _seguridadRepository.UsuariosAgregar(usuarioDTO);
            return response;
        }

        public async Task<Response> UsuariosBuscar(string? Cedula, string? Nombre, int? IdEmpresa) 
        {
            response = await _seguridadRepository.UsuariosBuscar(Cedula, Nombre, IdEmpresa);
            return response;
        }

        public async Task<Response> UsuariosEditarObtener(int IdUsuario)
        {
            response = await _seguridadRepository.UsuariosEditarObtener(IdUsuario);
            return response;
        }

        public async Task<Response> UsuariosEditar(UsuarioDTO usuarioDTO)
        {
            response = await _seguridadRepository.UsuariosEditar(usuarioDTO);
            return response;
        }
    }
}
