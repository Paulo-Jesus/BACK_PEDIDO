using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace DataLayer.Repositories.Seguridad.Usuarios
{
    public interface IUsuariosRepository
    {
        public Task<Response> UsuariosObtener();

        public Task<Response> UsuariosAgregar(UsuarioDTO usuarioDTO);

        public Task<Response> UsuariosBuscar(string? Cedula, string? Nombre, int? IdEmpresa);

        public Task<Response> UsuariosEditar(UsuarioDTO usuarioDTO);

        public Task<Response> UsuarioEliminar(int IdUsuario);

    }

}
