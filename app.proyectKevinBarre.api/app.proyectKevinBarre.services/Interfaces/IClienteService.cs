using app.proyectKevinBarre.common.Dto;
using ECommerce_NetCore.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.proyectKevinBarre.services.Interfaces
{
    public interface IClienteService
    {
       
        Task<BaseResponse<ClienteDto>> GetEntidad(int id);

        Task<BaseResponse<List<ClienteDto>>> GetEntidadLista();

        Task<BaseResponse<ClienteDto>> CrearEntidad(ClienteDto request);

        Task<BaseResponse<ClienteDto>> ActualizarEntidad(int id, ClienteDto request);

        Task<BaseResponse<string>> EliminarEntidad(int id);

    }
}
