using EntitiLayer.Models.Entities;
using EntityLayer.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Seguridad.DesbloquearCuenta
{
    public class ProveedorService : IProveedor
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        List<Proveedor> listaRestaurante = new();
        Response response = new();

        public ProveedorService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString(DataLayer.Common.DLVariables.ConnectionString)!;
        }


        public IEnumerable<Proveedor> GetRestaurantes()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand(DataLayer.Common.DLStoredProcedures.SP_ObtenerTodosProveedores, conn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        byte[] logotipoByte = (byte[])reader[DataLayer.Common.DLVariables.Logotipo];

                        Proveedor rs = new Proveedor
                        {
                            IdProveedor = Convert.ToInt32(reader[DataLayer.Common.DLVariables.IdProveedor]),
                            Ruc         = reader[DataLayer.Common.DLVariables.RUC].ToString()!,
                            Nombre      = reader[DataLayer.Common.DLVariables.Nombre].ToString()!,
                            Correo      = reader[DataLayer.Common.DLVariables.Correo].ToString()!,
                            Telefono    = reader[DataLayer.Common.DLVariables.Telefono].ToString()!,
                            Direccion   = reader[DataLayer.Common.DLVariables.Direccion].ToString()!,
                            Username    = reader[DataLayer.Common.DLVariables.Username].ToString()!,
                            Logotipo    = Convert.ToBase64String(logotipoByte),
                            Contrasena  = reader[DataLayer.Common.DLVariables.Contrasena].ToString()!,
                            IdRol       = Convert.ToInt32(reader[DataLayer.Common.DLVariables.IdRol]),
                            IdEstado    = Convert.ToInt32(reader[DataLayer.Common.DLVariables.IdEstado])
                        };
                        listaRestaurante.Add(rs);
                    }
                    conn.Close();
                }
            }
            return listaRestaurante!;
        }

        public Response registrar(Proveedor restaurante)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(DataLayer.Common.DLStoredProcedures.SP_RegistrarProveedor, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue(DataLayer.Common.DLVariables.colRUC, restaurante.Ruc);
                command.Parameters.AddWithValue(DataLayer.Common.DLVariables.colNombre, restaurante.Nombre);
                command.Parameters.AddWithValue(DataLayer.Common.DLVariables.colCorreo, restaurante.Correo);
                command.Parameters.AddWithValue(DataLayer.Common.DLVariables.colTelefono, restaurante.Telefono);
                command.Parameters.AddWithValue(DataLayer.Common.DLVariables.colUsername, restaurante.Username);
                command.Parameters.AddWithValue(DataLayer.Common.DLVariables.colDireccion, restaurante.Direccion);
                command.Parameters.AddWithValue(DataLayer.Common.DLVariables.colLogotipo, Convert.FromBase64String(restaurante.Logotipo!));
                command.Parameters.AddWithValue(DataLayer.Common.DLVariables.colContrasena, restaurante.Contrasena);
                command.Parameters.AddWithValue(DataLayer.Common.DLVariables.colIdRol, DataLayer.Common.DLVariables.idRolProveedor);
                command.Parameters.AddWithValue(DataLayer.Common.DLVariables.colIdEstado, DataLayer.Common.DLVariables.idEstadoBloqueado);

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    response.Message = DataLayer.Common.DLMessages.Msj_Registro_Exito;
                    response.Code = ResponseType.Success;
                    return response;
                }
            }
            return response;
        }

    }
}
