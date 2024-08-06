using EntityLayer.Models.DTO;
using EntityLayer.Models.Entities;
using Riok.Mapperly.Abstractions;

namespace EntityLayer.Models.Mappers
{
    [Mapper]
    public partial class EmpresaMapper
    {
        public partial EmpresaDTO EmpresaToEmpresaDTO(Empresa empresa);
        public partial Empresa EmpresaDTOToEmpresa(EmpresaDTO empresaDTO);
    }
}
