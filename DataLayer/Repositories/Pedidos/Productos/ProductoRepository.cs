using DataLayer.Database;
using DataLayer.Utilities;
using EntityLayer.Models.DTO;
using EntityLayer.Models.Mappers;
using EntityLayer.Responses;
using Microsoft.Data.SqlClient;
using EntitiLayer.Models.Entities;
using DataLayer.Common;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories.Pedidos.Productos
{
    public class ProductoRepository : IProductoRepository
    {

        private readonly PedidosDatabaseContext _context;
        private readonly ProductoMapper productoMapper = new();
        private readonly Response response = new PedidosDatabase().DatabaseConnection;
        private SqlConnection connection = new();
        private readonly Utility _utility;

        public ProductoRepository(PedidosDatabaseContext context, Utility utility)
        {
            _context = context;
            _utility = utility;
        }

        public async Task<Response> ObtenerProductos()
        {
            try
            {
                List<Producto> productos = await _context.Productos
                    .Where(u => u.IdEstado == 1)
                    .ToListAsync();
                List<ProductoDTO> productoDTO = productos.Select(productos => productoMapper.ProductoToProductoDTO(productos)).ToList();

                if (productos.Count < 1)
                {
                    response.Code = ResponseType.Success;
                    response.Message = DLMessages.NoUsuariosRegistrados;
                    response.Data = null;

                    return response;
                }

                response.Code = ResponseType.Success;
                response.Message = DLMessages.ListadoUsuarios;
                response.Data = productoDTO;
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;
            }
            return response;
        }

        public async Task<Response> IngresarProductos(ProductoDTO productoDTO)
        {
            try
            {
                Producto nuevoProducto = productoMapper.ProductoDTOToProducto(productoDTO);
                nuevoProducto.Imagen = Convert.ToBase64String(productoDTO.Base64);

                _context.Productos.Add(nuevoProducto);
                await _context.SaveChangesAsync();

                response.Code = ResponseType.Success;
                response.Message = DLMessages.ListadoUsuarios;
                response.Data = productoDTO;
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;
            }
            return response;
        }
    }
}
