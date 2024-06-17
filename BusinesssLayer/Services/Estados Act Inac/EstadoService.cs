
using BACK_PEDIDO.Models;
using EntityLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesssLayer.Services.Estados_Act_Inac
{
    public class EstadoService
    {

        private readonly BdPedidosContext _context;
        Modelo modelo = new Modelo();

        public EstadoService(BdPedidosContext context)
        {
            _context = context;
         
        }

        public async Task<Modelo> GetListEstados()
        {
            try
            {

                List<EstadoDTO> estados =await _context.Estados
                               .Take(2)
                               .Select(r => new EstadoDTO(r.Nombre))
                               .ToListAsync();
                modelo.data = estados;
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
