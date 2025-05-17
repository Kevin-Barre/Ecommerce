using app.proyectKevinBarre.common.Dto;
using app.proyectKevinBarre.services.Implementations;
using app.proyectKevinBarre.services.Interfaces;
using Azure;
using ECommerce_NetCore.Dto.Request;
using Microsoft.AspNetCore.Mvc;

namespace app.proyectKevinBarre.api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {

        private readonly IProductoService _productoService;

        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }


        [HttpGet("obtenerProducto")]
        public async Task<IActionResult> ObtenerProducto()
        {
            var result = await _productoService.GetEntidadLista();
            if(result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost("insertarProducto")]
        public async Task<IActionResult> PostProductos([FromBody] ProductoDto request)
        {
            var response = await _productoService.CrearEntidad(request);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var response = await _productoService.GetEntidad(id);
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
        public async Task<IActionResult> Actualizar(int id, [FromBody] ProductoDto request)
        {
            var result = await _productoService.ActualizarEntidad(id, request);
            return Ok(result);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _productoService.EliminarEntidad(id);
            return Ok(result);
        }

    }
}
