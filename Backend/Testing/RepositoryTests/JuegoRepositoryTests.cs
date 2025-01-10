using Dominio.Entidades;
using Infraestructura;
using Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Testing.RepositoryTests
{
    // Clase de prueba para el repositorio de juegos
    public class JuegoRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly JuegoRepository _repository;

        // Configuración inicial antes de cada prueba
        public JuegoRepositoryTests()
        {
            // Configura el contexto de la base de datos en memoria
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "JuegoTestDB")
                .Options;

            // Crea una nueva instancia del contexto en memoria
            _context = new ApplicationDbContext(options);

            // Inicia el repositorio con el contexto
            _repository = new JuegoRepository(_context);
        }

        // Test para leer un juego por ID
        [Fact]
        public async Task ReadGameByIdAsyncTests()
        {
            // Arrange: Creamos un juego de prueba
            var juego = CreateSampleJuego();

            // Agregamos el juego a la base de datos
            await _repository.CreateGameAsync(juego);

            // Act: Ejecutamos el método que queremos probar
            var result = await _repository.ReadGameByIdAsync(juego.Id);

            // Assert: Comprobamos si el resultado es el esperado
            Assert.NotNull(result);
            Assert.Equal(juego.Titulo, result.Titulo);
        }

        // Test para leer un juego por nombre
        [Fact]
        public async Task ReadGameByNameAsync_ShouldReturnListOfGames_WhenGamesExist()
        {
            // Arrange: Creamos una lista de juegos con nombres que coincidan parcialmente
            var nombreBusqueda = "Juego de prueba";
            var juegos = CreateSampleGamesList(3);


            // Agregamos los juegos a la base de datos simulada
            foreach (var juego in juegos)
            {
                await _repository.CreateGameAsync(juego);
            }

            // Act: Ejecutamos el método de búsqueda por nombre
            var result = await _repository.ReadGameByNameAsync(nombreBusqueda);

            // Assert: Verificamos que el resultado sea correcto
            Assert.NotNull(result); // Verificamos que no sea nulo
            Assert.NotEmpty(result); // Verificamos que haya elementos en la lista
            Assert.Equal(3, result.Count()); // Verificamos que se obtengan exactamente 3 juegos
        }

        [Fact]
        public async Task ReadGameByNameAsync_ShouldReturnEmptyList_WhenNoGamesExist()
        {
            // Arrange: Definimos un nombre de búsqueda que no coincida con ningún juego
            var nombreBusqueda = "Juego Inexistente";

            // Act: Ejecutamos el método de búsqueda
            var result = await _repository.ReadGameByNameAsync(nombreBusqueda);

            // Assert: Verificamos que el resultado sea una lista vacía
            Assert.NotNull(result); // La lista no debe ser nula
            Assert.Empty(result); // Debe estar vacía
        }

        // Test para crear un nuevo juego
        [Fact]
        public async Task CreateGameAsyncTests()
        {
            // Arrange: Creamos un juego para agregarlo a la base de datos
            var juego = CreateSampleJuego();

            // Act: Intentamos crear el juego en la base de datos
            await _repository.CreateGameAsync(juego);

            // Assert: Comprobamos que el juego fue añadido correctamente
            var result = await _context.Juegos.FindAsync(juego.Id);
            Assert.NotNull(result);
            Assert.Equal(juego.Titulo, result.Titulo);
        }

        // Test para eliminar un juego por ID
        [Fact]
        public async Task DeleteGameByIdAsyncTests()
        {
            // Arrange: Creamos un juego de prueba
            var juego = CreateSampleJuego();
            await _repository.CreateGameAsync(juego);

            // Act: Eliminamos el juego por su ID
            await _repository.DeleteGameByIdAsync(juego.Id);

            // Assert: Verificamos que el juego ha sido eliminado
            var result = await _context.Juegos.FindAsync(juego.Id);
            Assert.Null(result);
        }

        // Método para crear un juego de ejemplo
        private Juego CreateSampleJuego()
        {
            return new Juego
            {
                Id = 1,  // O puedes quitar este Id si la base de datos lo genera automáticamente
                Titulo = "Resident Evil 4 REMASTERED",
                Precio = 35984,
                Portada = "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/2050650/header.jpg",
                Descripcion = "Seis años han pasado desde el desastre biológico en Raccoon City...",
                Desarrollador = "CAPCOM",
                Editor = "CAPCOM",
                Clasificacion = new Clasificacion
                {
                    Id = 5,
                    Nombre = "PEGI 18",
                    Descripcion = "Adecuado para mayores de 18 años"
                },
                FechaDeLanzamiento = new DateTime(2023, 3, 23),
                Idiomas = new List<Idioma>
                {
                new Idioma { Nombre = "Español" },
                new Idioma { Nombre = "Ingles" },
                new Idioma { Nombre = "Frances" },
                new Idioma { Nombre = "Italiano" },
                new Idioma { Nombre = "Aleman" },
                new Idioma { Nombre = "Japones" },
                new Idioma { Nombre = "Chino" },
                new Idioma { Nombre = "Portugues" },
                new Idioma { Nombre = "Ruso" }
                },
                Generos = new List<Genero>
                {
                new Genero { Nombre = "Aventura" },
                new Genero { Nombre = "Accion" },
                new Genero { Nombre = "Survival Horror" }
                },
                Plataformas = new List<Plataforma>
                {
                new Plataforma { Nombre = "PC" },
                new Plataforma { Nombre = "PlayStation" },
                new Plataforma { Nombre = "Xbox" },
                new Plataforma { Nombre = "Nintendo Switch" }
                },
                Imagenes = new List<Imagen>
                {
                new Imagen { Url = "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/2050650/ss_d90819dc43141eee26b69a6cab43be00164adcb0.600x338.jpg" },
                new Imagen { Url = "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/2050650/ss_5a4297e594297a13f1f4c665966eb3d88d37b58d.600x338.jpg" },
                new Imagen { Url = "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/2050650/ss_29ffb23060c862bcbe1d1434e83d41ab10484d8e.600x338.jpg" }
                },
                Videos = new List<Video>
                {
                new Video { Url = "https://youtu.be/htF0IryqWK8" },
                new Video { Url = "https://youtu.be/lRYsWx0stFA" }
                }
            };
        }

        private List<Juego> CreateSampleGamesList(int count = 5)
        {
            var juegos = new List<Juego>();

            for (int i = 1; i <= count; i++)
            {
                juegos.Add(new Juego
                {
                    Id = i, // Cada juego tiene un ID único
                    Titulo = $"Juego de prueba {i}",
                    Precio = 30000 + (i * 1000), // Diferente precio para cada juego
                    Portada = $"https://example.com/game_{i}/header.jpg",
                    Descripcion = $"Descripción del juego de prueba {i}.",
                    Desarrollador = $"Desarrollador {i}",
                    Editor = $"Editor {i}",
                    Clasificacion = new Clasificacion
                    {
                        Id = i,
                        Nombre = $"Clasificación {i}",
                        Descripcion = $"Descripción de clasificación {i}"
                    },
                    FechaDeLanzamiento = new DateTime(2023, 1, 1).AddMonths(i), // Fechas de lanzamiento únicas
                    Idiomas = new List<Idioma>
                    {
                        new Idioma { Nombre = "Español" },
                        new Idioma { Nombre = "Inglés" }
                    },
                    Generos = new List<Genero>
                    {
                        new Genero { Nombre = "Acción" },
                        new Genero { Nombre = "Aventura" }
                    },
                    Plataformas = new List<Plataforma>
                    {
                        new Plataforma { Nombre = "PC" },
                        new Plataforma { Nombre = "Xbox" }
                    },
                    Imagenes = new List<Imagen>
                    {
                        new Imagen { Url = $"https://example.com/game_{i}/image1.jpg" },
                        new Imagen { Url = $"https://example.com/game_{i}/image2.jpg" }
                    },
                    Videos = new List<Video>
                    {
                        new Video { Url = $"https://example.com/game_{i}/trailer1.mp4" },
                        new Video { Url = $"https://example.com/game_{i}/trailer2.mp4" }
                    }
                });
            }

            return juegos;
        }


        // Método Dispose para limpiar los recursos después de cada prueba
        public void Dispose()
        {
            _context.Database.EnsureDeleted(); // Elimina la base de datos en memoria al finalizar las pruebas
            _context.Dispose();
        }
    }
}
