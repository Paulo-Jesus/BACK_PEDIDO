using BACK_PEDIDO.Models;
using EntityLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesssLayer.Services.Roles
{
    public class RolService
    {

        private readonly BdPedidosContext _context;
        Modelo modelo= new Modelo();

        public RolService(BdPedidosContext context)
        {
            _context = context;

        }



        public async Task<Modelo> GetListRoles()
        {
            try
            {
                List<RolesDTO> lisRol = await _context.Rols.Take(4).Select(r => new RolesDTO(r.Nombre)).ToListAsync();
                modelo.data = lisRol;
                modelo.message = "Lista Generada con éxito";
                modelo.error = false;
                return modelo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
