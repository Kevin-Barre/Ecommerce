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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace app.proyectKevinBarre.services.Implementations
{
    public class CategoriaService(ICategoriaRepository repository, IRabbitMQService rabbitMQService) : ICategoriaService
    {
        private readonly ICategoriaRepository _repository = repository;
        private readonly IRabbitMQService _rabbitMQService = rabbitMQService;


        public async Task<BaseResponse<CategoriaDto>> ActualizarEntidad(int id, CategoriaDto request)
        {
            var response = new BaseResponse<CategoriaDto>();
            try
            {

                Categoria categoria = new();
                categoria.Id = id;
                categoria.Nombre = request.Nombre;
                categoria.Descripcion = request.Descripcion;
                

                await _repository.UpdateEntidad(categoria);


                response.Result = new CategoriaDto
                {
                    Id = categoria.Id,
                    Nombre = categoria.Nombre,
                    Descripcion = request.Descripcion
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

        public async Task<BaseResponse<CategoriaDto>> CreateEntidad(CategoriaDto request)
        {
            var response = new BaseResponse<CategoriaDto>();
            try
            {
                Categoria categoryEntity = new();
                categoryEntity.Nombre = request.Nombre;
                categoryEntity.Descripcion = request.Descripcion;

                var categoria = await _repository.CreateEntidad(categoryEntity);

                response.Result = new CategoriaDto
                {
                    Id = categoria.Id,
                    Nombre = categoria.Nombre,
                    Descripcion = categoria.Descripcion
                };

                response.Success = true;
                await _rabbitMQService.PublishMessage(response.Result, "categoriasQueue");
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

        public async Task<BaseResponse<CategoriaDto>> GetEntidad(int id)
        {
            var response = new BaseResponse<CategoriaDto>();
            try
            {
                var categoria = await _repository.GetEntidad(id);
                if (categoria == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "Registro no encontrado";
                    return response;
                }

                response.Result = new CategoriaDto
                {
                    Id = categoria.Id,
                    Nombre = categoria.Nombre,
                    Descripcion = categoria.Descripcion
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


        public async Task<BaseResponse<List<CategoriaDto>>> GetEntidadLista()
        {
            var response = new BaseResponse<List<CategoriaDto>>();
            try
            {
                var result = await _repository.ObtenerEntidadesLista();

                response.Result = result.Select(p => new CategoriaDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion

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
