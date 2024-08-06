using DataLayer.Common;
using DataLayer.Database;
using EntityLayer.Models.DTO;
using EntityLayer.Models.Entities;
using EntityLayer.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DataLayer.Repositories.Seguridad.CrearPerfil
{
    public class CrearPerfilRepository : ICrearPerfilRepository
    {

        private readonly PedidosDatabaseContext _context;
        private readonly Response response = new PedidosDatabase().DatabaseConnection;
        private SqlConnection connection = new();

        public CrearPerfilRepository(PedidosDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Response> GetListEstados()
        {
            try
            {

                List<EstadoDTO> estados = await _context.Estados.Select(r => new EstadoDTO(r.Nombre)).ToListAsync();
                //.Take(2)
                await _context.Rols.Select(r => new RolDTO(r.IdRol, r.Nombre, r.IdEstado)).ToListAsync();
                response.Data = estados;
                response.Message = DLMessages.ListaGeneradaConExito;
                response.Code = ResponseType.Success;
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Response> GetListRoles()
        {
            try
            {

                // List<RolDTO> lisRol = await _context.Rols.Select(r => new RolDTO()).ToListAsync();

                List<RolDTO> lisRol = await _context.Rols.Select(r => new RolDTO(r.IdRol, r.Nombre, r.IdEstado)).ToListAsync();
                response.Data = lisRol;
                response.Message = DLMessages.param_Message;
                response.Code = ResponseType.Success;

            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task MetodoAgregar(SqlConnection connection, RolDTO rol)
        {
            try
            {
                SqlCommand command = new(DLVariables.usp_crearRol, connection);

                command.Parameters.Add(new SqlParameter(DLSPParameters.Nombre, SqlDbType.VarChar, 100)).Value = rol.Nombre;
                command.Parameters.Add(new SqlParameter(DLSPParameters.IdEstado, SqlDbType.Int)).Value = rol.IdEstado;

                command.CommandType = CommandType.StoredProcedure;

                int num = await command.ExecuteNonQueryAsync();

                if (num > 0)
                {
                    response.Code = ResponseType.Success;
                    response.Message = DLMessages.RolAgregado;
                    response.Data = null;
                }
                else
                {
                    response.Code = ResponseType.Error;
                    response.Message = DLMessages.RolNoAgregado;
                    response.Data = null;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<Response> AddRol(RolDTO rol)
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            try
            {
                List<Rol> roles = await _context.Rols
                   .Where(u => u.Nombre == rol.Nombre)
                   .ToListAsync();

                if (roles.Count < 1)
                {
                    if (response.Code == ResponseType.Error)
                    {
                        return response;
                    }

                    connection = (SqlConnection)response.Data!;
                    await MetodoAgregar(connection, rol);

                    return response;
                }

                await Editar(rol);

                await _context.SaveChangesAsync();
                await tx.CommitAsync();
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();

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

        public async Task<Response> Editar(RolDTO rolDTO)
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            try
            {
                var rol = await _context.Rols.FirstOrDefaultAsync(u => u.Nombre == rolDTO.Nombre);

                //usuario!.Contrasena = _utility.EncriptarContrasena(usuarioDTOEditar.Cedula);

                rol!.Nombre = rolDTO.Nombre;
               rol.IdEstado = rolDTO.IdEstado;

                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                response.Code = ResponseType.Success;
                response.Message = DLMessages.RolEditado;
                response.Data = null;
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();

                response.Code = ResponseType.Error;
                response.Message = DLMessages.RolNoEditado;
                response.Data = ex.Data;
            }
            return response;
        }
    }
}
