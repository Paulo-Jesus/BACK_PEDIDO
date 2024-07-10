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

                List<EstadoDTO> estados = await _context.Estados
                               .Take(2)
                               .Select(r => new EstadoDTO(r.Nombre))
                               .ToListAsync();
                response.Data = estados;
                response.Message = "Lista Generada con éxito";
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

                List<RolesDTO> lisRol = await _context.Rols.Select(r => new RolesDTO(r.Nombre, r.IdEstado)).ToListAsync();

                response.Data = lisRol;
                response.Message = "param.Message";
                response.Code = ResponseType.Success;

            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task MetodoAgregar(SqlConnection connection, RolesDTO rol)
        {
            try
            {
                SqlCommand command = new("usp_crearRol", connection);

                command.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 100)).Value = rol.Nombre;
                command.Parameters.Add(new SqlParameter("@IdEstado", SqlDbType.Int)).Value = rol.Estado;

                command.CommandType = CommandType.StoredProcedure;

                int num = await command.ExecuteNonQueryAsync();

                if (num > 0)
                {
                    response.Code = ResponseType.Success;
                    response.Message = "Rol Agregado";
                    response.Data = null;
                }
                else
                {
                    response.Code = ResponseType.Error;
                    response.Message = "Rol No Agregado";
                    response.Data = null;
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<Response> AddRol(RolesDTO rol)
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

        public async Task<Response> Editar(RolesDTO rolDTO)
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            try
            {
                var rol = await _context.Rols.FirstOrDefaultAsync(u => u.Nombre == rolDTO.Nombre);

                //usuario!.Contrasena = _utility.EncriptarContrasena(usuarioDTOEditar.Cedula);

                rol!.Nombre = rolDTO.Nombre;
                rol.IdEstado = rolDTO.Estado;

                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                response.Code = ResponseType.Success;
                response.Message = "Rol Editado";
                response.Data = null;
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();

                response.Code = ResponseType.Error;
                response.Message = "Rol No Editado";
                response.Data = ex.Data;
            }
            return response;
        }
    }
}
