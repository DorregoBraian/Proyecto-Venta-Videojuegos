using Aplicacion.DTOs;
using Aplicacion.IServicios;
using AutoMapper;
using Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Rest_de_Videos_Juegos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlataformaController : ControllerBase
    {
        private readonly IPlataformaService _plataformaService;
        private readonly IMapper _mapper;

        public PlataformaController(IPlataformaService plataformaService, IMapper mapper)
        {
            _plataformaService = plataformaService;
            _mapper = mapper;
        }

        // Devuelve todas las plataformas
        // GET: api/plataforma
        [HttpGet]
        public async Task<IActionResult> GetAllPlataformas()
        {
            var plataformas = await _plataformaService.GetAllPlataformasAsync();
            if (plataformas != null)
            {
                return Ok(plataformas); // Devuelve el resultado en un HTTP 200 OK con los datos
            }
            return NotFound("No se encontro ningun recurso en la base de datos"); // Retorna HTTP 404 si no hay clasificaciones
        }

        // Devuelve una plataforma por su ID
        // GET: api/plataforma/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlataformaById(int id)
        {
            if (id > 0)
            {
                var plataforma = await _plataformaService.GetPlataformaByIdAsync(id);
                if (plataforma != null)
                {
                    return Ok(plataforma); // Retorna HTTP 200 con la plataforma encontrada
                }
                return NotFound("No se encontro ningun recurso en la base de datos"); // Retorna HTTP 404 si no hay clasificaciones
            }
            return BadRequest("El parámetro es inválido."); // Retorna HTTP 400 si el ID no es válido
        }

        // Crea una nueva plataforma
        // POST: api/plataforma
        [HttpPost]
        public async Task<IActionResult> CreatePlataforma([FromBody] PlataformaDTO plataformaDto)
        {
            if (ModelState.IsValid)
            {
                var existingPlataforma = await _plataformaService.GetPlataformaByNombreAsync(plataformaDto.Nombre);
                if (existingPlataforma == null)
                {
                    await _plataformaService.CreatePlataformaAsync(plataformaDto);
                    return CreatedAtAction(nameof(GetPlataformaById), new { id = plataformaDto.Id }, plataformaDto); // HTTP 201
                }
                return BadRequest("La plataforma ya existe"); // Retorna HTTP 400 si la plataforma ya existe
            }
            return BadRequest("Modelo no es válido" + ModelState); // Retorna HTTP 400 si el modelo no es válido
        }


        // Actualiza una plataforma existente
        // PUT: api/plataforma/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlataforma(int id, [FromBody] PlataformaDTO plataformaDto)
        {
            if (id > 0 && ModelState.IsValid)
            {
                var existingPlataforma = await _plataformaService.GetPlataformaByIdAsync(id);
                if (existingPlataforma != null)
                {
                    var result = await _plataformaService.UpdatePlataformaByIdAsync(id, plataformaDto);
                    return Ok(result); // HTTP 200 después de una actualización exitosa
                }
                return NotFound("No se encontro la plataforma"); // Retorna HTTP 404 si no se encuentra la clasificación
            }
            return BadRequest("El parámetro y/o modelo es inválido." + ModelState); // Retorna HTTP 400 si los IDs no coinciden
        }

        // Elimina una plataforma por su ID
        // DELETE: api/plataforma/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlataforma(int id)
        {
            if (id > 0)
            {
                var plataforma = await _plataformaService.GetPlataformaByIdAsync(id);
                if (plataforma != null)
                {
                    var result = await _plataformaService.DeletePlataformaByIdAsync(id);
                    return Ok(result); // HTTP 204 No Content después de la eliminación exitosa
                }
                return NotFound("No se encontro la plataforma"); // Retorna HTTP 404 si no se encuentra la plataforma
            }
            return BadRequest("El parámetro es inválido."); // Retorna HTTP 400 si el ID no es válido
        }
    }
}
