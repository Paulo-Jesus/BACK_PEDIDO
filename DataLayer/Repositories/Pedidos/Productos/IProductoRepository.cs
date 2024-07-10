using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace DataLayer.Repositories.Pedidos.Productos
{
    public interface IProductoRepository
    {
        Task<Response> ObtenerProductos(int IdProveedor);
        Task<Response> IngresarProducto(ProductoDTO productoDTO);
        Task<Response> ActualizarProducto(ProductoDTO productoDTO);
        Task<Response> EstadoProducto(int productoId, int estado);
    }
}
