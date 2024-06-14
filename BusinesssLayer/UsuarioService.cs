using BACK_PEDIDO.Models;
using BusinesssLayer.Interfaces;
using DataLayer.COMMON;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesssLayer
{
    public class UsuarioService : IUsuario
    {
        private readonly string _connectionString;
        private readonly List<Usuario> lista = new();

        public UsuarioService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(Common.nombreConexion); 
        }
        public IEnumerable<Usuario> obtenerTodosUsuarios()
        {          
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(Common.SP_ObtenerTodosBloqueados, conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Usuario usuario = new Usuario
                        {
                            Nombre = reader[Common.nombre].ToString()!,
                            NombreUsuario = reader[Common.nombreUsuario].ToString()!,
                            EstadoIdEstado = Convert.ToInt32(reader[Common.estadoId])
                        };
                        lista.Add(usuario);
                    }
                }
            }        
            return lista;        
        }

        public Usuario buscarUsuarioBloqueado(string nombreUsuario)
        {
            Usuario personaEncontrada = new Usuario();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString)) {
                    SqlCommand command = new SqlCommand(Common.SP_Buscar_NombreUsuario, conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue(Common.nombreUsuarioColumna, nombreUsuario);
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read()) {
                        personaEncontrada = new Usuario
                        {
                            Nombre = reader[Common.nombre].ToString()!,
                            NombreUsuario = reader[Common.nombreUsuario].ToString()!,
                            EstadoIdEstado = Convert.ToInt32(reader[Common.estadoId])
                        };
                        return personaEncontrada;
                    }
                }
            }
            catch (Exception ex) { 
            
            }
            return personaEncontrada!;
        }


    }
}
