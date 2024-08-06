using EntityLayer.Models.DTO;
using EntityLayer.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace EntityLayer.Models.Mappers
{
    [Mapper]
    public partial class RolMapper
    {
        public partial RolDTO RoltoRolesDTO(Rol rol);
        public partial Rol RolesDTOToRol(RolDTO rolDTO);
    }
}
