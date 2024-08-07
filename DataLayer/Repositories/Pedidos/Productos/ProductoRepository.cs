﻿using DataLayer.Database;
using Utility = DataLayer.Utilities.Utility;
using EntityLayer.Models.DTO;
using EntityLayer.Models.Mappers;
using EntityLayer.Responses;
using Microsoft.Data.SqlClient;
using EntityLayer.Models.Entities;
using DataLayer.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace DataLayer.Repositories.Pedidos.Productos
{
    public class ProductoRepository : IProductoRepository
    {

        private readonly PedidosDatabaseContext _context;
        private readonly ProductoMapper productoMapper = new();
        private readonly Response response = new PedidosDatabase().DatabaseConnection;
        private SqlConnection connection = new();
        private readonly Utility _utility;
        SqlDataReader reader = null;

        public ProductoRepository(PedidosDatabaseContext context, Utility utility)
        {
            _context = context;
            _utility = utility;
        }

        public async Task<Response> ObtenerProductos(int IdProveedor)
        {
            connection = (SqlConnection)response.Data!;
            try
            {
                SqlCommand command = new SqlCommand(DLStoredProcedures.SP_GeneralValidation, connection);
                command.Parameters.Add(new SqlParameter(DLSPParameters.Type, SqlDbType.VarChar, 40)).Value = DLVariables.SP_ParamType_IP;
                command.Parameters.Add(new SqlParameter(DLSPParameters.IdProveedor, SqlDbType.Int)).Value = IdProveedor;
                command.CommandType = CommandType.StoredProcedure;
                reader = await command.ExecuteReaderAsync();

                List<ProductoDTO> productosDTO = new List<ProductoDTO>();

                while (await reader.ReadAsync())
                {
                    if (reader.GetInt32("IdEstado") == 1)
                    {

                        ProductoDTO productoDTO = new ProductoDTO();
                        productoDTO.IdProducto = reader.GetInt32(reader.GetOrdinal(DLVariables.IdProducto));
                        productoDTO.Nombre = reader.GetString(reader.GetOrdinal(DLVariables.Nombre));
                        productoDTO.Descripcion = reader.GetString(reader.GetOrdinal(DLVariables.Descripcion));
                        productoDTO.Precio = Convert.ToDouble(reader.GetDecimal(reader.GetOrdinal(DLVariables.Precio)));
                        productoDTO.Categoria = reader.GetString(reader.GetOrdinal(DLVariables.Categoria));
                        productoDTO.IdCategoria = reader.GetInt32(reader.GetOrdinal(DLVariables.IdCategoria));
                        productoDTO.IdEstado = reader.GetInt32(DLVariables.IdEstado);
                        productoDTO.ImagenBase64 = reader.IsDBNull(reader.GetOrdinal(DLVariables.Imagen)) ?
                            string.Empty : Convert.ToBase64String((byte[])reader[DLVariables.Imagen]);


                        productosDTO.Add(productoDTO);
                    }
                }

                response.Code = ResponseType.Success;
                response.Message = DLMessages.ListaProductos;
                response.Data = productosDTO;
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;
            }
            return response;
        }


        public async Task<Response> IngresarProducto(ProductoDTO productoDTO)
        {
            Response response = new Response();
            try
            {
                Producto nuevoProducto = productoMapper.ProductoDTOToProducto(productoDTO);
                nuevoProducto.Imagen = Convert.FromBase64String(productoDTO.ImagenBase64!);
                _context.Productos.Add(nuevoProducto);

                await _context.SaveChangesAsync();

                response.Code = ResponseType.Success;
                response.Message = DLMessages.IngresoProducto;
                response.Data = productoDTO;
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;
            }
            return response;
        }

        public async Task<Response> ActualizarProducto(ProductoDTO productoDTO)
        {
            try
            {
                Producto? producto = await _context.Productos.FindAsync(productoDTO.IdProducto);

                if (producto == null)
                {
                    throw new Exception(DLMessages.ProductoNoEncontrado);
                }

                producto.Nombre = productoDTO.Nombre.IsNullOrEmpty() ? producto.Nombre : productoDTO.Nombre!;
                producto.Descripcion = productoDTO.Descripcion.IsNullOrEmpty() ? producto.Descripcion : productoDTO.Descripcion!;
                producto.Precio = productoDTO.Precio.ToString().IsNullOrEmpty() ? producto.Precio : Convert.ToDecimal(productoDTO.Precio);
                producto.Imagen = productoDTO.ImagenBase64.IsNullOrEmpty() ? producto.Imagen : Convert.FromBase64String(productoDTO.ImagenBase64!);
                producto.IdCategoria = productoDTO.IdCategoria.ToString().IsNullOrEmpty() ? producto.IdCategoria : productoDTO.IdCategoria;
                producto.IdEstado = productoDTO.IdEstado.ToString().IsNullOrEmpty() ? producto.IdEstado : productoDTO.IdEstado;

                _context.Productos.Update(producto);
                await _context.SaveChangesAsync();

                response.Code = ResponseType.Success;
                response.Message = DLMessages.ProductoActualizado;
                response.Data = null;
            }
            catch (Exception ex)
            {
                response.Code = ResponseType.Error;
                response.Message = ex.Message;
                response.Data = ex.Data;
            }
            return response;
        }

        public async Task<Response> EstadoProducto(int productoId, int estado)
        {
            Response response = new Response();
            try
            {
                Producto? producto = await _context.Productos.FindAsync(productoId);

                if (producto == null)
                {
                    throw new Exception(DLMessages.ProductoNoEncontrado);
                }

                producto.IdEstado = estado;
                _context.Productos.Update(producto);
                await _context.SaveChangesAsync();

                response.Code = ResponseType.Success;
                response.Message = estado == 1 ? DLMessages.ProductoActivado : DLMessages.ProductoDesactivado;
                response.Data = null;
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
