using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace DataLayer.Repositories.Seguridad.Usuarios
{
    public interface IUsuariosRepository
    {
        public Task<Response> ObtenerTodos();

        public Task<Response> Agregar(UsuarioDTO usuarioDTO);

        public Task<Response> Buscar(string? Cedula, string? Nombre, int? IdEmpresa);

        public Task<Response> Editar(UsuarioDTO usuarioDTO);

        public Task<Response> Eliminar(int IdUsuario);

    }

}
