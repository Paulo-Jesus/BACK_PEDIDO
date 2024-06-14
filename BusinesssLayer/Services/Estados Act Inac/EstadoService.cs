using AutoMapper;
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
        private readonly IMapper _mapper;
        Modelo modelo = new Modelo();

        public EstadoService(BdPedidosContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Modelo> GetListEstados()
        {
            try
            {
                
                var estados = await _context.Estados.ToListAsync();
                var estadoDTO = _mapper.Map<IEnumerable<EstadoDTO>>(estados);


                modelo.data = estadoDTO;
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
