using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace DataLayer.Repositories.Proveedor
{
    public interface IProveedorRepository
    {
        Task<Response> GetRestaurantes();
        Task<Response> registrar(ProveedorDTO proveedor);
    }
}
