using DataLayer.Common;
using DataLayer.Database;
using DataLayer.Utilities;
using EntitiLayer.Models.Entities;
using EntityLayer.Models.DTO;
using EntityLayer.Models.Mappers;
using EntityLayer.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DataLayer.Repositories.Seguridad.Usuarios
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly PedidosDatabaseContext _context;
        private readonly Response response = new PedidosDatabase().DatabaseConnection;
        private readonly UsuarioMapper usuarioMapper = new();
        private readonly Utility _utility;
        private SqlConnection connection = new();

        public UsuariosRepository(PedidosDatabaseContext context, Utility utility)
        {
            _context = context;
            _utility = utility; 
        }

        public async Task<Response> ObtenerTodos()
        {
            try
            {
                List<Usuario> usuarios = await _context.Usuarios
                    .Where(u => u.IdEstado == 1)
                    .ToListAsync();
                List<UsuarioDTO> usuariosDTO = usuarios.Select(usuarios => usuarioMapper.UsuarioToUsuarioDTO(usuarios)).ToList();

                if (usuarios.Count < 1)
                {
                    response.Code = ResponseType.Success;
                    response.Message = DLMessages.NoUsuariosRegistrados;
                    response.Data = null;

                    return response;
                }

                response.Code = ResponseType.Success;
                response.Message = DLMessages.ListadoUsuarios;
                response.Data = usuariosDTO;
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;
            }
            return response;
        }

        public async Task MetodoAgregar(SqlConnection connection, UsuarioDTO usuarioDTO)
        {
        
            SqlCommand command = new("SP_UsuariosAgregar", connection);
            command.Parameters.Add(new SqlParameter("@Cedula", SqlDbType.VarChar, 10)).Value = usuarioDTO.Cedula;
            command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 100)).Value = usuarioDTO.Nombre;
            command.Parameters.Add(new SqlParameter("@Usuario", SqlDbType.VarChar, 100)).Value = usuarioDTO.Correo;
            command.Parameters.Add(new SqlParameter("@Telefono", SqlDbType.VarChar, 10)).Value = usuarioDTO.Telefono;
            command.Parameters.Add(new SqlParameter("@Direccion", SqlDbType.VarChar, 100)).Value = usuarioDTO.Direccion;
            command.Parameters.Add(new SqlParameter("@Username", SqlDbType.VarChar, 10)).Value = usuarioDTO.Cedula;
            command.Parameters.Add(new SqlParameter("@Contrasena", SqlDbType.VarChar, 100)).Value = _utility.encriptarPass(usuarioDTO.Cedula);
            command.Parameters.Add(new SqlParameter("@IdRol", SqlDbType.Int)).Value = usuarioDTO.IdRol;
            command.Parameters.Add(new SqlParameter("@IdEmpresa ", SqlDbType.Int)).Value = usuarioDTO.IdEmpresa;
            command.Parameters.Add(new SqlParameter("@IdEstado", SqlDbType.Int)).Value = usuarioDTO.IdEstado;
            command.CommandType = CommandType.StoredProcedure;

            int num = await command.ExecuteNonQueryAsync();

            if (num < 0)
            {
                response.Code = ResponseType.Success;
                response.Message = "Usuario agregado.";
                response.Data = null;
            }
            else
            {
                response.Code = ResponseType.Error;
                response.Message = "Error, usuario no agregado.";
                response.Data = null;
            }
        }

        public async Task<Response> Agregar(UsuarioDTO usuarioDTO)
        {
            try
            {
                List<Usuario> usuarios = await _context.Usuarios
                   .Where(u => u.Cedula == usuarioDTO.Cedula)
                   .ToListAsync();

                if (usuarios.Count < 1)
                {
                    if (response.Code == ResponseType.Error)
                    {
                        return response;
                    }

                    connection = (SqlConnection)response.Data!;
                    await MetodoAgregar(connection, usuarioDTO);

                    return response;
                }

                await Editar(usuarioDTO);
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;
            }
            finally
            {
                if (connection.State > 0)
                {
                    await connection.CloseAsync();
                }
            }
            return response;
        }

        public async Task<Response> Buscar(string? Cedula, string? Nombre, int? IdEmpresa)
        {
            try
            {
                List<Usuario> usuarios = [];
                IQueryable<Usuario> query = _context.Usuarios.AsQueryable();

                if (!string.IsNullOrEmpty(Cedula))
                {
                    List<Usuario> query1 = await query.Where(u => u.Cedula.Contains(Cedula)).ToListAsync();
                    usuarios = [.. usuarios, .. query1];
                    //usuarios = usuarios.Concat(query1).ToList();
                }

                if (!string.IsNullOrEmpty(Nombre))
                {
                    List<Usuario> query2 = await query.Where(u => u.Nombre.Contains(Nombre)).ToListAsync();
                    usuarios = [.. usuarios, .. query2];
                }

                if (IdEmpresa > 0)
                {
                    List<Usuario> query3 = await query.Where(u => u.IdEmpresa == IdEmpresa).ToListAsync();
                    usuarios = [.. usuarios, .. query3];
                }

                if (usuarios.Count < 1)
                {
                    response.Code = ResponseType.Success;
                    response.Message = DLMessages.NoCoincidencia;
                    response.Data = null;

                    return response;
                }

                usuarios = usuarios.Distinct().ToList();
                List<UsuarioDTO> usuariosDTOs = usuarios.Select(usuarios => usuarioMapper.UsuarioToUsuarioDTO(usuarios)).ToList();

                response.Code = ResponseType.Success;
                response.Message = "Listado de Usuarios";
                response.Data = usuariosDTOs;
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;
            }
            return response;
        }

        public async Task<Response> Editar(UsuarioDTO usuarioDTO)
        {
            try
            {
                Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == usuarioDTO.IdUsuario);

                //usuario!.Contrasena = _utility.encriptarPass(usuarioDTO.Cedula);

                usuario!.Nombre = usuarioDTO.Nombre;
                usuario.Correo = usuarioDTO.Correo;
                usuario.Telefono = usuarioDTO.Telefono;
                usuario.Direccion = usuarioDTO.Direccion;
                usuario.IdEmpresa = usuarioDTO.IdEmpresa;
                usuario.IdRol = usuarioDTO.IdRol;
                usuario.IdEstado = usuarioDTO.IdEstado;

                await _context.SaveChangesAsync();

                response.Code = ResponseType.Success;
                response.Message = "Usuario Actualizado";
                response.Data = null;
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;
            }
            return response;
        }

        public async Task<Response> Eliminar(int IdUsuario)
        {
            try
            {
                Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == IdUsuario);

                usuario!.IdEstado = 2;

                await _context.SaveChangesAsync();

                response.Code = ResponseType.Success;
                response.Message = "Usuario Eliminado";
                response.Data = null;
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;
            }
            return response;
        }
    }
}
