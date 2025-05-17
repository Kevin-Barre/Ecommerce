using app.proyectKevinBarre.common.Dto;
using ECommerce_NetCore.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.proyectKevinBarre.services.Interfaces
{
    public interface IProductoService
    {
        Task<BaseResponse<ProductoDto>> GetEntidad(int id);

        Task<BaseResponse<List<ProductoDto>>> GetEntidadLista();

        Task<BaseResponse<ProductoDto>> CrearEntidad(ProductoDto request);

        Task<BaseResponse<ProductoDto>> ActualizarEntidad(int id, ProductoDto request);

        Task<BaseResponse<string>> EliminarEntidad(int id);
    }
}