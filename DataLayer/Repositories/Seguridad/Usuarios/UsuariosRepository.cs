using DataLayer.Common;
using DataLayer.Database;
using DataLayer.Utilities;
using EntityLayer.Models.DTO;
using EntityLayer.Models.Entities;
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
                response.Message = DLMessages.ListadoDeUsuarios;
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

        public async Task MetodoAgregar(SqlConnection connection, usuarioDTOEditar usuarioDTO)
        {
        
            SqlCommand command = new(DLStoredProcedures.SP_InsertarUsuarioConCuenta, connection);

            command.Parameters.Add(new SqlParameter(DLParameters.Correo, SqlDbType.VarChar, 100)).Value = usuarioDTO.Correo;
            command.Parameters.Add(new SqlParameter(DLParameters.Contrasena, SqlDbType.VarChar, 100)).Value = _utility.encriptarContrasena(usuarioDTO.Cedula);
            command.Parameters.Add(new SqlParameter(DLParameters.IdRol, SqlDbType.Int)).Value = usuarioDTO.IdRol;
            command.Parameters.Add(new SqlParameter(DLParameters.IdEmpresa, SqlDbType.Int)).Value = usuarioDTO.IdEstado;


            command.Parameters.Add(new SqlParameter(DLParameters.Cedula, SqlDbType.VarChar, 10)).Value = usuarioDTO.Cedula;
            command.Parameters.Add(new SqlParameter(DLParameters.Nombre, SqlDbType.VarChar, 100)).Value = usuarioDTO.Nombre;
            command.Parameters.Add(new SqlParameter(DLParameters.Telefono, SqlDbType.VarChar, 10)).Value = usuarioDTO.Telefono;
            command.Parameters.Add(new SqlParameter(DLParameters.Direccion, SqlDbType.VarChar, 100)).Value = usuarioDTO.Direccion;
            command.Parameters.Add(new SqlParameter(DLParameters.IdEmpresa, SqlDbType.Int)).Value = usuarioDTO.IdEmpresa;
            
            command.CommandType = CommandType.StoredProcedure;

            int num = await command.ExecuteNonQueryAsync();

            if (num < 0)
            {
                response.Code = ResponseType.Success;
                response.Message = DLMessages.UsuarioAgregado;
                response.Data = null;
            }
            else
            {
                response.Code = ResponseType.Error;
                response.Message = DLMessages.UsuarioNoAgregado;
                response.Data = null;
            }
        }

        public async Task<Response> Agregar(usuarioDTOEditar usuarioDTO)
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
                    response.Message = DLMessages.NoCoincidenciaBusqueda;
                    response.Data = null;

                    return response;
                }

                usuarios = usuarios.Distinct().ToList();
                List<UsuarioDTO> usuariosDTOs = usuarios.Select(usuarios => usuarioMapper.UsuarioToUsuarioDTO(usuarios)).ToList();

                response.Code = ResponseType.Success;
                response.Message = DLMessages.ListadoDeUsuarios;
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

        public async Task<Response> Editar(usuarioDTOEditar usuarioDTOEditar)
        {
            try
            {
                var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == usuarioDTOEditar.IdUsuario);

                var cuenta = await _context.Cuenta.FirstOrDefaultAsync(c => c.IdCuenta == usuarioDTOEditar.IdCuenta);

                //usuario!.Contrasena = _utility.encriptarContrasena(usuarioDTOEditar.Cedula);

                usuario!.Nombre = usuarioDTOEditar.Nombre;
                cuenta!.Correo = usuarioDTOEditar.Correo;
                usuario.Telefono = usuarioDTOEditar.Telefono;
                usuario.Direccion = usuarioDTOEditar.Direccion;
                usuario.IdEmpresa = usuarioDTOEditar.IdEmpresa;
                cuenta.IdRol = usuarioDTOEditar.IdRol;
                usuario.IdEstado = usuarioDTOEditar.IdEstado;

                await _context.SaveChangesAsync();

                response.Code = ResponseType.Success;
                response.Message = DLMessages.CorreoActualizado;
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
                response.Message = DLMessages.CorreoEliminado;
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
