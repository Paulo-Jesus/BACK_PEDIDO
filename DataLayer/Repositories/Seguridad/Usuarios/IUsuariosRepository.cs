using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace DataLayer.Repositories.Seguridad.Usuarios
{
    public interface IUsuariosRepository
    {
        public Task<Response> ObtenerTodos();

        public Task<Response> Agregar(usuarioDTOEditar usuarioDTO);

        public Task<Response> Buscar(string? Cedula, string? Nombre, int? IdEmpresa);

        public Task<Response> Editar(usuarioDTOEditar usuarioDTOEditar);

        public Task<Response> Eliminar(int IdUsuario);

    }

}
