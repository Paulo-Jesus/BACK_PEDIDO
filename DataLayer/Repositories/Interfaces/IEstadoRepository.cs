using EntityLayer.Responses;

namespace DataLayer.Repositories.Interfaces
{
    public interface IEstadoRepository
    {
        public Task<Response> ObtenerEstadosEF();
    }
}
