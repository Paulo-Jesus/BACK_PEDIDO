using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Pedidos.InsertarPedido
{
    public interface IPedidoService
    {
        public Task<Response> InsertarPedido(PedidosInsertarDto pedido);
    }
}
