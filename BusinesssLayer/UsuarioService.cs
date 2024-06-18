using AutoMapper;
using BACK_PEDIDO.Models;
using BusinesssLayer.Interfaces;
using DataLayer.COMMON;
using EntityLayer.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinesssLayer
{
    public class UsuarioService : IUsuario
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private readonly List<UsuarioBlockDTO> lista = new();
        private readonly IMapper _mapper;

        public UsuarioService(IConfiguration configuration, IMapper mapper)
        {
            _connectionString = configuration.GetConnectionString(Common.nombreConexion)!;
            _configuration = configuration;
            _mapper = mapper;
        }
        public IEnumerable<UsuarioBlockDTO> obtenerTodosUsuarios()
        {          
            using (SqlConnection conn = new (_connectionString))
            {
                SqlCommand command = new SqlCommand(Common.SP_ObtenerTodosBloqueados, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UsuarioBlockDTO usuario = new UsuarioBlockDTO
                        {
                            Nombre = reader[Common.nombre].ToString()!,
                            NombreUsuario = reader[Common.nombreUsuario].ToString()!,
                            IdEstado = Convert.ToInt32(reader[Common.estadoId])
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
                if (validarNombreUsuarioVacio(nombreUsuario)) {
                    using (SqlConnection conn = new(_connectionString)) {
                        SqlCommand command = new SqlCommand(Common.SP_Buscar_NombreUsuario, conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(Common.nombreUsuarioColumna, nombreUsuario);
                        conn.Open();

                        command.ExecuteNonQuery();

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read()) {
                            user = new UsuarioBlockDTO
                            {
                                Nombre = reader[Common.nombre].ToString()!,
                                NombreUsuario = reader[Common.nombreUsuario].ToString()!,
                                IdEstado = Convert.ToInt32(reader[Common.estadoId])
                            };
                            return user;
                        }
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
            return user;
        }

        public String DesbloquearUsuario(string nombreUsuario ) {
            String msj = String.Empty;
            if (validarNombreUsuarioVacio(nombreUsuario))
            {
                try
                {
                    using (SqlConnection conn = new(_connectionString))
                    {
                        SqlCommand command = new SqlCommand(Common.SP_Desbloquear_Usuario, conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue(Common.nombreUsuarioColumna, nombreUsuario);

                        conn.Open();

                        command.ExecuteNonQuery();

                        msj = Common.msjUserUnBlock;
                    }
                }
                catch (Exception ex) { 
                    msj = Common.msjUserNoUnBlock + ex.ToString();
                }
            }
            return msj;
        }

        
        public UsuarioLoginDTO validarLogin(UsuarioLoginDTO usuario)
        {
            UsuarioLoginDTO user = new UsuarioLoginDTO();
            try
            {
                using (SqlConnection conn = new(_connectionString)) {
                    SqlCommand command = new SqlCommand(Common.SP_ValidarLogin,conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Common.nombreUsuarioColumna,usuario.NombreUsuario);
                    command.Parameters.AddWithValue(Common.claveColumna,usuario.Clave);

                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read()) {
                        user = new UsuarioLoginDTO { 
                            NombreUsuario = reader[Common.nombreUsuario].ToString(),
                            Clave = reader[Common.clave].ToString()
                        };
                        return user;
                        //var token = GenerarToken(usuario.NombreUsuario!);
                    }
                }
            }
            catch (Exception ex) { 
                Console.WriteLine (ex.ToString());
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
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

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
