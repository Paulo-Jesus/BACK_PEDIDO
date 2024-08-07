using EntityLayer.Responses;

namespace DataLayer.Pedidos.HistorialPedidos
{
    public interface IHistorialPedidosRepository
    {
        public Task<Response> GetListPedidos();
    }
}
