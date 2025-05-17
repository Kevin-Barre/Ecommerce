
using app.proyectKevinBarre.common.Dto;
using ECommerce_NetCore.Dto.Request;

namespace app.proyectKevinBarre.services.Interfaces
{
    public interface ICategoriaService
    {
        Task<BaseResponse<CategoriaDto>> GetEntidad(int id);

        Task<BaseResponse<List<CategoriaDto>>> GetEntidadLista();

        Task<BaseResponse<CategoriaDto>> CreateEntidad(CategoriaDto request);

        Task<BaseResponse<CategoriaDto>> ActualizarEntidad(int id, CategoriaDto request);

        Task<BaseResponse<string>> EliminarEntidad(int id);
    }
}
