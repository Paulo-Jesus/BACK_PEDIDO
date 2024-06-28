using EntityLayer.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using EntityLayer.Responses;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Models.DTO;

namespace BusinessLayer.Services.Seguridad.DesbloquearCuenta
{
    public class ProveedorService : IProveedor
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        List<ProveedorDTO> listaRestaurante = new();
        Response response = new();

        public ProveedorService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString(DataLayer.Common.DLVariables.ConnectionString)!;
        }


        public IEnumerable<ProveedorDTO> GetRestaurantes()
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
                        byte[] logotipoByte = (byte[])reader[BusinessLayer.Common.BLRows.Logotipo];

                        ProveedorDTO rs = new ProveedorDTO
                        {                         
                            Nombre = reader[BusinessLayer.Common.BLRows.Nombre].ToString()!,
                            Logotipo = Convert.ToBase64String(logotipoByte),
                        };
                        listaRestaurante.Add(rs);
                    }
                    conn.Close();
                }
            }
            return listaRestaurante;
        }

        public Response registrar(ProveedorDTO restaurante)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(DataLayer.Common.DLStoredProcedures.SP_RegistrarProveedor, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue(BusinessLayer.Common.BLRows.colRUC, restaurante.Ruc);
                command.Parameters.AddWithValue(BusinessLayer.Common.BLRows.colNombre, restaurante.Nombre);
                command.Parameters.AddWithValue(BusinessLayer.Common.BLRows.colCorreo, restaurante.Correo);
                command.Parameters.AddWithValue(BusinessLayer.Common.BLRows.colTelefono, restaurante.Telefono);
                command.Parameters.AddWithValue(BusinessLayer.Common.BLRows.colDireccion, restaurante.Direccion);
                command.Parameters.AddWithValue(BusinessLayer.Common.BLRows.colLogotipo, Convert.FromBase64String(restaurante.Logotipo!));
                command.Parameters.AddWithValue(BusinessLayer.Common.BLRows.colContrasena, restaurante.Contrasena);
                command.Parameters.AddWithValue(BusinessLayer.Common.BLRows.colIdRol, BusinessLayer.Common.BLRows.idRolProveedor);
                command.Parameters.AddWithValue(BusinessLayer.Common.BLRows.colIdEstado, BusinessLayer.Common.BLRows.idEstadoBloqueado);

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
