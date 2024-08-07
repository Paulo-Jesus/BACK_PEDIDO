using EntityLayer.Models.DTO;
using EntityLayer.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace EntityLayer.Models.Mappers
{
    [Mapper]
    public partial class ProductoMapper
    {
        public partial ProductoDTO ProductoToProductoDTO(Producto producto);
        public partial Producto ProductoDTOToProducto(ProductoDTO productoDTO);
    }
}