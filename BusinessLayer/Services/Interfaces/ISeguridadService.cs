using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace BusinessLayer.Services.Interfaces
{
    public interface ISeguridadService
    {
        public Task<Response> UsuariosObtener();

        public Task<Response> UsuariosAgregar(UsuarioDTO usuarioDTO);

        public Task<Response> UsuariosBuscar(string? Cedula, string? Nombre, int? IdEmpresa);

        public Task<Response> UsuariosEditarObtener(int IdUsuario);

        public Task<Response> UsuariosEditar(UsuarioDTO usuarioDTO);
    }
}
