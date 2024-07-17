using DataLayer.Pedidos.HistorialPedidos;
using DataLayer.Repositories.Parametros;
using EntityLayer.Models.DTO;
using EntityLayer.Responses;
using BusinessLayer.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Pedidos.HistorialPedidos
{
    public class HistorialPedidosService : IHistorialPedidosService
    {
        private readonly IHistorialPedidosRepository _historialPedidosRepository;
        private Response response = new();

        public HistorialPedidosService(IHistorialPedidosRepository historialPedidosRepository)
        {
            _historialPedidosRepository = historialPedidosRepository;
        }

        public async Task<Response> GetListPedidos()
        {
            List<PedidosDto> pedidos = [];
            try
            {
                response = await _historialPedidosRepository.GetListPedidos();
                if (response.Code == ResponseType.Success)
                {
                    var dataSet = (DataSet)response.Data!;
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        PedidosDto pedido = new()
                        {
                            FechaPedido = row[BLVariable.FechaPedido].ToString()!,
                            NombreUsuario  = row[BLVariable.NombreUsuario].ToString()!,
                            NombrePedido = row[BLVariable.NombreProducto].ToString()!,
                            PrecioProducto = decimal.Parse(row[BLVariable.PrecioProducto].ToString()!),
                            Cantidad = int.Parse(row[BLVariable.Cantidad].ToString()!)
                        };
                        pedidos.Add(pedido);
                    }
                    response.Data = pedidos;
                }
            }
            catch (Exception ex)
            {

                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;
            }
            return response;
        }
    }
}
