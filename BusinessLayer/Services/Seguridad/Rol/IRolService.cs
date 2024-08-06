
using EntityLayer.Models.DTO;
using EntityLayer.Responses;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Seguridad.Rol
{
    public interface IRolService
    {
        public Task<Response> Lista();

        
    }
}
