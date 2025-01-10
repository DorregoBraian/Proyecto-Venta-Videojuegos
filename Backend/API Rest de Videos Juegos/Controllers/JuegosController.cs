using Aplicacion.DTOs;
using AutoMapper;
using Dominio.IServicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Rest_de_Videos_Juegos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JuegosController : ControllerBase
    {
        private readonly IJuegoService _juegoService;
        private readonly IMapper _mapper;

        public JuegosController(IJuegoService juegoService, IMapper mapper)
        {
            _juegoService = juegoService; 
            _mapper = mapper; 
        }

        // Devuelve todos los Juegos
        // GET: api/juegos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JuegoDTO>>> GetAllJuegos()
        {
            var juegos = await _juegoService.GetAllGamesAsync();
            if (juegos != null)
            {
                return Ok(juegos); // Devuelve el resultado en un HTTP 200 OK con los datos mapeados
            }
            return NotFound("No se encontraron juegos"); // HTTP 404 si no se encuentran juegos
        }

        // Devuelve un Juego por su ID
        // GET: api/juegos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<JuegoDTO>> GetJuegoById(int id)
        {
            if (id > 0)
            {
                var juego = await _juegoService.GetGameByIdAsync(id);
                if (juego != null)
                {
                    return Ok(juego); // Devuelve el resultado en un HTTP 200 OK con los datos mapeados
                }
                return NotFound("Juego no encontrado"); // HTTP 404 si no se encuentra el juego
            }
            return BadRequest("ID de juego inválido"); // HTTP 400 si el ID es inválido
        }

        // Devuelve un Juego por su Nombre
        // GET: api/juegos/{nombre}
        [HttpGet("nombre/{nombre}")]
        public async Task<ActionResult<IEnumerable<JuegoDTO>>> GetJuegoByName(string nombre)
        {
            if (!string.IsNullOrEmpty(nombre))
            {
                var juego = await _juegoService.GetGameByNameAsync(nombre);
                if (juego != null)
                {
                    return Ok(juego); // HTTP 200 OK con el objeto mapeado

                }
                return NotFound("Juego no encontrado"); // HTTP 404 si no se encuentra el juego
            }
            return BadRequest("Nombre de juego inválido"); // HTTP 400 si el nombre es inválido
        }

        // Crea un Juego
        // POST: api/juegos
        [HttpPost]
        public async Task<ActionResult<JuegoDTO>> CreateJuego(CreateJuegoDTO juegoDto)
        {
            if (ModelState.IsValid)
            {
                var juego = await _juegoService.CreateGameAsync(juegoDto); // Llama al servicio para crear el juego
                if (juego != null)
                {
                    return CreatedAtAction(nameof(GetJuegoById), new { id = juego.Id }, juego); // HTTP 201 Created
                }
                return BadRequest("Error al crear el juego"); // HTTP 400 si hay un error al crear el juego
            }
            return BadRequest("Modelo no es válido" + ModelState); // Retorna HTTP 400 si el modelo no es válido
        }

        // Actualiza un Juego por su ID
        // PUT: api/juegos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJuego(int id, [FromBody] CreateJuegoDTO createJuegoDto)
        {
            if (id > 0 || ModelState.IsValid)
            {
                var existingJuego = await _juegoService.GetGameByIdAsync(id); // Verifica si el juego existe
                if (existingJuego != null)
                {
                    var juego = await _juegoService.UpdateGameByIdAsync(id, createJuegoDto);
                    return Ok(juego);
                }
                return NotFound("Juego no encontrado"); // HTTP 404 si no se encuentra el juego
            }
            return BadRequest("El parámetro y/o modelo es inválido." + ModelState); // Retorna HTTP 400 si los IDs no coinciden
        }

        // Elimina un Juego por su ID
        // DELETE: api/juegos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJuego(int id)
        {
            if (id > 0)
            {
                var existingJuego = await _juegoService.GetGameByIdAsync(id); // Verifica si el juego existe
                if (existingJuego != null)
                {
                    var result = await _juegoService.DeleteGameByIdAsync(id); // Llama al servicio para eliminar el juego
                    return Ok(result); // HTTP 204 No Content después de la eliminación exitosa
                }
                return NotFound("Juego no encontrado"); // HTTP 404 si no se encuentra el juego
            }
            return BadRequest("ID de juego inválido"); // HTTP 400 si el ID es inválido
        }

        // Endpoint para filtrar juegos por género
        [HttpGet("genero/{genero}")]
        public async Task<IActionResult> GetJuegosByGenero(string genero)
        {
            
            var juegos = await _juegoService.GetJuegosByGeneroAsync(genero);
            if (!juegos.Any())
            {
                return NotFound($"No se encontraron juegos con el género '{genero}'.");
            }
            return Ok(juegos);
        }

        // Endpoint para filtrar juegos por plataforma
        [HttpGet("plataforma/{plataforma}")]
        public async Task<IActionResult> GetJuegosByPlataforma(string plataforma)
        {
            var juegos = await _juegoService.GetJuegosByPlataformaAsync(plataforma);
            if (!juegos.Any())
            {
                return NotFound($"No se encontraron juegos para la plataforma '{plataforma}'.");
            }
            return Ok(juegos);
        }

        // Endpoint para filtrar juegos por idioma
        [HttpGet("idioma/{idioma}")]
        public async Task<IActionResult> GetJuegosByIdioma(string idioma)
        {
            var juegos = await _juegoService.GetJuegosByIdiomaAsync(idioma);
            if (!juegos.Any())
            {
                return NotFound($"No se encontraron juegos en el idioma '{idioma}'.");
            }
            return Ok(juegos);
        }

        // Endpoint para filtrar juegos por clasificación
        [HttpGet("clasificacion/{clasificacion}")]
        public async Task<IActionResult> GetJuegosByClasificacion(string clasificacion)
        {
            var juegos = await _juegoService.GetJuegosByClasificacionAsync(clasificacion);
            if (!juegos.Any())
            {
                return NotFound($"No se encontraron juegos con la clasificación '{clasificacion}'.");
            }
            return Ok(juegos);
        }

        // Endpoint para filtrar juegos con multiples filtros y paginados
        [HttpGet("filtrar")]
        public async Task<IActionResult> FiltrarJuegos(
                [FromQuery] string? idioma = null,
                [FromQuery] string? plataforma = null,
                [FromQuery] string? genero = null,
                [FromQuery] string? clasificacion = null,
                [FromQuery] int page = 1,
                [FromQuery] int pageSize = 10)
        {
            try
            {
                // Validar parámetros de paginación
                if (page <= 0 || pageSize <= 0)
                {
                    return BadRequest("El número de página y el tamaño de página deben ser mayores a 0.");
                }

                // Validar filtros
                if (string.IsNullOrEmpty(idioma) && string.IsNullOrEmpty(plataforma) && string.IsNullOrEmpty(genero) && string.IsNullOrEmpty(clasificacion))
                {
                    return BadRequest("Debes proporcionar al menos un filtro (idioma, plataforma, género o clasificación).");
                }

                // Obtener datos desde el servicio
                var (juegos, totalCount) = await _juegoService.GetJuegosFiltradosAsync(idioma, plataforma, genero, clasificacion, page, pageSize);

                if (juegos == null)
                {
                    return NotFound("No se encontraron juegos con los filtros especificados.");
                }

                // Devolver JSON con totalCount y los juegos
                var response = new JuegosFiltradosResponse
                {
                    Juegos = juegos,
                    TotalJuegos = totalCount,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // Endpoint para obtener juegos paginados
        [HttpGet("paginacion")]
        public async Task<IActionResult> GetJuegosPaginados([FromQuery] int cursor, [FromQuery] int limit = 10)
        {
            try
            {
                var (juegos, totalJuegos) = await _juegoService.GetJuegosPaginadosAsync(cursor, limit);

                // Preparar respuesta con el cursor para la siguiente página
                var nextCursor = juegos.LastOrDefault()?.Id;

                var response = new PaginacionJuegosResponse
                {
                    Juegos = juegos,
                    TotalJuegos = totalJuegos,
                    NextCursor = nextCursor
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
