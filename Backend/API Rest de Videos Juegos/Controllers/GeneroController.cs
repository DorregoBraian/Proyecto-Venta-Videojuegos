using Aplicacion.DTOs;
using Aplicacion.IServicios;
using Aplicacion.Servicio;
using AutoMapper;
using Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Rest_de_Videos_Juegos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroService _generoService;
        private readonly IMapper _mapper;

        public GeneroController(IGeneroService generoService, IMapper mapper)
        {
            _generoService = generoService;
            _mapper = mapper;
        }

        // Devuelve todos los géneros
        // GET: api/genero
        [HttpGet]
        public async Task<IActionResult> GetAllGeneros()
        {
            var generos = await _generoService.GetAllGenerosAsync(); // Obtiene todos los géneros
            if (generos != null)
            {
                return Ok(generos); // Retorna HTTP 200 OK con la lista de géneros
            }
            return NotFound("No se encontro ningun recurso en la base de datos"); // Retorna HTTP 404 si no hay géneros
        }

        // Devuelve un género por su ID
        // GET: api/genero/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGeneroById(int id)
        {
            if (id > 0)
            {
                var generoDto = await _generoService.GetGeneroByIdAsync(id);
                if (generoDto != null)
                {
                    return Ok(generoDto); // Retorna HTTP 200 OK con el género encontrado
                }
                return NotFound("No se encontro ningun recurso en la base de datos"); // Retorna HTTP 404 si no hay géneros
            }
            return BadRequest("El parámetro es inválido."); // Retorna HTTP 400 si el ID no es válido
        }

        // Crea un nuevo género
        // POST: api/genero
        [HttpPost]
        public async Task<IActionResult> CreateGenero([FromBody] GeneroDTO generoDto)
        {
            if (ModelState.IsValid)
            {
                var existingGenero = await _generoService.GetGeneroByNombreAsync(generoDto.Nombre);
                if (existingGenero == null)
                {
                    var nuevaGenero = await _generoService.CreateGeneroAsync(generoDto);
                    // Retorna HTTP 201 Created con la ruta del nuevo recurso
                    return CreatedAtAction(nameof(GetGeneroById), new { id = nuevaGenero.Id }, nuevaGenero);
                }
                return Conflict("El recurso ya existe en la base de datos"); // Retorna HTTP 409 Conflict si el género ya existe
            }
            return BadRequest("Modelo no es válido" + ModelState); // Retorna HTTP 400 Bad Request si el modelo no es válido
        }

        // Actualiza un género por su ID
        // PUT: api/genero/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenero(int id, [FromBody] GeneroDTO generoDto)
        {
            if (id > 0 && ModelState.IsValid)
            {
                var existingGenero = await _generoService.GetGeneroByIdAsync(id);
                if (existingGenero != null)
                {
                    var result = await _generoService.UpdateGeneroByIdAsync(id, generoDto); // Llama al servicio para actualizar el género
                    return Ok(result); // Retorna HTTP 200 si la actualización fue exitosa
                }
                return NotFound("No se encontro la clasificación"); // Retorna HTTP 404 si no se encuentra la clasificación
            }
            return BadRequest("El parámetro y/o modelo es inválido." + ModelState); // Retorna HTTP 400 si los IDs no coinciden
        }

        // Elimina un género por su ID
        // DELETE: api/genero/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenero(int id)
        {
            if (id > 0)
            {
                var existingGenero = await _generoService.GetGeneroByIdAsync(id);
                if (existingGenero != null)
                {
                    var result = await _generoService.DeleteGeneroByIdAsync(id);
                    return Ok(result); // Retorna HTTP 200 si la eliminación fue exitosa
                }
                return NotFound("No se encontro la clasificación"); // Retorna HTTP 404 si no se encuentra la clasificación
            }
            return BadRequest("El parámetro es inválido."); // Retorna HTTP 400 si el ID no es válido
        }
    }
}
