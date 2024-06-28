using EntityLayer.Models.DTO;
using EntityLayer.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace EntityLayer.Models.Mappers
{
    [Mapper]
    public partial class MenuMapper
    {
        public partial MenuDTO MenuToMenuDTO(Menu menu);
        public partial Menu MenuDTOToMenu(MenuDTO menuDTO);
    }
}
