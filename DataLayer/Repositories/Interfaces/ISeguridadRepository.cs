using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DataLayer.Repositories.Interfaces
{
    public interface ISeguridadRepository
    {
        public Task<Response> UsuariosObtener();

        public Task<Response> UsuariosAgregar(UsuarioDTO usuarioDTO);

        public Task<Response> UsuariosBuscar(string? Cedula, string? Nombre, int? IdEmpresa);

        public Task<Response> UsuariosEditarObtener(int IdUsuario);

        public Task<Response> UsuariosEditar(UsuarioDTO usuarioDTO);

    }
}
