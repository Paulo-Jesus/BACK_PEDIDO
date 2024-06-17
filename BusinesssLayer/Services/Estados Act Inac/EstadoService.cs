
using BACK_PEDIDO.Models;
using EntityLayer.Model;
using EntityLayer.Response;
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
        Response response= new Response();

        public EstadoService(BdPedidosContext context)
        {
            _context = context;
         
        }

        public async Task<Response> GetListEstados()
        {
            try
            {

                List<EstadoDTO> estados =await _context.Estados
                               .Take(2)
                               .Select(r => new EstadoDTO(r.Nombre))
                               .ToListAsync();
                response.Data = estados;
                response.Message = "Lista Generada con éxito";
                response.Code = ResponseType.Success;
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
