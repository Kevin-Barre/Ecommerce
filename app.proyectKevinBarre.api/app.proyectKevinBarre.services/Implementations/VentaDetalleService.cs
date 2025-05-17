
using app.projectKevinBarre.services.eventMQ;
using app.proyectKevinBarre.accessData.repositories;
using app.proyectKevinBarre.common.Dto;
using app.proyectKevinBarre.entities.Models;
using app.proyectKevinBarre.services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.proyectKevinBarre.services.Implementations
{
    public class VentaDetalleService(IVentaDetalleRepository repository, IRabbitMQService rabbitMQService) : IVentaDetalleService 
    {
        private readonly IVentaDetalleRepository _repository = repository;
        private readonly IRabbitMQService _rabbitMQService = rabbitMQService;

        public async Task<BaseResponse<DetalleVentaDto>> ActualizarEntidad (int id, DetalleVentaDto request)
        {
            var response = new BaseResponse<DetalleVentaDto>();
            try
            {
                VentaDetalle vD = new();
                vD.Id = id;
                vD.VentaId = request.VentaId;
                vD.NumeroItem = request.NumeroItem;
                vD.ProductoId = request.ProductoId;
                vD.PrecioUnitario = request.PrecioUnitario;
                vD.Cantidad = request.Cantidad;
                vD.Total = request.Total;
                

                await _repository.UpdateEntidad(vD);


                response.Result = new DetalleVentaDto
                {
                    Id = vD.Id, 
                    VentaId = vD.VentaId,
                    NumeroItem = vD.NumeroItem,
                    ProductoId = vD.ProductoId,
                    PrecioUnitario = vD.PrecioUnitario,
                    Cantidad = vD.Cantidad,
                    Total = vD.Total
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

        public async Task<BaseResponse<DetalleVentaDto>> CrearEntidad(DetalleVentaDto request)
        {
            var response = new BaseResponse<DetalleVentaDto>();
            try
            {

                VentaDetalle vD = new();
                vD.VentaId = request.VentaId;
                vD.NumeroItem = request.NumeroItem;
                vD.ProductoId = request.ProductoId;
                vD.PrecioUnitario = request.PrecioUnitario;
                vD.Cantidad = request.Cantidad;
                vD.Total = request.Total;

                vD = await _repository.CreateEntidad(vD);

                response.Result = new DetalleVentaDto
                {
                    Id = vD.Id,
                    VentaId = vD.VentaId,
                    NumeroItem = vD.NumeroItem,
                    ProductoId = vD.ProductoId,
                    PrecioUnitario = vD.PrecioUnitario,
                    Cantidad = vD.Cantidad,
                    Total = vD.Total
                };

                response.Success = true;
                await _rabbitMQService.PublishMessage(response.Result, "ventaDetallesQueue");
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

        public async Task<BaseResponse<DetalleVentaDto>> GetEntidad(int id)
        {
            var response = new BaseResponse<DetalleVentaDto>();
            try
            {
                var vD = await _repository.GetEntidad(id);
                if (vD == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Registro no encontrado";
                    return response;
                }

                response.Result = new DetalleVentaDto
                {
                    Id = vD.Id,
                    VentaId = vD.VentaId,
                    NumeroItem = vD.NumeroItem,
                    ProductoId = vD.ProductoId,
                    PrecioUnitario = vD.PrecioUnitario,
                    Cantidad = vD.Cantidad,
                    Total = vD.Total
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


        public async Task<BaseResponse<List<DetalleVentaDto>>> GetEntidadLista()
        {
            var response = new BaseResponse<List<DetalleVentaDto>>();
            try
            {
                var result = await _repository.ObtenerEntidadesLista();

                response.Result = result.Select(p => new DetalleVentaDto
                {
                    Id = p.Id,
                    VentaId = p.VentaId,
                    NumeroItem = p.NumeroItem,
                    ProductoId = p.ProductoId,
                    PrecioUnitario = p.PrecioUnitario,
                    Cantidad = p.Cantidad,
                    Total = p.Total

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

