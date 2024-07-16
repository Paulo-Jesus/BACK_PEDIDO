using EntityLayer.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Responses;
using EntityLayer.Models.DTO;

namespace BusinessLayer.Services.Proveedor
{
    public interface IProveedorService
    {
        Task<Response> GetRestaurantes();
        Task<Response> registrar(ProveedorDTO proveedor);
    }
}
