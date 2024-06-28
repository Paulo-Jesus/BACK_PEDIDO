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

        public async Task<Response> ObtenerTodos()
        {
            response = await _usuariosRepository.ObtenerTodos();
            return response;
        }

        public async Task<Response> Agregar(usuarioDTOEditar usuarioDTO)
        {
            response = await _usuariosRepository.Agregar(usuarioDTO);
            return response;
        }

        public async Task<Response> Buscar(string? Cedula, string? Nombre, int? IdEmpresa)
        {
            response = await _usuariosRepository.Buscar(Cedula, Nombre, IdEmpresa);
            return response;
        }

        public async Task<Response> Editar(usuarioDTOEditar usuarioDTO)
        {
            response = await _usuariosRepository.Editar(usuarioDTO);
            return response;
        }

        public async Task<Response> Elminar(int IdUsuario)
        {
            response = await _usuariosRepository.Eliminar(IdUsuario);
            return response;
        }
    }
}
