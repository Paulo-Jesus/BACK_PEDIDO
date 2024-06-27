﻿using EntityLayer.Models.DTO;
using EntityLayer.Responses;

namespace DataLayer.Repositories.Pedidos.Menu
{
    public interface IMenuRepository
    {
        Task<Response> RegistrarMenu(int idProveedor, string HoraInicio, string HoraFin, int[] IdProductos);
        Task<Response> ExisteMenu(int idProveedor);
    }
}
