using BusinesssLayer.Interfaces;
using DataLayer.COMMON;
using EntityLayer.Models;
using EntityLayer.Responses;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesssLayer
{
    public class RestauranteService : IRestaurantes
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        List<Restaurante> listaRestaurante = new();
        Response response = new();

        public RestauranteService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString(Common.nombreConexion)!;
        }


        public IEnumerable<Restaurante> GetRestaurantes()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString)) {
                conn.Open();

                using (SqlCommand command = new SqlCommand(Common.SP_ObtenerTodosRestaurantes,conn)) {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader reader = command.ExecuteReader();
                    
                    while (reader.Read()) { 
                        Restaurante rs = new Restaurante { 
                            RUC       = reader[Common.ruc].ToString(),
                            Nombre    = reader[Common.nombreR].ToString(),
                            Correo    = reader[Common.correoR].ToString(),
                            Telefono  = reader[Common.telefono].ToString(),
                            Direccion = reader[Common.direccion].ToString(),
                            Username  = reader[Common.username].ToString(),
                            Logotipo  = reader[Common.logotipo].ToString(),
                            Contrasena     = reader[Common.claveR].ToString(),
                            IdRol     = Convert.ToInt32(reader[Common.idRol]),
                            IdEstado  = Convert.ToInt32(reader[Common.idEstado])
                        };
                        listaRestaurante.Add(rs);
                    }
                    conn.Close();
                }
            }
            return listaRestaurante!;
        }

        public Response registrar(Restaurante restaurante)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString)) {
                SqlCommand command = new SqlCommand(Common.SP_RegistrarRestaurante, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue(Common.colRuc,restaurante.RUC);
                command.Parameters.AddWithValue(Common.colNombreR, restaurante.Nombre);
                command.Parameters.AddWithValue(Common.colCorreoR, restaurante.Correo);
                command.Parameters.AddWithValue(Common.colTelefonoR, restaurante.Telefono);
                command.Parameters.AddWithValue(Common.colUsernameR, restaurante.Username);
                command.Parameters.AddWithValue(Common.colDireccionR, restaurante.Direccion);
                command.Parameters.AddWithValue(Common.colLogotipoR, Convert.FromBase64String(restaurante.Logotipo!));
                command.Parameters.AddWithValue("@Contrasena", restaurante.Contrasena);
                command.Parameters.AddWithValue(Common.colIdRolR, Common.rolProveedor);
                command.Parameters.AddWithValue(Common.colIdEstado, Common.idEstadoActivo);

                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) {
                    response.Message = Common.msjExito;
                    response.Code = ResponseType.Success;
                    return response;
                }
            }
            return response;
        }

        public Byte[] convertirAByte(String imageString)
        {
            byte[] imagenByte;

            imagenByte = Convert.FromBase64String(imageString);

            return imagenByte; 
        } 

    }
}
