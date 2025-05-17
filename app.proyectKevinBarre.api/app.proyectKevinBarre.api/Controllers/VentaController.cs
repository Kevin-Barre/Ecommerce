using app.proyectKevinBarre.common.Dto;
using app.proyectKevinBarre.services.Interfaces;
using ECommerce_NetCore.Dto.Request;
using Microsoft.AspNetCore.Mvc;

namespace app.proyectKevinBarre.api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : Controller
    {

        private readonly IVentaService _ventaService;

        public VentaController(IVentaService ventaService)
        {
            _ventaService = ventaService;
        }


        [HttpGet("obtenerVentas")]
        public async Task<IActionResult> ObtenerClientes()
        {
            var result = await _ventaService.GetEntidadLista();
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost("insertarVenta")]
        public async Task<IActionResult> PostCliens([FromBody] VentaDto request)
        {
            var response = await _ventaService.CrearEntidad(request);

            return Ok(response);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var response = await _ventaService.GetEntidad(id);
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
        public async Task<IActionResult> Actualizar(int id, [FromBody] VentaDto request)
        {
            var result = await _ventaService.ActualizarEntidad(id, request); 
            return Ok(result);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _ventaService.EliminarEntidad(id);
            return Ok(result);
        }

    }
    
}
