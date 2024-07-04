using EntityLayer.Responses;

namespace BusinessLayer.Services.Pedidos.HistorialPedidos
{
    public interface IHistorialPedidosService
    {
        public Task<Response> GetListPedidos();
    }
}
