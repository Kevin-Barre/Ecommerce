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
    public class CategoriaController : Controller
    {

        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }


        [HttpGet("obtenerCategoria")]
        public async Task<IActionResult> ObtenerCategoria()
        {
            var result = await _categoriaService.GetEntidadLista();
            if(result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost("insertarCategoria")]
        public async Task<IActionResult> PostCategories([FromBody] CategoriaDto request)
        {
            var response = await _categoriaService.CreateEntidad(request);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var response = await _categoriaService.GetEntidad(id);
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
        public async Task<IActionResult> Actualizar(int id, [FromBody] CategoriaDto request)
        {
            var result = await _categoriaService.ActualizarEntidad(id, request);
            return Ok(result);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _categoriaService.EliminarEntidad(id);
            return Ok(result);
        }
    }
}
