using app.proyectKevinBarre.common.Dto;
using app.proyectKevinBarre.services.Interfaces;
using ECommerce_NetCore.Dto.Request;
using Microsoft.AspNetCore.Mvc;

namespace app.proyectKevinBarre.api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {

        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }


        [HttpGet("obtenerClientes")]
        public async Task<IActionResult> ObtenerClientes()
        {
            var result = await _clienteService.GetEntidadLista();
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost("insertarCliente")]
        public async Task<IActionResult> PostCliens([FromBody] ClienteDto request)
        {
            var response = await _clienteService.CrearEntidad(request);

            return Ok(response);
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var response = await _clienteService.GetEntidad(id);
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
        public async Task<IActionResult> Actualizar(int id, [FromBody] ClienteDto request)
        {
            var result = await _clienteService.ActualizarEntidad(id, request); 
            return Ok(result);
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _clienteService.EliminarEntidad(id);
            return Ok(result);
        }

    }
    
}
