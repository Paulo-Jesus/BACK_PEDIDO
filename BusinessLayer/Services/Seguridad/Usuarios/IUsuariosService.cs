using EntityLayer.Responses;
using EntityLayer.Models.DTO;

namespace BusinessLayer.Services.Seguridad.Usuarios
{
    public interface IUsuariosService
    {
        public Task<Response> ObtenerTodos();

        public Task<Response> Agregar(usuarioDTOEditar usuarioDTO);

        public Task<Response> Buscar(string? Cedula, string? Nombre, int? IdEmpresa);

        public Task<Response> Editar(usuarioDTOEditar usuarioDTO);

        public Task<Response> Elminar(int IdUsuario);
    }
}
