using EntityLayer.Models.DTO;
using EntityLayer.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace EntityLayer.Models.Mappers
{
    [Mapper]
    public partial class EstadoMapper
    {
        public partial EstadoDTO EstadoToEstadoDTO(Estado estado);
    }
}
