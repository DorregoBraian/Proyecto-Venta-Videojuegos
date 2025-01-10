using Aplicacion.DTOs;
using AutoMapper;
using Dominio.Entidades;
using Dominio.IRepositorios;
using Dominio.IServicios;
using Microsoft.IdentityModel.Tokens;


namespace Aplicacion.Servicio
{
    public class JuegoService : IJuegoService
    {
        private readonly IJuegoRepository _juegoRepository;
        private readonly IMapper _mapper;

        public JuegoService(IJuegoRepository juegoRepository, IMapper mapper)
        {
            _juegoRepository = juegoRepository;
            _mapper = mapper;
        }

        // Devuelve todos los juegos
        public async Task<IEnumerable<JuegoDTO>> GetAllGamesAsync()
        {
            var juegos = await _juegoRepository.ReadAllGameAsync();
            if (juegos != null)
            {
                return _mapper.Map<IEnumerable<JuegoDTO>>(juegos);
            }
            throw new KeyNotFoundException("No se encontró ninguna juego");
        }

        // Obtiene un juego por su ID
        public async Task<JuegoDTO> GetGameByIdAsync(int id)
        {
            if (id > 0)
            {
                var juego = await _juegoRepository.ReadGameByIdAsync(id);
                if (juego != null)
                {
                    return _mapper.Map<JuegoDTO>(juego);
                }
                throw new ArgumentNullException(nameof(id), $"No se encontró un juego con el ID {id}.");
            }
            throw new ArgumentException("El ID es null o 0");
        }

        // Obtiene un juego por nombre
        public async Task<IEnumerable<JuegoDTO>> GetGameByNameAsync(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return new List<JuegoDTO>(); // Devuelve una lista vacía si el término está vacío o nulo
            }
            var juego = await _juegoRepository.ReadGameByNameAsync(nombre);
            // Si juegos es null, el operador ?? devuelve una lista vacía
            return _mapper.Map<IEnumerable<JuegoDTO>>(juego ?? new List<Juego>());
        }

        // Crea un nuevo juego a partir de un DTO
        public async Task<JuegoDTO> CreateGameAsync(CreateJuegoDTO juegoDto)
        {
            if (juegoDto != null)
            {
                var juego = _mapper.Map<Juego>(juegoDto);
                if (juego != null)
                {
                    await _juegoRepository.CreateGameAsync(juego);
                    return _mapper.Map<JuegoDTO>(juego);
                }
                throw new ArgumentNullException(nameof(juego), "El juego mapeado es nulo.");
            }
            throw new ArgumentNullException(nameof(juegoDto), "El DTO proporcionado es nulo.");
        }

        // Actualiza un juego existente mediante su ID
        public async Task<JuegoDTO> UpdateGameByIdAsync(int id, CreateJuegoDTO createJuegoDTO)
        {
            if (createJuegoDTO != null || id > 0)
            {
                var juegoExistente = await _juegoRepository.ReadGameByIdAsync(id);
                if (juegoExistente != null)
                {
                    await _juegoRepository.UpdateGameAsync(juegoExistente);
                    var juegoDto = _mapper.Map<JuegoDTO>(createJuegoDTO);
                    return juegoDto;

                }
                throw new ArgumentNullException($"No se encontró el juego con el ID {id}.");
            }
            throw new ArgumentNullException(nameof(createJuegoDTO), "El DTO proporcionado es nulo o el ID es 0.");
        }

        // Elimina un juego por su ID
        public async Task<JuegoDTO> DeleteGameByIdAsync(int id)
        {
            if (id > 0)
            {
                var juego = await _juegoRepository.ReadGameByIdAsync(id);
                if (juego != null)
                {
                    await _juegoRepository.DeleteGameByIdAsync(id);
                    var juegoDto = _mapper.Map<JuegoDTO>(juego);
                    return juegoDto;
                }
                throw new ArgumentNullException($"No se encontró el juego con el ID {id}.");
            }
            throw new ArgumentException("El ID es 0.");
        }

        // Obtener juegos filtrados por género
        public async Task<IEnumerable<JuegoDTO>> GetJuegosByGeneroAsync(string genero)
        {
            if (genero != null)
            {
                var juegos = await _juegoRepository.GetJuegosByGeneroAsync(genero);
                if (juegos != null)
                {
                    return _mapper.Map<IEnumerable<JuegoDTO>>(juegos);
                }
                throw new ArgumentNullException(nameof(genero), $"No se encontró un juego con el género {genero}.");
            }
            return new List<JuegoDTO>();
            throw new ArgumentNullException(nameof(genero), "El género es null.");
        }

        // Obtener juegos filtrados por plataforma
        public async Task<IEnumerable<JuegoDTO>> GetJuegosByPlataformaAsync(string plataforma)
        {
            if (plataforma != null)
            {
                var juegos = await _juegoRepository.GetJuegosByPlataformaAsync(plataforma);
                if (juegos != null)
                {
                    return _mapper.Map<IEnumerable<JuegoDTO>>(juegos);
                }
                throw new ArgumentNullException(nameof(plataforma), $"No se encontró un juego con la plataforma {plataforma}.");
            }
            return new List<JuegoDTO>();
            throw new ArgumentNullException(nameof(plataforma), "La plataforma es null.");
        }

        // Obtener juegos filtrados por idioma
        public async Task<IEnumerable<JuegoDTO>> GetJuegosByIdiomaAsync(string idioma)
        {
            if (idioma != null)
            {
                var juegos = await _juegoRepository.GetJuegosByIdiomaAsync(idioma);
                if (juegos != null)
                {
                    return _mapper.Map<IEnumerable<JuegoDTO>>(juegos);
                }
                throw new ArgumentNullException(nameof(idioma), $"No se encontró un juego con el idioma {idioma}.");
            }
            return new List<JuegoDTO>();
            throw new ArgumentNullException(nameof(idioma), "El idioma es null.");
        }

        // Obtener juegos filtrados por clasificación
        public async Task<IEnumerable<JuegoDTO>> GetJuegosByClasificacionAsync(string clasificacion)
        {
            if (clasificacion != null)
            {
                var juegos = await _juegoRepository.GetJuegosByClasificacionAsync(clasificacion);
                if (juegos != null)
                {
                    return _mapper.Map<IEnumerable<JuegoDTO>>(juegos);
                }
                throw new ArgumentNullException(nameof(clasificacion), $"No se encontró un juego con la clasificación {clasificacion}.");
            }
            return new List<JuegoDTO>();
            throw new ArgumentNullException(nameof(clasificacion), "La clasificación es null.");
        }

        // Obtener juegos filtrados con multiples filtros
        public async Task<(IEnumerable<JuegoDTO> Juegos, int totalJuegos)> GetJuegosFiltradosAsync(string idioma, string plataforma, string genero, string clasificacion, int page, int pageSize)
        {
            // Validar parámetros de paginación
            if (page <= 0 || pageSize <= 0)
            {
                throw new ArgumentException("El número de página y el tamaño de página deben ser mayores a 0.");
            }

            // Validar filtros
            if (string.IsNullOrEmpty(idioma) && string.IsNullOrEmpty(plataforma) && string.IsNullOrEmpty(genero) && string.IsNullOrEmpty(clasificacion))
            {
                throw new ArgumentException("Debes proporcionar al menos un filtro (idioma, plataforma, género o clasificación).");
            }

            // Llamar al repositorio
            var (juegos, totalJuegos) = await _juegoRepository.GetJuegosFiltradosAsync(idioma, plataforma, genero, clasificacion, page, pageSize);

            // Mapear los juegos a DTO
            var juegosDTO = _mapper.Map<IEnumerable<JuegoDTO>>(juegos);

            return (juegosDTO, totalJuegos); // Devolver como tupla

        }

        // Obtener juegos paginados
        public async Task<(IEnumerable<JuegoDTO> juegos, int totalJuegos)> GetJuegosPaginadosAsync(int cursor, int limit)
        {
            if (limit <= 0)
            {
                throw new ArgumentException("El límite debe ser mayor que 0.");
            }

            var (juegos, totalJuegos) = await _juegoRepository.GetJuegosPaginadosAsync(cursor, limit);

            // Mapear los juegos a DTOs
            var juegosDTO = _mapper.Map<IEnumerable<JuegoDTO>>(juegos);

            // Devolver los juegos DTO y el total de juegos
            return (juegosDTO, totalJuegos);
        }
    }
}

