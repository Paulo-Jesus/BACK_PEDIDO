using DataLayer.Repositories.Pedidos.Menu;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using DataLayer.Common;

namespace BusinessLayer.Services.Pedidos.Menu
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;
        private Response response = new();

        public MenuService(IMenuRepository menuRepository) 
        {
            _menuRepository = menuRepository;
        }

        public async Task<Response> RegistrarMenu(int idProveedor, string HoraInicio, string HoraFin, int[] IdProductos)
        {
            response = await _menuRepository.RegistrarMenu(idProveedor, HoraInicio, HoraFin, IdProductos);
            return response;
        }
        public async Task<Response> ActualizarMenu(int idProveedor, string HoraInicio, string HoraFin, int[] IdProductos)
        {
            response = await _menuRepository.ActualizarMenu(idProveedor, HoraInicio, HoraFin, IdProductos);
            return response;
        }

        public async Task<Response> TieneMenu(int idProveedor)
        {
            response = await _menuRepository.DatosMenu(idProveedor, DLVariables.SP_ParamType_EMD);
            return response;
        }

        public async Task<Response> DatosMenuCompleto(int idProveedor)
        {
            response = await _menuRepository.DatosMenuCompleto(idProveedor);
            return response;
        }
    }
}
