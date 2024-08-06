using EntityLayer.Models.Entities;
using Microsoft.Data.SqlClient;
using Utility = DataLayer.Utilities.Utility;
using Microsoft.Extensions.Configuration;
using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Models.DTO;
using DataLayer.Database;
using EntityLayer.Models.Mappers;
using DataLayer.Common;

namespace DataLayer.Repositories.Proveedor
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly PedidosDatabaseContext _context;
        private readonly string _connectionString;
        private readonly ProductoMapper productoMapper = new();
        private readonly Response response = new PedidosDatabase().DatabaseConnection;
        private SqlConnection connection = new();
        List<ProveedorDTO> listaRestaurante = new();
        private readonly Utility _utility;
        SqlDataReader reader = null;

        public ProveedorRepository(PedidosDatabaseContext context, Utility utility, IConfiguration configuration)
        {
            _connectionString = Environment.GetEnvironmentVariable(DLVariables.ConnectionString)!;
            _context = context;
            _utility = utility;
        }


        public async Task<Response> GetRestaurantes()
        {
            connection = (SqlConnection)response.Data!;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand(DLStoredProcedures.SP_ObtenerTodosProveedores, connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        byte[] logotipoByte = (byte[])reader[DLVariables.Logotipo];

                        ProveedorDTO rs = new ProveedorDTO
                        {
<<<<<<< Updated upstream
=======
                            IdProveedor = Convert.ToInt32(reader[DLVariables.IdProveedor]),
>>>>>>> Stashed changes
                            Nombre = reader[DLVariables.Nombre].ToString()!,
                            Logotipo = Convert.ToBase64String(logotipoByte),
                        };
                        listaRestaurante.Add(rs);
                    }
                    response.Code = ResponseType.Success;
                    response.Message = String.Empty;
                    response.Data = listaRestaurante;

                    await connection.CloseAsync();
                }
            }
            return response;
        }

        public async Task<Response> registrar(ProveedorDTO restaurante)
        {
            connection = (SqlConnection)response.Data!;
            string contrasenia = _utility.EncriptarContrasena(restaurante.Contrasena);
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(DLStoredProcedures.SP_RegistrarProveedor, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue(DLVariables.colRUC, restaurante.Ruc);
                command.Parameters.AddWithValue(DLVariables.colNombre, restaurante.Nombre);
                command.Parameters.AddWithValue(DLVariables.colCorreo, restaurante.Correo);
                command.Parameters.AddWithValue(DLVariables.colTelefono, restaurante.Telefono);
                command.Parameters.AddWithValue(DLVariables.colDireccion, restaurante.Direccion);
                command.Parameters.AddWithValue(DLVariables.colLogotipo, Convert.FromBase64String(restaurante.Logotipo!));
                command.Parameters.AddWithValue(DLVariables.colContrasena, contrasenia);
                command.Parameters.AddWithValue(DLVariables.colIdRol, DLVariables.idRolProveedor);
                command.Parameters.AddWithValue(DLVariables.colIdEstado, DLVariables.idEstadoBloqueado);

                await connection.OpenAsync();
                int newResult = await command.ExecuteNonQueryAsync();

                if (!(newResult <= 0))
                {
                    response.Message = DLMessages.Msj_Registro_Exito;
                    response.Code = ResponseType.Success;
                    response.Data = String.Empty;
                    return response;
                }

                await connection.CloseAsync();
            }
            return response;
        }
    }
}
