using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace BusinessLayer.Services.Pedidos.Menu
{
    public interface IMenuService
    {
        Task<Response> RegistrarMenu(int idProveedor, string HoraInicio, string HoraFin, int[] IdProductos);
        Task<Response> ActualizarMenu(int IdProveedor, string HoraInicio, string HoraFin, int[] IdProductos);
        Task<Response> TieneMenu(int idProveedor);
        Task<Response> DatosMenuCompleto(int idProveedor);
    }
}
