using EntityLayer.Responses;

namespace BusinessLayer.Services.Interfaces
{
    public interface IEstadoService
    {
        public Task<Response> ObtenerEstadosEF();
    }
}
