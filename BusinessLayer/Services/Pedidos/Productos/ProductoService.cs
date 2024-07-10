using DataLayer.Repositories.Pedidos.Productos;
using DataLayer.Repositories.Seguridad.Usuarios;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace BusinessLayer.Services.Pedidos.Productos
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        private Response response = new();

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<Response> ObtenerProductos(int IdProveedor)
        {
            response = await _productoRepository.ObtenerProductos(IdProveedor);
            return response;
        }

        public async Task<Response> IngresarProducto(ProductoDTO productoDTO)
        {
            response = await _productoRepository.IngresarProducto(productoDTO);
            return response;
        }

        public async Task<Response> ActualizarProducto(ProductoDTO productoDTO)
        {
            response = await _productoRepository.ActualizarProducto(productoDTO);
            return response;
        }

        public async Task<Response> EstadoProducto(int productoId, int estado)
        {
            response = await _productoRepository.EstadoProducto(productoId, estado);
            return response;
        }
    }
}
