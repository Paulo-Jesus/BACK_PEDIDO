using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace BusinessLayer.Services.Parametros
{
    public interface IParametrosService
    {
        public Task<Response> Guardar(EmpresaDTO empresaDTO);
    }
}
