using AutoMapper;
using BACK_PEDIDO.Models;
using BusinesssLayer.Interfaces;
using DataLayer.COMMON;
using EntityLayer.DTO;
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
        private readonly List<UsuarioBlockDTO> lista = new();
        private readonly IMapper _mapper;

        public UsuarioService(IConfiguration configuration, IMapper mapper)
        {
            _connectionString = configuration.GetConnectionString(Common.nombreConexion)!;
            _mapper = mapper;
        }
        public IEnumerable<UsuarioBlockDTO> obtenerTodosUsuarios()
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
                using (SqlConnection conn = new SqlConnection(_connectionString)) {
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
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
            return user;
        }


    }
}
