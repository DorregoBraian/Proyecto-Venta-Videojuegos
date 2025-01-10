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
    public class ClasificacionController : ControllerBase
    {
        private readonly IClasificacionService _clasificacionService;
        private readonly IMapper _mapper;

        public ClasificacionController(IClasificacionService clasificacionService, IMapper mapper)
        {
            _clasificacionService = clasificacionService;
            _mapper = mapper;
        }

        // Devuelve todas las clasificaciones
        // GET: api/clasificacion
        [HttpGet]
        public async Task<IActionResult> GetAllClasificaciones()
        {
            var clasificaciones = await _clasificacionService.GetAllClasificacionesAsync();
            if (clasificaciones != null)
            {
                return Ok(clasificaciones); // Retorna HTTP 200 con la lista de clasificaciones
            }
            return NotFound("No se encontro ningun recurso en la base de datos"); // Retorna HTTP 404 si no hay clasificaciones
        }

        // Devuelve una clasificación por su ID
        // GET: api/clasificacion/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClasificacionById(int id)
        {
            if (id > 0)
            {
                var clasificacionDto = await _clasificacionService.GetClasificacionByIdAsync(id);
                if (clasificacionDto != null)
                {
                    return Ok(clasificacionDto); // Retorna HTTP 200 con la clasificación encontrada
                }
                return NotFound("No se encontro ningun recurso en la base de datos"); // Retorna HTTP 404 si no hay clasificaciones
            }
            return BadRequest("El parámetro es inválido."); // Retorna HTTP 400 si el ID no es válido
        }

        // Crea una nueva clasificación
        // POST: api/clasificacion
        [HttpPost]
        public async Task<IActionResult> CreateClasificacion([FromBody] ClasificacionDTO clasificacionDto)
        {
            if (ModelState.IsValid)
            {
                var existingClasificacion = await _clasificacionService.GetClasificacionByNombreAsync(clasificacionDto.Nombre);
                if (existingClasificacion == null)
                {
                    var nuevaClasificacion = await _clasificacionService.CreateClasificacionAsync(clasificacionDto); 
                    return CreatedAtAction(nameof(GetClasificacionById), new { id = nuevaClasificacion.Id }, nuevaClasificacion);
                }
                return Conflict("Ya existe una clasificación con ese nombre."); // Devuelve conflicto si ya existe
            }
            return BadRequest("Modelo no es válido" + ModelState); // Maneja el caso de un modelo no válido
        }

        // Actualiza una clasificación por su ID
        // PUT: api/clasificacion/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClasificacion(int id, [FromBody] ClasificacionDTO clasificacionDto)
        {
            if (id > 0 && ModelState.IsValid)
            {
                var existingClasificacion = await _clasificacionService.GetClasificacionByIdAsync(id);
                if (existingClasificacion != null)
                {
                    var result = await _clasificacionService.UpdateClasificacionByIdAsync(id, clasificacionDto);
                    return Ok(result); // Retorna HTTP 200 si la actualización fue exitosa
                }
                return NotFound("No se encontro la clasificación"); // Retorna HTTP 404 si no se encuentra la clasificación
            }
            return BadRequest("El parámetro y/o modelo es inválido." + ModelState); // Retorna HTTP 400 si los IDs no coinciden
        }

        // Elimina una clasificación por su ID
        // DELETE: api/clasificacion/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClasificacion(int id)
        {
            if (id > 0)
            {
                var clasificacion = await _clasificacionService.GetClasificacionByIdAsync(id);
                if (clasificacion != null)
                {
                    var result = await _clasificacionService.DeleteClasificacionByIdAsync(id);
                    return Ok(result); // Retorna HTTP 200 si se elimina exitosamente
                }
                return NotFound("No se encontro la clasificación"); // Retorna HTTP 404 si no se encuentra la clasificación
            }
            return BadRequest("El parámetro es inválido."); // Retorna HTTP 400 si el ID no es válido
        }
    }
}

