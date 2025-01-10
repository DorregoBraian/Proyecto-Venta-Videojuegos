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
    public class IdiomaController : ControllerBase
    {
        private readonly IIdiomaService _idiomaService;
        private readonly IMapper _mapper;

        public IdiomaController(IIdiomaService idiomaService, IMapper mapper)
        {
            _idiomaService = idiomaService;
            _mapper = mapper;
        }

        // Devuelve todos los idiomas
        // GET: api/idioma
        [HttpGet]
        public async Task<IActionResult> GetIdiomas()
        {
            var idiomas = await _idiomaService.GetAllIdiomasAsync(); // Lógica de obtención desde el servicio
            if (idiomas != null)
            {
                return Ok(idiomas); // Retorna HTTP 200 OK con la lista de DTOs
            }
            return NotFound("No se encontro ningun recurso en la base de datos"); // Retorna HTTP 404 si no hay clasificaciones
        }

        // Devuelve un idioma por su ID
        // GET: api/idioma/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIdiomaById(int id)
        {
            if (id > 0)
            {
                var idioma = await _idiomaService.GetIdiomaByIdAsync(id);
                if (idioma != null)
                {
                    return Ok(idioma); // Retorna HTTP 200 OK con el DTO
                }
                return NotFound("No se encontro ningun recurso en la base de datos"); // Retorna HTTP 404 si el recurso no existe
            }
            return BadRequest("El parámetro es inválido."); // Retorna HTTP 400 si el ID no es válido
        }

        // Crea un nuevo idioma
        // POST: api/idioma
        [HttpPost]
        public async Task<IActionResult> CreateIdioma([FromBody] IdiomaDTO idiomaDto)
        {
            if (ModelState.IsValid)
            {
                var existingIdioma = await _idiomaService.GetIdiomaByNombreAsync(idiomaDto.Nombre);
                if (existingIdioma == null)
                {
                    var nuevoIdioma = await _idiomaService.CreateIdiomaAsync(idiomaDto);
                    return CreatedAtAction(nameof(GetIdiomaById), new { id = nuevoIdioma.Id }, nuevoIdioma); // Retorna HTTP 201 Created
                }
                return Conflict("Ya existe un idoma con ese nombre."); // Retorna HTTP 409 si ya existe
            }
            return BadRequest("Modelo no es válido" + ModelState); // Retorna HTTP 400 si el modelo no es válido
        }

        // Actualiza un idioma por su ID
        // PUT: api/idioma/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIdioma(int id, [FromBody] IdiomaDTO idiomaDto)
        {
            if (id > 0 && ModelState.IsValid)
            {
                var existingIdioma = await _idiomaService.GetIdiomaByIdAsync(id);
                if (existingIdioma != null)
                {
                    var result = await _idiomaService.UpdateIdiomaByIdAsync(id, idiomaDto);
                    return Ok(result); // Retorna HTTP 204 No Content
                }
                return NotFound("No se encontro ningun recurso en la base de datos"); // Retorna HTTP 404 si el recurso no existe
            }
            return BadRequest("El parámetro y/o modelo es inválido." + ModelState); // Retorna HTTP 400 si los IDs no coinciden
        }

        // Elimina un idioma por su ID
        // DELETE: api/idioma/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIdioma(int id)
        {
            if (id > 0)
            {
                var idioma = await _idiomaService.GetIdiomaByIdAsync(id);
                if (idioma != null)
                {
                    var result = await _idiomaService.DeleteIdiomaByIdAsync(id);
                    return Ok(result); // Retorna HTTP 204 No Content
                }
                return NotFound("No se encontro ningun idioma"); // Retorna HTTP 404 si el recurso no existe
            }
            return BadRequest("El parámetro es inválido."); // Retorna HTTP 400 si el ID no es válido
        }
    }
}
