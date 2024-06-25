using EntitiLayer.Models.Entities;
using EntityLayer.Models.DTO;
using Riok.Mapperly.Abstractions;

namespace EntityLayer.Models.Mappers
{
    [Mapper]
    public partial class UsuarioMapper
    {
        public partial UsuarioDTO UsuarioToUsuarioDTO(Usuario usuario);
    }
}
