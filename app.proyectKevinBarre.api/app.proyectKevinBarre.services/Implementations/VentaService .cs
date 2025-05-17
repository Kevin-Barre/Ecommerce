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
    public class VentaService(IVentaRepository repository, IRabbitMQService rabbitMQService) : IVentaService
    {
        private readonly IVentaRepository _repository = repository;
        private readonly IRabbitMQService _rabbitMQService = rabbitMQService;


        public async Task<BaseResponse<VentaDto>> ActualizarEntidad(int id, VentaDto request)
        {
            var response = new BaseResponse<VentaDto>();
            try
            {
                Venta venta = new();
                venta.Id = id;
                venta.ClienteId = request.ClienteId;
                venta.FechaVenta = request.FechaVenta;
                venta.NumeroFactura = request.NumeroFactura;
                venta.MetodoPago = request.MetodoPago;
                venta.TotalVenta = request.TotalVenta;

                await _repository.UpdateEntidad(venta); 

              
                 response.Result  = new VentaDto
                 {
                    Id = venta.Id,
                    ClienteId = venta.ClienteId,
                    FechaVenta = venta.FechaVenta,
                    NumeroFactura = venta.NumeroFactura,
                    MetodoPago = venta.MetodoPago,
                    TotalVenta = venta.TotalVenta
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

        public async Task<BaseResponse<VentaDto>> CrearEntidad(VentaDto request)
        {
            var response = new BaseResponse<VentaDto>();
            try
            {
                Venta venta = new();
                venta.ClienteId = request.ClienteId;
                venta.FechaVenta = request.FechaVenta;
                venta.NumeroFactura = request.NumeroFactura;
                venta.MetodoPago = request.MetodoPago;
                venta.TotalVenta = request.TotalVenta;



                venta = await _repository.CreateEntidad(venta);

                response.Result = new VentaDto
                {
                    Id = venta.Id,
                    ClienteId = venta.ClienteId,
                    FechaVenta = venta.FechaVenta,
                    NumeroFactura = venta.NumeroFactura,
                    MetodoPago = venta.MetodoPago,
                    TotalVenta = venta.TotalVenta

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

        public async Task<BaseResponse<string>> EliminarEntidad(int id)
        {
            var  response = new BaseResponse<string>();
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

        public async Task<BaseResponse<VentaDto>> GetEntidad(int id)
        {
            var response = new BaseResponse<VentaDto>();
            try
            {
                var venta = await _repository.GetEntidad(id);
                if (venta == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Registro no encontrado";
                    return response;
                }

                response.Result = new VentaDto
                {
                    Id = venta.Id,
                    ClienteId = venta.ClienteId,
                    FechaVenta = venta.FechaVenta,
                    NumeroFactura = venta.NumeroFactura,
                    MetodoPago = venta.MetodoPago,
                    TotalVenta = venta.TotalVenta
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


        public async Task<BaseResponse<List<VentaDto>>> GetEntidadLista()
        {
            var response = new BaseResponse<List<VentaDto>>();
            try
            {
                var result = await _repository.ObtenerEntidadesLista();

                response.Result = result.Select(p => new VentaDto
                {
                    Id = p.Id,
                    ClienteId = p.ClienteId,
                    FechaVenta = p.FechaVenta,
                    NumeroFactura = p.NumeroFactura,
                    MetodoPago = p.MetodoPago, 
                    TotalVenta= p.TotalVenta         

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
