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

<<<<<<< Updated upstream
                List<EstadoDTO> estados = await _context.Estados
                               .Take(2)
                               .Select(r => new EstadoDTO(r.Nombre))
                               .ToListAsync();
=======
                List<EstadoDTO> estados = await _context.Estados.Select(r => new EstadoDTO(r.Nombre)).ToListAsync();
                //.Take(2)
                await _context.Rols.Select(r => new RolDTO(r.IdRol, r.Nombre, r.IdEstado)).ToListAsync();
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
                List<RolesDTO> lisRol = await _context.Rols.Select(r => new RolesDTO(r.Nombre, r.IdEstado)).ToListAsync();

=======
                // List<RolDTO> lisRol = await _context.Rols.Select(r => new RolDTO()).ToListAsync();

                List<RolDTO> lisRol = await _context.Rols.Select(r => new RolDTO(r.IdRol, r.Nombre, r.IdEstado)).ToListAsync();
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
        public async Task MetodoAgregar(SqlConnection connection, RolesDTO rol)
=======
        public async Task MetodoAgregar(SqlConnection connection, RolDTO rol)
>>>>>>> Stashed changes
        {
            try
            {
                SqlCommand command = new(DLVariables.usp_crearRol, connection);

                command.Parameters.Add(new SqlParameter(DLSPParameters.Nombre, SqlDbType.VarChar, 100)).Value = rol.Nombre;
<<<<<<< Updated upstream
                command.Parameters.Add(new SqlParameter(DLSPParameters.IdEstado, SqlDbType.Int)).Value = rol.Estado;
=======
                command.Parameters.Add(new SqlParameter(DLSPParameters.IdEstado, SqlDbType.Int)).Value = rol.IdEstado;
>>>>>>> Stashed changes

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

<<<<<<< Updated upstream
        public async Task<Response> AddRol(RolesDTO rol)
=======
        public async Task<Response> AddRol(RolDTO rol)
>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
        public async Task<Response> Editar(RolesDTO rolDTO)
=======
        public async Task<Response> Editar(RolDTO rolDTO)
>>>>>>> Stashed changes
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            try
            {
                var rol = await _context.Rols.FirstOrDefaultAsync(u => u.Nombre == rolDTO.Nombre);

                //usuario!.Contrasena = _utility.EncriptarContrasena(usuarioDTOEditar.Cedula);

                rol!.Nombre = rolDTO.Nombre;
<<<<<<< Updated upstream
                rol.IdEstado = rolDTO.Estado;
=======
               rol.IdEstado = rolDTO.IdEstado;
>>>>>>> Stashed changes

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
