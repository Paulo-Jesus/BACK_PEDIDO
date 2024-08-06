using DataLayer.Repositories.RolRepository;
using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Seguridad.Rol
{
    public class RolService: IRolService

    {
        private readonly IRolRepository _rolRepository;
        private Response response = new();

        
        public async Task<Response> Lista()
        {
            response = await _rolRepository.Lista();
            return response;
        }

    }
}
