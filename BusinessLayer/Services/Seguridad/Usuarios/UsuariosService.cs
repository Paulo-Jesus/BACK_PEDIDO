using DataLayer.Repositories.Seguridad.Usuarios;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace BusinessLayer.Services.Seguridad.Usuarios
{
    public class UsuariosService : IUsuariosService
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private Response response = new();

        public UsuariosService(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        public async Task<Response> UsuariosObtener()
        {
            response = await _usuariosRepository.UsuariosObtener();
            return response;
        }

        public async Task<Response> UsuariosAgregar(UsuarioDTO usuarioDTO)
        {
            response = await _usuariosRepository.UsuariosAgregar(usuarioDTO);
            return response;
        }

        public async Task<Response> UsuariosBuscar(string? Cedula, string? Nombre, int? IdEmpresa)
        {
            response = await _usuariosRepository.UsuariosBuscar(Cedula, Nombre, IdEmpresa);
            return response;
        }

        public async Task<Response> UsuariosEditar(UsuarioDTO usuarioDTO)
        {
            response = await _usuariosRepository.UsuariosEditar(usuarioDTO);
            return response;
        }

    }
}
