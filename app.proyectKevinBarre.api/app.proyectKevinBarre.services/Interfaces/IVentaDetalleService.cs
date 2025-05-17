using app.proyectKevinBarre.common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.proyectKevinBarre.services.Interfaces
{
    public interface IVentaDetalleService
    {
        Task<BaseResponse<DetalleVentaDto>> GetEntidad(int id);

        Task<BaseResponse<List<DetalleVentaDto>>> GetEntidadLista();

        Task<BaseResponse<DetalleVentaDto>> CrearEntidad(DetalleVentaDto request);

        Task<BaseResponse<DetalleVentaDto>> ActualizarEntidad(int id, DetalleVentaDto request);

        Task<BaseResponse<string>> EliminarEntidad(int id);

    }
}
