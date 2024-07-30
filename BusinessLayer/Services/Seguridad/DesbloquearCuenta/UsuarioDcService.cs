using DataLayer.Common;
using DataLayer.Repositories.Pedidos.Productos;
using DataLayer.Repositories.Seguridad.DesbloquearCuenta;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
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
    public class UsuarioDcService : IUsuarioDcService
    {
        private readonly IUsuarioDcRepository _usuarioDcRepository;
        private Response response = new();

        public UsuarioDcService(IUsuarioDcRepository usuarioDcRepository)
        {
            _usuarioDcRepository = usuarioDcRepository;
        }

        public async Task<Response> obtenerTodosUsuarios()
        {
            response = await _usuarioDcRepository.obtenerTodosUsuarios();
            return response;
        }

        public async Task<Response> buscarUsuarioBloqueado(string Correo)
        {
            response = await _usuarioDcRepository.buscarUsuarioBloqueado(Correo);
            return response;
        }

        public async Task<Response> DesbloquearUsuario(string Correo)
        {
            response = await _usuarioDcRepository.DesbloquearUsuario(Correo);
            return response;
        }

    }
}
