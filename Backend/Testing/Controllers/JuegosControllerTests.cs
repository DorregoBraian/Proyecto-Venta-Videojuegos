using API_Rest_de_Videos_Juegos;
using API_Rest_de_Videos_Juegos.Controllers;
using Aplicacion.DTOs;
using AutoMapper;
using Dominio.Entidades;
using Dominio.IServicios;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Testing.Controllers
{
    public class JuegosControllerTests
    {
        private readonly Mock<IJuegoService> _mockJuegoService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly JuegosController _mockJuegosController;

        public JuegosControllerTests()
        {
            _mockJuegoService = new Mock<IJuegoService>();
            _mockMapper = new Mock<IMapper>();
            _mockJuegosController = new JuegosController(_mockJuegoService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetJuegosTests()
        {
            // Arrange
            var listJuegosDto = CreateListJuegoDTO();

            _mockJuegoService.Setup(service => service.GetAllGamesAsync()).ReturnsAsync(listJuegosDto);

            // Act
            var result = await _mockJuegosController.GetAllJuegos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<JuegoDTO>>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(listJuegosDto, okResult.Value);
            Assert.Equal(listJuegosDto.Count, returnValue.Count());
        }

        [Fact]
        public async Task GetJuegoByIdTests()
        {
            // Arrange
            var juegosDto = CreateSimpleJuegoDTO();

            _mockJuegoService.Setup(service => service.GetGameByIdAsync(juegosDto.Id)).ReturnsAsync(juegosDto);

            // Act
            var result = await _mockJuegosController.GetJuegoById(juegosDto.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<JuegoDTO>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(juegosDto, okResult.Value);
            Assert.Equal(juegosDto.Id, returnValue.Id);
            Assert.Equal(juegosDto.Titulo, returnValue.Titulo);
            Assert.Equal(juegosDto.Precio, returnValue.Precio);
            Assert.Equal(juegosDto.Portada, returnValue.Portada);
            Assert.Equal(juegosDto.Desarrollador, returnValue.Desarrollador);
            Assert.Equal(juegosDto.Descripcion, returnValue.Descripcion);
            Assert.Equal(juegosDto.Editor,returnValue.Editor);
            Assert.Equal(juegosDto.FechaDeLanzamiento,returnValue.FechaDeLanzamiento);
            Assert.Equal(juegosDto.Clasificacion, returnValue.Clasificacion);
            Assert.Equal(juegosDto.Generos, returnValue.Generos);
            Assert.Equal(juegosDto.Idiomas, returnValue.Idiomas);
            Assert.Equal(juegosDto.Plataformas, returnValue.Plataformas);
            Assert.Equal(juegosDto.Imagenes, returnValue.Imagenes);
            Assert.Equal(juegosDto.Videos, returnValue.Videos);

        }

       [Fact]
        public async Task GetJuegoByNameTests()
        {
            // Arrange
            var juegosList = CreateListJuegoDTO();

            _mockJuegoService.Setup(service => service.GetGameByNameAsync("Juego 1")).ReturnsAsync(juegosList);

            // Act
            var result = await _mockJuegosController.GetJuegoByName("Juego 1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<JuegoDTO>>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(juegosList, okResult.Value);
            Assert.Equal(juegosList.Count, returnValue.Count());
        }

         [Fact]
        public async Task CreateJuegoTests()
        {
            // Arrange
            var createJuegoDto = CreateSimpleCreateJuegoDTO();
            var juegoDto = CreateSimpleJuegoDTO();

            _mockJuegoService.Setup(service => service.CreateGameAsync(createJuegoDto)).ReturnsAsync(juegoDto);

            // Act
            var result = await _mockJuegosController.CreateJuego(createJuegoDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<JuegoDTO>(createdResult.Value);
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal(juegoDto, createdResult.Value);

        }

        [Fact]
        public async Task UpdateJuegoTests()
        {
            // Arrange
            var createJuegoDto = new CreateJuegoDTO { Titulo = "Juego Actualizado" };
            var juegoDto = CreateSimpleJuegoDTO();

            _mockJuegoService.Setup(service => service.GetGameByIdAsync(juegoDto.Id)).ReturnsAsync(juegoDto);
            _mockJuegoService.Setup(service => service.UpdateGameByIdAsync(juegoDto.Id, createJuegoDto)).ReturnsAsync(juegoDto);

            // Act
            var result = await _mockJuegosController.UpdateJuego(juegoDto.Id, createJuegoDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<JuegoDTO>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(juegoDto, okResult.Value);
        }

        [Fact]
        public async Task DeleteJuegoTests()
        {
            // Arrange
            var juegoDto = CreateSimpleJuegoDTO();

            _mockJuegoService.Setup(service => service.GetGameByIdAsync(juegoDto.Id)).ReturnsAsync(juegoDto);
            _mockJuegoService.Setup(service => service.DeleteGameByIdAsync(juegoDto.Id)).ReturnsAsync(juegoDto);

            // Act
            var result = await _mockJuegosController.DeleteJuego(juegoDto.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<JuegoDTO>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(juegoDto, okResult.Value);
        }

        [Fact]
        public async Task GetJuegosByGeneroTests()
        {
            // Arrange
            var juegosList = CreateListJuegoDTO();

            _mockJuegoService.Setup(service => service.GetJuegosByGeneroAsync("Aventura")).ReturnsAsync(juegosList);

            // Act
            var result = await _mockJuegosController.GetJuegosByGenero("Aventura");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<JuegoDTO>>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(juegosList.Count, returnValue.Count());

        }

        [Fact]
        public async Task GetJuegosByPlataformaTests()
        {
            // Arrange
            var juegosList = CreateListJuegoDTO();

            _mockJuegoService.Setup(service => service.GetJuegosByPlataformaAsync("PC")).ReturnsAsync(juegosList);

            // Act
            var result = await _mockJuegosController.GetJuegosByPlataforma("PC");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<JuegoDTO>>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(juegosList.Count, returnValue.Count());
        }

        [Fact]
        public async Task GetJuegosByIdiomaTests()
        {
            // Arrange
            var juegosList = CreateListJuegoDTO();

            _mockJuegoService.Setup(service => service.GetJuegosByIdiomaAsync("Español")).ReturnsAsync(juegosList);

            // Act
            var result = await _mockJuegosController.GetJuegosByIdioma("Español");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<JuegoDTO>>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(juegosList.Count, returnValue.Count());
        }

        [Fact]
        public async Task GetJuegosByClasificacionTests()
        {
            // Arrange
            var juegosList = CreateListJuegoDTO();

            _mockJuegoService.Setup(service => service.GetJuegosByClasificacionAsync("PEGI 12")).ReturnsAsync(juegosList);

            // Act
            var result = await _mockJuegosController.GetJuegosByClasificacion("PEGI 12");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<JuegoDTO>>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(juegosList.Count, returnValue.Count());
        }

        [Fact]
        public async Task GetJuegosPaginados_ReturnsOkResult()
        {
            // Arrange
            var juegosList = CreateListJuegoDTO();
            int cursor = 0;
            int limit = 2;
            int totalJuegos = juegosList.Count;
            var paginatedJuegos = juegosList.Take(limit).ToList();

            _mockJuegoService.Setup(service => service.GetJuegosPaginadosAsync(cursor, limit)).ReturnsAsync((paginatedJuegos, totalJuegos));

            // Act
            var result = await _mockJuegosController.GetJuegosPaginados(cursor, limit);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseValue = Assert.IsType<PaginacionJuegosResponse>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(paginatedJuegos, responseValue.Juegos);
            Assert.Equal(totalJuegos, responseValue.TotalJuegos);
            Assert.Equal(paginatedJuegos.LastOrDefault()?.Id, responseValue.NextCursor);
            Assert.IsType<int>(responseValue.TotalJuegos);
            Assert.IsType<int>(responseValue.NextCursor);
        }

        [Fact]
        public async Task FiltrarJuegos_ReturnsOkResult()
        {
            // Arrange
            var juegosList = CreateListJuegoDTO();
            string idioma = "Español";
            string plataforma = "PC";
            string genero = "Acción";
            string clasificacion = "PEGI 18";
            int page = 1;
            int pageSize = 2;
            int totalCount = juegosList.Count;

            _mockJuegoService.Setup(service => service.GetJuegosFiltradosAsync(idioma, plataforma, genero, clasificacion, page, pageSize)).ReturnsAsync((juegosList, totalCount));

            // Act
            var result = await _mockJuegosController.FiltrarJuegos(idioma, plataforma, genero, clasificacion, page, pageSize);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseValue = Assert.IsType<JuegosFiltradosResponse>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<int>(responseValue.TotalJuegos);
            Assert.IsAssignableFrom<IEnumerable<JuegoDTO>>(responseValue.Juegos);
        }

        // Método auxiliar para obtener una lista de juegos de prueba
        private List<CreateJuegoDTO> CreateListCreateJuego()
        {
            // Retorna una lista de juegos de prueba
            return new List<CreateJuegoDTO>
            {
                new CreateJuegoDTO
                {
                    Titulo = "Juego 1",
                    Precio = 50,
                    Portada = "portada1.jpg",
                    Descripcion = "Descripción del juego 1",
                    Desarrollador = "Desarrollador 1",
                    Editor = "Editor 1",
                    Clasificacion = "T",
                    FechaDeLanzamiento = DateTime.Now,
                    Generos = new List<string> { "Acción" },
                    Plataformas = new List<string> { "PC" },
                    Imagenes = new List<string> { "img1.jpg" },
                    Videos = new List<string> { "video1.mp4" },
                    Idiomas = new List<string> { "Español" }
                },
                new CreateJuegoDTO
                {
                    Titulo = "Juego 2",
                    Precio = 60,
                    Portada = "portada2.jpg",
                    Descripcion = "Descripción del juego 2",
                    Desarrollador = "Desarrollador 2",
                    Editor = "Editor 2",
                    Clasificacion = "M",
                    FechaDeLanzamiento = DateTime.Now,
                    Generos = new List<string> { "Aventura" },
                    Plataformas = new List<string> { "PS5" },
                    Imagenes = new List<string> { "img2.jpg" },
                    Videos = new List<string> { "video2.mp4" },
                    Idiomas = new List<string> { "Inglés" }
                },
                new CreateJuegoDTO
                {
                    Titulo = "Juego 3",
                    Precio = 70,
                    Portada = "portada3.jpg",
                    Descripcion = "Descripción del juego 3",
                    Desarrollador = "Desarrollador 3",
                    Editor = "Editor 3",
                    Clasificacion = "E",
                    FechaDeLanzamiento = DateTime.Now,
                    Generos = new List<string> { "RPG" },
                    Plataformas = new List<string> { "Xbox" },
                    Imagenes = new List<string> { "img3.jpg" },
                    Videos = new List<string> { "video3.mp4" },
                    Idiomas = new List<string> { "Francés" }
                }
            };
        }

        private List<JuegoDTO> CreateListJuegoDTO()
        {
            // Retorna una lista de juegos de prueba
            return new List<JuegoDTO>
            {
                new JuegoDTO
                {
                    Id = 1,
                    Titulo = "Juego 1",
                    Precio = 50,
                    Portada = "portada1.jpg",
                    Descripcion = "Descripción del juego 1",
                    Desarrollador = "Desarrollador 1",
                    Editor = "Editor 1",
                    Clasificacion = "T",
                    FechaDeLanzamiento = DateTime.Now,
                    Generos = new List<string> { "Acción" },
                    Plataformas = new List<string> { "PC" },
                    Imagenes = new List<string> { "img1.jpg" },
                    Videos = new List<string> { "video1.mp4" },
                    Idiomas = new List<string> { "Español" }
                },
                new JuegoDTO
                {
                    Id= 2,
                    Titulo = "Juego 2",
                    Precio = 60,
                    Portada = "portada2.jpg",
                    Descripcion = "Descripción del juego 2",
                    Desarrollador = "Desarrollador 2",
                    Editor = "Editor 2",
                    Clasificacion = "M",
                    FechaDeLanzamiento = DateTime.Now,
                    Generos = new List<string> { "Aventura" },
                    Plataformas = new List<string> { "PS5" },
                    Imagenes = new List<string> { "img2.jpg" },
                    Videos = new List<string> { "video2.mp4" },
                    Idiomas = new List<string> { "Inglés" }
                },
                new JuegoDTO
                {
                    Id = 3,
                    Titulo = "Juego 3",
                    Precio = 70,
                    Portada = "portada3.jpg",
                    Descripcion = "Descripción del juego 3",
                    Desarrollador = "Desarrollador 3",
                    Editor = "Editor 3",
                    Clasificacion = "E",
                    FechaDeLanzamiento = DateTime.Now,
                    Generos = new List<string> { "RPG" },
                    Plataformas = new List<string> { "Xbox" },
                    Imagenes = new List<string> { "img3.jpg" },
                    Videos = new List<string> { "video3.mp4" },
                    Idiomas = new List<string> { "Francés" }
                }
            };
        }

        // Método auxiliar para crear un solo objeto de JuegoDTO con datos de ejemplo
        private CreateJuegoDTO CreateSimpleCreateJuegoDTO()
        {
            return new CreateJuegoDTO
            {
                Titulo = "Juego de Ejemplo",
                Precio = 60,
                Portada = "url_imagen_juego_ejemplo",
                Descripcion = "Este es un juego de ejemplo para propósitos de prueba.",
                Desarrollador = "Desarrollador Ejemplo",
                Editor = "Editor Ejemplo",
                Clasificacion = "PEGI 12",
                FechaDeLanzamiento = new DateTime(2023, 5, 10),
                Generos = new List<string> { "Acción", "Aventura" },
                Plataformas = new List<string> { "PC", "PlayStation" },
                Imagenes = new List<string> { "imagen_ejemplo1.jpg", "imagen_ejemplo2.jpg" },
                Videos = new List<string> { "video_ejemplo1.mp4" },
                Idiomas = new List<string> { "Español", "Inglés" },
            };
        }
        
        private JuegoDTO CreateSimpleJuegoDTO()
        {
            return new JuegoDTO
            {
                Id = 1,
                Titulo = "Juego de Ejemplo",
                Precio = 60,
                Portada = "url_imagen_juego_ejemplo",
                Descripcion = "Este es un juego de ejemplo para propósitos de prueba.",
                Desarrollador = "Desarrollador Ejemplo",
                Editor = "Editor Ejemplo",
                Clasificacion = "PEGI 12",
                FechaDeLanzamiento = new DateTime(2023, 5, 10),
                Generos = new List<string> { "Acción", "Aventura" },
                Plataformas = new List<string> { "PC", "PlayStation" },
                Imagenes = new List<string> { "imagen_ejemplo1.jpg", "imagen_ejemplo2.jpg" },
                Videos = new List<string> { "video_ejemplo1.mp4" },
                Idiomas = new List<string> { "Español", "Inglés" },
            };
        }
    }
}
