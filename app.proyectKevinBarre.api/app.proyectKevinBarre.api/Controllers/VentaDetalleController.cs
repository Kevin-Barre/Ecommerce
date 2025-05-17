using app.proyectKevinBarre.common.Dto;
using app.proyectKevinBarre.services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace app.proyectKevinBarre.api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class VentaDetalleController : Controller
    {

        private readonly IVentaDetalleService _ventaDetalleService;

        public VentaDetalleController(IVentaDetalleService ventaDetalleService)
        {
            _ventaDetalleService = ventaDetalleService;
        }


        [HttpGet("obtenerVentaDeatlle")]
        public async Task<IActionResult> ObtenerDetalleVenta()
        {
            var result = await _ventaDetalleService.GetEntidadLista();
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost("insertarVentaDetalle")]
        public async Task<IActionResult> PostCliens([FromBody] DetalleVentaDto request)
        {
            var response = await _ventaDetalleService.CrearEntidad(request);

            return Ok(response);
        }
        

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var response = await _ventaDetalleService.GetEntidad(id);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] DetalleVentaDto request)
        {
            var result = await _ventaDetalleService.ActualizarEntidad(id, request);
            return Ok(result);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _ventaDetalleService.EliminarEntidad(id);
            return Ok(result);
        }

    }
}
