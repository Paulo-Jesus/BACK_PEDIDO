using DataLayer.Pedidos.InsertarPedido;
using DataLayer.Repositories.Pedidos.Menu;
using EntityLayer.Models.DTO;
using EntityLayer.Models.Entities;
using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Pedidos.InsertarPedido
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidosRepository _pedidoRepository;
        private Response response = new();

        public PedidoService(IPedidosRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<Response> InsertarPedido(PedidosInsertarDto pedido)
        {
            response = await _pedidoRepository.InsertarPedido(pedido);
            return response;
        }
    }
}
