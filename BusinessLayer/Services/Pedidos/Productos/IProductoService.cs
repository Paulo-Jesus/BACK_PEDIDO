using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace BusinessLayer.Services.Pedidos.Productos
{
    public interface IProductoService
    {
        Task<Response> ObtenerProductos(int IdProveedor);
        Task<Response> IngresarProducto(ProductoDTO productoDTO);
        Task<Response> ActualizarProducto(ProductoDTO productoDTO);
        Task<Response> EstadoProducto(int productoId, int estado);
    }
}
