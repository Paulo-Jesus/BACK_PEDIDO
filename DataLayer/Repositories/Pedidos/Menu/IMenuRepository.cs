using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace DataLayer.Repositories.Pedidos.Menu
{
    public interface IMenuRepository
    {
        Task<Response> RegistrarMenu(int idProveedor, string HoraInicio, string HoraFin, int[] IdProductos);
        Task<Response> ActualizarMenu(int IdProveedor, string HoraInicio, string HoraFin, int[] IdProductos);
        Task<Response> DatosMenu(int idProveedor, string TipoTrx);
    }
}
