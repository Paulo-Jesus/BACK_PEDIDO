using EntityLayer.Models.DTO;
using EntityLayer.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace EntityLayer.Models.Mappers
{
    [Mapper]
    public partial class UsuarioMapper
    {
        public partial UsuarioDTO UsuarioToUsuarioDTO(Usuario usuario);
    }
}
