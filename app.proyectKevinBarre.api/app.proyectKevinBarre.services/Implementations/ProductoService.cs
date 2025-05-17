using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.projectKevinBarre.services.eventMQ;
using app.proyectKevinBarre.accessData.repositories;
using app.proyectKevinBarre.common.Dto;
using app.proyectKevinBarre.entities.Models;
using app.proyectKevinBarre.services.Interfaces;
using ECommerce_NetCore.Dto.Request;

namespace app.proyectKevinBarre.services.Implementations
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;
        private readonly IRabbitMQService _rabbitMQService;

        public ProductoService(IProductoRepository repository, IRabbitMQService rabbitMQService)
        {
            _repository = repository;
            _rabbitMQService = rabbitMQService;

        }

        public async Task<BaseResponse<ProductoDto>> ActualizarEntidad(int id, ProductoDto request)
        {
            var response = new BaseResponse<ProductoDto>();
            try
            {
                Producto producto = new();
                producto.Id = id;
                producto.Nombre = request.Nombre;
                producto.Descripcion = request.Descripcion;
                producto.CategoriaId = request.CategoriaId;
                producto.PrecioUnitario = request.PrecioUnitario;

                await _repository.UpdateEntidad(producto);


                response.Result = new ProductoDto
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    CategoriaId = producto.CategoriaId,
                    PrecioUnitario = producto.PrecioUnitario,
                };
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<ProductoDto>> CrearEntidad(ProductoDto request)
        {
            var response = new BaseResponse<ProductoDto>();
            try
            {
                Producto producto = new();
                producto.Nombre = request.Nombre;
                producto.Descripcion = request.Descripcion;
                producto.CategoriaId = request.CategoriaId;
                producto.PrecioUnitario = request.PrecioUnitario;


                producto = await _repository.CreateEntidad(producto);

                response.Result = new ProductoDto
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    CategoriaId = producto.CategoriaId,
                    PrecioUnitario = producto.PrecioUnitario,

                };

                response.Success = true;
                await _rabbitMQService.PublishMessage(response.Result, "productoQueue");
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponse<string>> EliminarEntidad(int id)
        {
            var response = new BaseResponse<string>();
            try
            {
                await _repository.DeleteEntidad(id);

                response.Result = "OK";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse<ProductoDto>> GetEntidad(int id)
        {
            var response = new BaseResponse<ProductoDto>();
            try
            {
                var producto = await _repository.GetEntidad(id);
                if (producto == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Registro no encontrado";
                    return response;
                }

                response.Result = new ProductoDto
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                   Descripcion = producto.Descripcion,
                   CategoriaId = producto.CategoriaId,
                   PrecioUnitario = producto.PrecioUnitario
                };

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }

            return response;
        }


        public async Task<BaseResponse<List<ProductoDto>>> GetEntidadLista()
        {
            var response = new BaseResponse<List<ProductoDto>>();
            try
            {
                var result = await _repository.ObtenerEntidadesLista();

                response.Result = result.Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    CategoriaId = p.CategoriaId,
                    PrecioUnitario = p.PrecioUnitario

                }).ToList();
                if (response.Result.Count > 0)
                {
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.ErrorMessage = "Datos vacíos";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }
    }
}
