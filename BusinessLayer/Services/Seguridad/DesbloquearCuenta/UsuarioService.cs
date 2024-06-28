using EntityLayer.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Seguridad.DesbloquearCuenta
{
    public class UsuarioService : IUsuario
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly List<UsuarioBlockDTO> lista = new();

        public UsuarioService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(DataLayer.Common.DLVariables.ConnectionString)!;
            _configuration = configuration;
        }

        public IEnumerable<UsuarioBlockDTO> obtenerTodosUsuarios()
        {
            using (SqlConnection conn = new(_connectionString))
            {
                SqlCommand command = new SqlCommand(DataLayer.Common.DLStoredProcedures.SP_Obtener_Todos_Usuario_Bloqueados, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UsuarioBlockDTO usuario = new UsuarioBlockDTO
                        {
                            Nombre = reader.GetString(0),
                            Correo = reader.GetString(1),
                            NombreRol = reader.GetString(2),
                            NombreEstado = reader.GetString(3),
                        };
                        lista.Add(usuario);
                    }
                }
            }
            return lista;
        }

        public UsuarioBlockDTO buscarUsuarioBloqueado(string correo)
        {
            UsuarioBlockDTO user = new UsuarioBlockDTO();
            try
            {
                if (validarCampoVacio(correo))
                {
                    using (SqlConnection conn = new(_connectionString))
                    {
                        SqlCommand command = new SqlCommand(DataLayer.Common.DLStoredProcedures.SP_Buscar_Por_Correo, conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(BusinessLayer.Common.BLRows.CorreoU, correo);
                        conn.Open();

                        command.ExecuteNonQuery();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            user = new UsuarioBlockDTO
                            {
                                Nombre = reader.GetString(0),
                                Correo = reader.GetString(1),
                                NombreRol = reader.GetString(2),
                                NombreEstado = reader.GetString(3),
                            };
                            Console.WriteLine(user.ToString());
                            return user;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return user;
        }

        public String DesbloquearUsuario(string correo)
        {
            String msj = String.Empty;
            if (validarCampoVacio(correo))
            {
                try
                {
                    using (SqlConnection conn = new(_connectionString))
                    {
                        SqlCommand command = new SqlCommand(DataLayer.Common.DLStoredProcedures.SP_Desbloquear_Usuario, conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(BusinessLayer.Common.BLRows.CorreoU, correo);

                        conn.Open();

                        command.ExecuteNonQuery();

                        msj = DataLayer.Common.DLMessages.Msj_Usuario_Unblock;
                    }
                }
                catch (Exception ex)
                {
                    msj = DataLayer.Common.DLMessages.Msj_Usuario_block + ex.ToString();
                }
            }
            return msj;
        }


        public bool validarCampoVacio(string texto)
        {
            bool validar = false;
            if (!string.IsNullOrWhiteSpace(texto))
            {
                validar = true;
            }
            return validar;
        }

    }
}
