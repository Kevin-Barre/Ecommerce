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
    public class ClienteService(IClienteRepository repository, IRabbitMQService rabbitMQService) : IClienteService
    {
        private readonly IClienteRepository _repository = repository;
        private readonly IRabbitMQService _rabbitMQService = rabbitMQService;


        public async Task<BaseResponse<ClienteDto>> ActualizarEntidad(int id, ClienteDto request)
        {
            var response = new BaseResponse<ClienteDto>();
            try
            {
                Cliente cliente = new(); 
                cliente.Id = id;
                cliente.Nombre = request.Nombre;
                cliente.Apellido = request.Apellido;
                cliente.Email = request.Email;
                cliente.FechaNacimiento = request.FechaNacimiento;
                cliente.Fecha = DateTime.Now;
                cliente.CedulaIdentidad = request.CedulaIdentidad;

                await _repository.UpdateEntidad(cliente); 

              
                 response.Result  = new ClienteDto
                {
                    Id = cliente.Id,
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    Email = cliente.Email,
                    CedulaIdentidad = cliente.CedulaIdentidad,
                    FechaNacimiento = cliente.FechaNacimiento
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

        public async Task<BaseResponse<ClienteDto>> CrearEntidad(ClienteDto request)
        {
            var response = new BaseResponse<ClienteDto>();
            try
            {
                Cliente cliente = new();
                cliente.Nombre = request.Nombre;
                cliente.Apellido = request.Apellido;
                cliente.Email = request.Email;
                cliente.FechaNacimiento = request.FechaNacimiento;
                cliente.Fecha = DateTime.Now;
                cliente.CedulaIdentidad = request.CedulaIdentidad;


                cliente = await _repository.CreateEntidad(cliente);

                response.Result = new ClienteDto
                {
                    Id = cliente.Id,
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    Email = cliente.Email,
                    FechaNacimiento = cliente.FechaNacimiento,
                    CedulaIdentidad = cliente.CedulaIdentidad,
                    
                };

                response.Success = true;
                await _rabbitMQService.PublishMessage(response.Result, "clienteQueue");
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

        public async Task<BaseResponse<ClienteDto>> GetEntidad(int id)
        {
            var response = new BaseResponse<ClienteDto>();
            try
            {
                var cliente = await _repository.GetEntidad(id);
                if (cliente == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Registro no encontrado";
                    return response;
                }

                response.Result = new ClienteDto
                {
                    Id = cliente.Id,
                    Nombre = cliente.Nombre,
                    Apellido = cliente.Apellido,
                    Email = cliente.Email,
                    FechaNacimiento = cliente.FechaNacimiento,
                    CedulaIdentidad = cliente.CedulaIdentidad,
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


        public async Task<BaseResponse<List<ClienteDto>>> GetEntidadLista()
        {
            var response = new BaseResponse<List<ClienteDto>>();
            try
            {
                var result = await _repository.ObtenerEntidadesLista();

                response.Result = result.Select(p => new ClienteDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Apellido = p.Apellido,
                    Email = p.Email,
                    FechaNacimiento = p.FechaNacimiento,
                    CedulaIdentidad = p.CedulaIdentidad,

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
