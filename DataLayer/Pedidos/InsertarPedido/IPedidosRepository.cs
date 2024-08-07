using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Pedidos.InsertarPedido
{
    public interface IPedidosRepository
    {
        public Task<Response> InsertarPedido(PedidosInsertarDto pedido);
    }
}
