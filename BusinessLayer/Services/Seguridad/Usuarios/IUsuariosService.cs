using EntityLayer.Responses;
using EntityLayer.Models.DTO;

namespace BusinessLayer.Services.Seguridad.Usuarios
{
    public interface IUsuariosService
    {
        public Task<Response> UsuariosObtener();

        public Task<Response> UsuariosAgregar(UsuarioDTO usuarioDTO);

        public Task<Response> UsuariosBuscar(string? Cedula, string? Nombre, int? IdEmpresa);

        public Task<Response> UsuariosEditar(UsuarioDTO usuarioDTO);

    }
}
