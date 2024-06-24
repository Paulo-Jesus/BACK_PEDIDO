using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Interfaces
{
    public interface IEmpresaService
    {
        public Task<Response> ObtenerEmpresasEF();
        public Task<Response> RegistrarEmpresaADO(EmpresaDTO empresaDTO);
    }
}
