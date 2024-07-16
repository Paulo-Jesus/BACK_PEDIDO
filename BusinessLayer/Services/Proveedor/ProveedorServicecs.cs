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
using DataLayer.Repositories.Proveedor;
using DataLayer.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BusinessLayer.Services.Proveedor
{
    public class ProveedorService : IProveedorService
    {
        private readonly IProveedorRepository _proveedorRepository;
        private Response response = new();

        public ProveedorService(IProveedorRepository proveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
        }

        public async Task<Response> GetRestaurantes()
        {
            response = await _proveedorRepository.GetRestaurantes();
            return response;
        }

        public async Task<Response> registrar(ProveedorDTO restaurante)
        {
            response = await _proveedorRepository.registrar(restaurante);
            return response;

        }
    }

}
