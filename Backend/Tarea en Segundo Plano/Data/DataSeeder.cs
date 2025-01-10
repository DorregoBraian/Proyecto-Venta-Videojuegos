using AutoMapper;
using Dominio.Entidades;
using Dominio.IRepositorios;
using Newtonsoft.Json;


namespace Tarea_en_Segundo_Plano.Data
{
    public class DataSeeder
    {
        private readonly IJuegoRepository _juegoRepository;
        private readonly IGeneroRepository _generoRepository;
        private readonly IPlataformaRepository _plataformaRepository;
        private readonly IClasificacionRepository _clasificacionRepository;
        private readonly IIdiomaRepository _idiomaRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DataSeeder> _logger;

        public DataSeeder(
            IJuegoRepository juegoRepository,
            IGeneroRepository generoRepository,
            IPlataformaRepository plataformaRepository,
            IClasificacionRepository clasificacionRepository,
            IIdiomaRepository idiomaRepository,
            IMapper mapper,
            ILogger<DataSeeder> logger)
        {
            _juegoRepository = juegoRepository;
            _generoRepository = generoRepository;
            _plataformaRepository = plataformaRepository;
            _clasificacionRepository = clasificacionRepository;
            _idiomaRepository = idiomaRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task SeedDataAsync(string jsonFilePath)
        {
            try
            {
                _logger.LogInformation("Iniciando la carga de datos desde el archivo JSON.");

                // Verificar si el archivo existe
                if (!File.Exists(jsonFilePath))
                {
                    _logger.LogError($"El archivo JSON no se encontró en la ruta especificada: {jsonFilePath}");
                    return;
                }

                // Leer y deserializar el archivo JSON
                var jsonData = await File.ReadAllTextAsync(jsonFilePath);
                var juegosJson = JsonConvert.DeserializeObject<List<JuegoJsonModel>>(jsonData);

                if (juegosJson == null || !juegosJson.Any())
                {
                    _logger.LogError("El archivo JSON no contiene datos válidos de juegos.");
                    return;
                }

                // Obtener todos los juegos existentes de la base de datos
                var existingJuegos = await _juegoRepository.ReadAllGameAsync();

                // Procesar los juegos del JSON
                foreach (var juegoJson in juegosJson)
                {
                    // Comprobar si el juego ya existe en la base de datos (por título)
                    var existingJuego = existingJuegos.FirstOrDefault(j => j.Titulo == juegoJson.Titulo);

                    // Convertir la fecha de lanzamiento a UTC
                    var fechaUTC = DateTime.Parse(juegoJson.FechaDeLanzamiento).ToUniversalTime();

                    // Mapear el juego desde el modelo JSON
                    var juego = _mapper.Map<Juego>(juegoJson);
                    juego.FechaDeLanzamiento = fechaUTC;

                    // Manejar las video
                    juego.Videos = juegoJson.Videos.Select(video => new Video { Url = video }).ToList();

                    // Manejar las imágenes
                    juego.Imagenes = juegoJson.Imagenes.Select(imagen => new Imagen { Url = imagen }).ToList();

                    // Manejar Géneros
                    juego.Generos = await ProcesarGenerosAsync(juegoJson.Generos);

                    // Manejar Plataformas
                    juego.Plataformas = await ProcesarPlataformasAsync(juegoJson.Plataformas);

                    // Manejar Clasificación
                    juego.Clasificacion = await ObtenerClasificacionAsync(juegoJson.Clasificacion);

                    // Manejar Idiomas
                    juego.Idiomas = await ProcesarIdiomasAsync(juegoJson.Idioma);

                    if (existingJuego != null)
                    {
                        // Determinar si es necesario actualizar
                        bool necesitaActualizar = false;

                        // Comparar campos básicos
                        if (existingJuego.FechaDeLanzamiento != juego.FechaDeLanzamiento ||
                            existingJuego.Titulo != juego.Titulo ||
                            existingJuego.Portada != juego.Portada ||
                            existingJuego.Precio != juego.Precio ||
                            existingJuego.Descripcion != juego.Descripcion ||
                            existingJuego.Desarrollador != juego.Desarrollador ||
                            existingJuego.Editor != juego.Editor ||
                            existingJuego.Generos.Count != juego.Generos.Count ||
                            existingJuego.Plataformas.Count != juego.Plataformas.Count ||
                            existingJuego.Clasificacion.Nombre != juego.Clasificacion.Nombre ||
                            existingJuego.Idiomas.Count != juego.Idiomas.Count)
                        {
                            necesitaActualizar = true;
                        }

                        // Actualizar imágenes si hay cambios
                        if (!existingJuego.Imagenes.Select(img => img.Url)
                            .SequenceEqual(juego.Imagenes.Select(img => img.Url)))
                        {
                            ActualizarLista(existingJuego.Imagenes, juego.Imagenes);
                            necesitaActualizar = true;
                        }

                        // Actualizar videos si hay cambios
                        if (!existingJuego.Videos.Select(vid => vid.Url)
                            .SequenceEqual(juego.Videos.Select(vid => vid.Url)))
                        {
                            ActualizarLista(existingJuego.Videos, juego.Videos);
                            necesitaActualizar = true;
                        }

                        if (necesitaActualizar)
                        {
                            // Actualizar datos principales
                            existingJuego.Titulo = juego.Titulo;
                            existingJuego.Portada = juego.Portada;
                            existingJuego.Precio = juego.Precio;
                            existingJuego.Descripcion = juego.Descripcion;
                            existingJuego.Desarrollador = juego.Desarrollador;
                            existingJuego.Editor = juego.Editor;
                            existingJuego.FechaDeLanzamiento = juego.FechaDeLanzamiento;
                            existingJuego.Generos = juego.Generos;
                            existingJuego.Plataformas = juego.Plataformas;
                            existingJuego.Clasificacion = juego.Clasificacion;
                            existingJuego.Idiomas = juego.Idiomas;

                            // Guardar los cambios
                            await _juegoRepository.UpdateGameAsync(existingJuego);
                            _logger.LogInformation($"Juego '{juego.Titulo}' actualizado correctamente.");
                        }
                        else
                        {
                            _logger.LogInformation($"El juego '{juego.Titulo}' ya existe con los mismos datos.");
                        }
                    }
                    else
                    {
                        // Si el juego no existe, lo agregamos a la base de datos
                        await _juegoRepository.CreateGameAsync(juego);
                        _logger.LogInformation($"Juego '{juego.Titulo}' agregado correctamente.");
                    }
                }

                _logger.LogInformation("Carga de datos completada exitosamente.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error durante la carga de datos.");
                throw;
            }
        }

        //Método auxiliar para actualizar listas
        private void ActualizarLista<T>(ICollection<T> entidadLista, ICollection<T> nuevaLista) where T : class
        {
            entidadLista.Clear();
            foreach (var item in nuevaLista)
            {
                entidadLista.Add(item);
            }
        }

        // Método auxiliar para procesar los géneros
        private async Task<List<Genero>> ProcesarGenerosAsync(IEnumerable<string> generos)
        {
            var listaGeneros = new List<Genero>();

            foreach (var generoNombre in generos.Distinct())
            {
                var genero = await _generoRepository.ReadGeneroByNameAsync(generoNombre)
                            ?? new Genero { Nombre = generoNombre };

                listaGeneros.Add(genero);
            }

            return listaGeneros;
        }

        // Método auxiliar para procesar las plataformas
        private async Task<List<Plataforma>> ProcesarPlataformasAsync(IEnumerable<string> plataformas)
        {
            var listaPlataformas = new List<Plataforma>();

            foreach (var plataformaNombre in plataformas.Distinct())
            {
                var plataforma = await _plataformaRepository.ReadPlataformaByNameAsync(plataformaNombre)
                              ?? new Plataforma { Nombre = plataformaNombre };

                listaPlataformas.Add(plataforma);
            }

            return listaPlataformas;
        }

        // Método auxiliar para obtener clasificación
        private async Task<Clasificacion> ObtenerClasificacionAsync(string clasificacionNombre)
        {
            return await _clasificacionRepository.ReadClasificacionByNameAsync(clasificacionNombre)
                   ?? new Clasificacion { Nombre = clasificacionNombre };
        }

        // Método auxiliar para procesar idiomas
        private async Task<List<Idioma>> ProcesarIdiomasAsync(IEnumerable<string> idiomas)
        {
            var listaIdiomas = new List<Idioma>();

            foreach (var idiomaNombre in idiomas.Distinct())
            {
                var idioma = await _idiomaRepository.ReadIdiomaByNameAsync(idiomaNombre)
                            ?? new Idioma { Nombre = idiomaNombre };

                listaIdiomas.Add(idioma);
            }

            return listaIdiomas;
        }
    }
}
