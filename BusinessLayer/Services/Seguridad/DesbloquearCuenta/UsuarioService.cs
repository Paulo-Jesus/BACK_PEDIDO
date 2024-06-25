using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using EntityLayer.Models.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
                            Nombre = reader[DataLayer.Common.DLVariables.NombreU].ToString()!,
                            NombreUsuario = reader[DataLayer.Common.DLVariables.UsernameU].ToString()!,
                            IdEstado = Convert.ToInt32(reader[DataLayer.Common.DLVariables.IdEstado])
                        };
                        lista.Add(usuario);
                    }
                }
            }
            return lista;
        }

        public UsuarioBlockDTO buscarUsuarioBloqueado(string nombreUsuario)
        {
            UsuarioBlockDTO user = new UsuarioBlockDTO();
            try
            {
                if (validarNombreUsuarioVacio(nombreUsuario))
                {
                    using (SqlConnection conn = new(_connectionString))
                    {
                        SqlCommand command = new SqlCommand(DataLayer.Common.DLStoredProcedures.SP_Buscar_Por_Usuario, conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(DataLayer.Common.DLVariables.colUsernameU, nombreUsuario);
                        conn.Open();

                        command.ExecuteNonQuery();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            user = new UsuarioBlockDTO
                            {
                                Nombre = reader[DataLayer.Common.DLVariables.NombreU].ToString()!,
                                NombreUsuario = reader[DataLayer.Common.DLVariables.UsernameU].ToString()!,
                                IdEstado = Convert.ToInt32(reader[DataLayer.Common.DLVariables.IdEstado])
                            };
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

        public String DesbloquearUsuario(string nombreUsuario)
        {
            String msj = String.Empty;
            if (validarNombreUsuarioVacio(nombreUsuario))
            {
                try
                {
                    using (SqlConnection conn = new(_connectionString))
                    {
                        SqlCommand command = new SqlCommand(DataLayer.Common.DLStoredProcedures.SP_Desbloquear_Usuario, conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(DataLayer.Common.DLVariables.colUsernameU, nombreUsuario);

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


        public UsuarioLoginDTO validarLogin(UsuarioLoginDTO usuario)
        {
            UsuarioLoginDTO user = new UsuarioLoginDTO();
            try
            {
                using (SqlConnection conn = new(_connectionString))
                {
                    SqlCommand command = new SqlCommand(DataLayer.Common.DLStoredProcedures.SP_Validar_Login, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(DataLayer.Common.DLVariables.colUsernameU, usuario.NombreUsuario);
                    command.Parameters.AddWithValue(DataLayer.Common.DLVariables.colClaveU, usuario.Clave);

                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        user = new UsuarioLoginDTO
                        {
                            NombreUsuario = reader[DataLayer.Common.DLVariables.UsernameU].ToString(),
                            Clave = reader[DataLayer.Common.DLVariables.ContrasenaU].ToString()
                        };
                        return user;
                        //var token = GenerarToken(usuario.NombreUsuario!);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return user;
        }

        public bool validarNombreUsuarioVacio(string nombre)
        {
            bool validar = false;
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                validar = true;
            }
            return validar;
        }

        private string GenerarToken(string nombreUsuario)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings["key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, nombreUsuario),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    },
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
