using Aplicacion.DTOs;
using Aplicacion.Servicio;
using AutoMapper;
using Dominio.Entidades;
using Dominio.IRepositorios;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.ServiceTests
{
    public class JuegoServiceTests
    {
        private readonly Mock<IJuegoRepository> _mockRepository;
        private readonly Mock<IMapper> _mapper; // Mock del mapeador
        private readonly JuegoService _juegoService;

        public JuegoServiceTests()
        {
            _mockRepository = new Mock<IJuegoRepository>();
            _mapper = new Mock<IMapper>();
            _juegoService = new JuegoService(_mockRepository.Object, _mapper.Object);
        }

        // Prueba para obtener todos los juegos
        [Fact]
        public async Task GetAllGamesAsyncTests()
        {
            // Arrange
            var juegosList = CreateJuegoList(5);
            var juegosDTOList = CreateJuegoDTOList(5);

            _mockRepository.Setup(repo => repo.ReadAllGameAsync()).ReturnsAsync(juegosList);
            _mapper.Setup(mapper => mapper.Map<IEnumerable<JuegoDTO>>(juegosList)).Returns(juegosDTOList);

            // Act
            var result = await _juegoService.GetAllGamesAsync();

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(5, result.Count()); // Verifica que se devuelvan 5 juegos
            Assert.IsType<List<JuegoDTO>>(result); // Verifica que el tipo de retorno sea una lista de JuegoDTO

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockRepository.Verify(repo => repo.ReadAllGameAsync(), Times.Once);
            _mapper.Verify(mapper => mapper.Map<IEnumerable<JuegoDTO>>(juegosList), Times.Once);
        }

        [Fact]
        public async Task GetGameByIdAsyncTests()
        {
            // Arrange
            var juego = CreateSimpleJuego();
            var juegoDTO = CreateSimpleJuegoDTO();

            _mockRepository.Setup(repo => repo.ReadGameByIdAsync(juego.Id)).ReturnsAsync(juego);
            _mapper.Setup(mapper => mapper.Map<JuegoDTO>(juego)).Returns(juegoDTO);

            // Act
            var result = await _juegoService.GetGameByIdAsync(juegoDTO.Id);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(juegoDTO.Id, result.Id); // Verifica que el ID sea correcto
            Assert.Equal(juegoDTO.Titulo, result.Titulo); // Verifica que el título sea correcto
            Assert.IsType<JuegoDTO>(result); // Verifica que el resultado sea del tipo correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockRepository.Verify(repo => repo.ReadGameByIdAsync(juego.Id), Times.Once);
            _mapper.Verify(mapper => mapper.Map<JuegoDTO>(juego), Times.Once);
        }

        [Fact]
        public async Task CreateGameAsyncTests()
        {
            // Arrange
            var createJuegoDto = CreateSimpleCreateJuego();
            var juego = CreateSimpleJuego();
            var juegoDto = CreateSimpleJuegoDTO();

            // Configuración de los mocks
            _mapper.Setup(mapper => mapper.Map<Juego>(createJuegoDto)).Returns(juego);
            _mockRepository.Setup(repo => repo.CreateGameAsync(It.IsAny<Juego>())).ReturnsAsync(juego);
            _mapper.Setup(mapper => mapper.Map<JuegoDTO>(juego)).Returns(juegoDto);

            // Act
            var result = await _juegoService.CreateGameAsync(createJuegoDto);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(juegoDto.Id, result.Id); // Verifica el ID del juego creado
            Assert.Equal(juegoDto.Titulo, result.Titulo); // Verifica el título
            Assert.Equal(juegoDto.Precio, result.Precio); // Verifica el precio
            Assert.Equal(juegoDto.Descripcion, result.Descripcion); // Verifica la descripción
            Assert.Equal(juegoDto.Desarrollador, result.Desarrollador); // Verifica el desarrollador
            Assert.Equal(juegoDto.Editor, result.Editor); // Verifica el editor
            Assert.Equal(juegoDto.Clasificacion, result.Clasificacion); // Verifica la clasificación
            Assert.Equal(juegoDto.FechaDeLanzamiento, result.FechaDeLanzamiento); // Verifica la fecha de lanzamiento
            Assert.Equal(juegoDto.Generos, result.Generos); // Verifica los géneros
            Assert.Equal(juegoDto.Plataformas, result.Plataformas); // Verifica las plataformas
            Assert.Equal(juegoDto.Idiomas, result.Idiomas); // Verifica los idiomas
            Assert.Equal(juegoDto.Imagenes, result.Imagenes); // Verifica las imágenes
            Assert.Equal(juegoDto.Videos, result.Videos); // Verifica los videos
            Assert.IsType<JuegoDTO>(result); // Verifica el tipo de retorno

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mapper.Verify(mapper => mapper.Map<Juego>(createJuegoDto), Times.Once);
            _mockRepository.Verify(repo => repo.CreateGameAsync(It.IsAny<Juego>()), Times.Once);
            _mapper.Verify(mapper => mapper.Map<JuegoDTO>(juego), Times.Once);
        }

        [Fact]
        public async Task UpdateGameByIdAsyncTests()
        {
            // Arrange
            int id = 1;
            var createJuegoDto = CreateSimpleCreateJuego();
            var juego = CreateSimpleJuego(id);
            var juegoDto = CreateSimpleJuegoDTO(id);

            // Configuración de los mocks
            _mockRepository.Setup(repo => repo.ReadGameByIdAsync(id)).ReturnsAsync(juego);
            _mockRepository.Setup(repo => repo.UpdateGameAsync(It.IsAny<Juego>())).ReturnsAsync(juego);
            _mapper.Setup(m => m.Map<JuegoDTO>(createJuegoDto)).Returns(juegoDto);

            // Act
            var result = await _juegoService.UpdateGameByIdAsync(id, createJuegoDto);

            // Assert
            // Verifica que el resultado no es nulo
            Assert.NotNull(result);

            // Verifica que el ID y los datos actualizados sean correctos
            Assert.Equal(juegoDto.Id, result.Id);
            Assert.Equal(juegoDto.Titulo, result.Titulo);
            Assert.Equal(juegoDto.Precio, result.Precio);
            Assert.Equal(juegoDto.Descripcion, result.Descripcion);
            Assert.Equal(juegoDto.Desarrollador, result.Desarrollador);
            Assert.Equal(juegoDto.Editor, result.Editor);
            Assert.Equal(juegoDto.Clasificacion, result.Clasificacion);
            Assert.Equal(juegoDto.FechaDeLanzamiento, result.FechaDeLanzamiento);
            Assert.Equal(juegoDto.Generos, result.Generos);
            Assert.Equal(juegoDto.Plataformas, result.Plataformas);
            Assert.Equal(juegoDto.Idiomas, result.Idiomas);
            Assert.Equal(juegoDto.Imagenes, result.Imagenes);
            Assert.Equal(juegoDto.Videos, result.Videos);

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockRepository.Verify(repo => repo.ReadGameByIdAsync(id), Times.Once);
            _mockRepository.Verify(repo => repo.UpdateGameAsync(It.IsAny<Juego>()), Times.Once);
            _mapper.Verify(m => m.Map<JuegoDTO>(createJuegoDto), Times.Once);
        }

        [Fact]
        public async Task DeleteGameByIdAsyncTests()
        {
            // Arrange
            int id = 1;
            var juego = CreateSimpleJuego(id);

            // Configuración de los mocks
            _mockRepository.Setup(repo => repo.ReadGameByIdAsync(id)).ReturnsAsync(juego);
            _mockRepository.Setup(repo => repo.DeleteGameByIdAsync(id)).Returns(Task.CompletedTask);
            _mapper.Setup(mapper => mapper.Map<JuegoDTO>(juego)).Returns(CreateSimpleJuegoDTO());


            // Act
            var result = await _juegoService.DeleteGameByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(juego.Id, result.Id);
            Assert.Equal(juego.Titulo, result.Titulo);
            Assert.Equal(juego.Precio, result.Precio);
            Assert.Equal(juego.Descripcion, result.Descripcion);
            Assert.Equal(juego.Desarrollador, result.Desarrollador);
            Assert.Equal(juego.Editor, result.Editor);
            Assert.Equal(juego.Clasificacion.Nombre, result.Clasificacion);
            //Assert.Equal(juego.FechaDeLanzamiento, result.FechaDeLanzamiento);
            Assert.Equal(juego.Generos.Select(g => g.Nombre), result.Generos);
            Assert.Equal(juego.Plataformas.Select(p => p.Nombre), result.Plataformas);
            Assert.Equal(juego.Idiomas.Select(i => i.Nombre), result.Idiomas);
            Assert.Equal(juego.Imagenes.Select(i => i.Url), result.Imagenes);
            Assert.Equal(juego.Videos.Select(v => v.Url), result.Videos);

            // Verifica que los métodos del repositorio se llamaron correctamente
            _mockRepository.Verify(repo => repo.ReadGameByIdAsync(id), Times.Once);
            _mockRepository.Verify(repo => repo.DeleteGameByIdAsync(juego.Id), Times.Once);
        }


        [Fact]
        public async Task GetJuegosByGeneroAsyncTests()
        {
            // Arrange
            string genero = "Acción";
            var juegos = CreateJuegoList();
            var juegosDto = CreateJuegoDTOList();

            _mockRepository.Setup(repo => repo.GetJuegosByGeneroAsync(genero)).ReturnsAsync(juegos);
            _mapper.Setup(m => m.Map<IEnumerable<JuegoDTO>>(juegos)).Returns(juegosDto);

            // Act
            var result = await _juegoService.GetJuegosByGeneroAsync(genero);

            // Assert
            Assert.NotNull(result); // Asegúrate de que el resultado no sea nulo
            Assert.IsAssignableFrom<IEnumerable<JuegoDTO>>(result); // Verifica que el resultado sea del tipo esperado
            Assert.Equal(juegosDto.Count(), result.Count()); // Asegúrate de que el conteo coincida

            // Verifica que los juegos retornados tienen las mismas propiedades que los DTOs simulados
            for (int i = 0; i < juegosDto.Count(); i++)
            {
                Assert.Equal(juegosDto.ElementAt(i).Id, result.ElementAt(i).Id);
                Assert.Equal(juegosDto.ElementAt(i).Titulo, result.ElementAt(i).Titulo);
                Assert.Equal(juegosDto.ElementAt(i).Precio, result.ElementAt(i).Precio);
                Assert.Equal(juegosDto.ElementAt(i).Descripcion, result.ElementAt(i).Descripcion);
                Assert.Equal(juegosDto.ElementAt(i).Desarrollador, result.ElementAt(i).Desarrollador);
                Assert.Equal(juegosDto.ElementAt(i).Editor, result.ElementAt(i).Editor);
                Assert.Equal(juegosDto.ElementAt(i).FechaDeLanzamiento, result.ElementAt(i).FechaDeLanzamiento);
                Assert.Equal(juegosDto.ElementAt(i).Clasificacion, result.ElementAt(i).Clasificacion);
                Assert.Equal(juegosDto.ElementAt(i).Generos, result.ElementAt(i).Generos);
                Assert.Equal(juegosDto.ElementAt(i).Plataformas, result.ElementAt(i).Plataformas);
                Assert.Equal(juegosDto.ElementAt(i).Idiomas, result.ElementAt(i).Idiomas);
                Assert.Equal(juegosDto.ElementAt(i).Imagenes, result.ElementAt(i).Imagenes);
                Assert.Equal(juegosDto.ElementAt(i).Videos, result.ElementAt(i).Videos);
            }

            // Verifica que el repositorio y el mapper se llamaron correctamente
            _mockRepository.Verify(repo => repo.GetJuegosByGeneroAsync(genero), Times.Once);
            _mapper.Verify(m => m.Map<IEnumerable<JuegoDTO>>(juegos), Times.Once);
        }

        [Fact]
        public async Task GetJuegosByPlataformaAsyncTests()
        {
            // Arrange
            string plataforma = "Nintendo";
            var juegos = CreateJuegoList();
            var juegosDto = CreateJuegoDTOList();

            _mockRepository.Setup(repo => repo.GetJuegosByPlataformaAsync(plataforma)).ReturnsAsync(juegos);
            _mapper.Setup(m => m.Map<IEnumerable<JuegoDTO>>(juegos)).Returns(juegosDto);

            // Act
            var result = await _juegoService.GetJuegosByPlataformaAsync(plataforma);

            // Assert
            Assert.NotNull(result); // Asegúrate de que el resultado no sea nulo
            Assert.IsAssignableFrom<IEnumerable<JuegoDTO>>(result); // Verifica que el resultado sea del tipo esperado
            Assert.Equal(juegosDto.Count(), result.Count()); // Asegúrate de que el conteo coincida
            // Verifica que los juegos retornados tienen las mismas propiedades que los DTOs simulados
            for (int i = 0; i < juegosDto.Count(); i++)
            {
                Assert.Equal(juegosDto.ElementAt(i).Id, result.ElementAt(i).Id);
                Assert.Equal(juegosDto.ElementAt(i).Titulo, result.ElementAt(i).Titulo);
                Assert.Equal(juegosDto.ElementAt(i).Precio, result.ElementAt(i).Precio);
                Assert.Equal(juegosDto.ElementAt(i).Descripcion, result.ElementAt(i).Descripcion);
                Assert.Equal(juegosDto.ElementAt(i).Desarrollador, result.ElementAt(i).Desarrollador);
                Assert.Equal(juegosDto.ElementAt(i).Editor, result.ElementAt(i).Editor);
                Assert.Equal(juegosDto.ElementAt(i).FechaDeLanzamiento, result.ElementAt(i).FechaDeLanzamiento);
                Assert.Equal(juegosDto.ElementAt(i).Clasificacion, result.ElementAt(i).Clasificacion);
                Assert.Equal(juegosDto.ElementAt(i).Generos, result.ElementAt(i).Generos);
                Assert.Equal(juegosDto.ElementAt(i).Plataformas, result.ElementAt(i).Plataformas);
                Assert.Equal(juegosDto.ElementAt(i).Idiomas, result.ElementAt(i).Idiomas);
                Assert.Equal(juegosDto.ElementAt(i).Imagenes, result.ElementAt(i).Imagenes);
                Assert.Equal(juegosDto.ElementAt(i).Videos, result.ElementAt(i).Videos);
            }

            // Verifica que el repositorio y el mapper se llamaron correctamente
            _mockRepository.Verify(repo => repo.GetJuegosByPlataformaAsync(plataforma), Times.Once);
            _mapper.Verify(m => m.Map<IEnumerable<JuegoDTO>>(juegos), Times.Once);
        }

        [Fact]
        public async Task GetJuegosByIdiomaAsyncTests()
        {
            // Arrange
            string idioma = "Español";
            var juegos = CreateJuegoList();
            var juegosDto = CreateJuegoDTOList();

            _mockRepository.Setup(repo => repo.GetJuegosByIdiomaAsync(idioma)).ReturnsAsync(juegos);
            _mapper.Setup(m => m.Map<IEnumerable<JuegoDTO>>(juegos)).Returns(juegosDto);

            // Act
            var result = await _juegoService.GetJuegosByIdiomaAsync(idioma);

            // Assert
            Assert.NotNull(result); // Asegúrate de que el resultado no sea nulo
            Assert.IsAssignableFrom<IEnumerable<JuegoDTO>>(result); // Verifica que el resultado sea del tipo esperado
            Assert.Equal(juegosDto.Count(), result.Count()); // Asegúrate de que el conteo coincida

            // Verifica que los juegos retornados tienen las mismas propiedades que los DTOs simulados
            for (int i = 0; i < juegosDto.Count(); i++)
            {
                Assert.Equal(juegosDto.ElementAt(i).Id, result.ElementAt(i).Id);
                Assert.Equal(juegosDto.ElementAt(i).Titulo, result.ElementAt(i).Titulo);
                Assert.Equal(juegosDto.ElementAt(i).Precio, result.ElementAt(i).Precio);
                Assert.Equal(juegosDto.ElementAt(i).Descripcion, result.ElementAt(i).Descripcion);
                Assert.Equal(juegosDto.ElementAt(i).Desarrollador, result.ElementAt(i).Desarrollador);
                Assert.Equal(juegosDto.ElementAt(i).Editor, result.ElementAt(i).Editor);
                Assert.Equal(juegosDto.ElementAt(i).FechaDeLanzamiento, result.ElementAt(i).FechaDeLanzamiento);
                Assert.Equal(juegosDto.ElementAt(i).Clasificacion, result.ElementAt(i).Clasificacion);
                Assert.Equal(juegosDto.ElementAt(i).Generos, result.ElementAt(i).Generos);
                Assert.Equal(juegosDto.ElementAt(i).Plataformas, result.ElementAt(i).Plataformas);
                Assert.Equal(juegosDto.ElementAt(i).Idiomas, result.ElementAt(i).Idiomas);
                Assert.Equal(juegosDto.ElementAt(i).Imagenes, result.ElementAt(i).Imagenes);
                Assert.Equal(juegosDto.ElementAt(i).Videos, result.ElementAt(i).Videos);
            }
            // Verifica que el repositorio y el mapper se llamaron correctamente
            _mockRepository.Verify(repo => repo.GetJuegosByIdiomaAsync(idioma), Times.Once);
            _mapper.Verify(m => m.Map<IEnumerable<JuegoDTO>>(juegos), Times.Once);
        }

        [Fact]
        public async Task GetJuegosByClasificacionAsyncTests()
        {
            // Arrange
            string clasificacion = "E";
            var juegos = CreateJuegoList();
            var juegosDto = CreateJuegoDTOList();

            _mockRepository.Setup(repo => repo.GetJuegosByClasificacionAsync(clasificacion)).ReturnsAsync(juegos);
            _mapper.Setup(m => m.Map<IEnumerable<JuegoDTO>>(juegos)).Returns(juegosDto);

            // Act
            var result = await _juegoService.GetJuegosByClasificacionAsync(clasificacion);

            // Assert
            Assert.NotNull(result); // Asegúrate de que el resultado no sea nulo
            Assert.IsAssignableFrom<IEnumerable<JuegoDTO>>(result); // Verifica que el resultado sea del tipo esperado
            Assert.Equal(juegosDto.Count(), result.Count()); // Asegúrate de que el conteo coincida

            // Verifica que los juegos retornados tienen las mismas propiedades que los DTOs simulados
            for (int i = 0; i < juegosDto.Count(); i++)
            {
                Assert.Equal(juegosDto.ElementAt(i).Id, result.ElementAt(i).Id);
                Assert.Equal(juegosDto.ElementAt(i).Titulo, result.ElementAt(i).Titulo);
                Assert.Equal(juegosDto.ElementAt(i).Precio, result.ElementAt(i).Precio);
                Assert.Equal(juegosDto.ElementAt(i).Descripcion, result.ElementAt(i).Descripcion);
                Assert.Equal(juegosDto.ElementAt(i).Desarrollador, result.ElementAt(i).Desarrollador);
                Assert.Equal(juegosDto.ElementAt(i).Editor, result.ElementAt(i).Editor);
                Assert.Equal(juegosDto.ElementAt(i).FechaDeLanzamiento, result.ElementAt(i).FechaDeLanzamiento);
                Assert.Equal(juegosDto.ElementAt(i).Clasificacion, result.ElementAt(i).Clasificacion);
                Assert.Equal(juegosDto.ElementAt(i).Generos, result.ElementAt(i).Generos);
                Assert.Equal(juegosDto.ElementAt(i).Plataformas, result.ElementAt(i).Plataformas);
                Assert.Equal(juegosDto.ElementAt(i).Idiomas, result.ElementAt(i).Idiomas);
                Assert.Equal(juegosDto.ElementAt(i).Imagenes, result.ElementAt(i).Imagenes);
                Assert.Equal(juegosDto.ElementAt(i).Videos, result.ElementAt(i).Videos);
            }
            // Verifica que el repositorio y el mapper se llamaron
            _mockRepository.Verify(repo => repo.GetJuegosByClasificacionAsync(clasificacion), Times.Once);
            _mapper.Verify(m => m.Map<IEnumerable<JuegoDTO>>(juegos), Times.Once);
        }

        [Fact]
        public async Task GetJuegosFiltradosAsyncTests()
        {
            // Arrange
            string idioma = "Español";
            string plataforma = "PC";
            string genero = "Acción";
            string clasificacion = "Mature";
            int page = 1;
            int pageSize = 10;

            var juegos = CreateJuegoList(); // Lista simulada de juegos
            int totalJuegos = juegos.Count; // Total de juegos que coinciden con los filtros
            var juegosDto = CreateJuegoDTOList(); // Lista simulada de DTOs

            _mockRepository.Setup(repo => repo.GetJuegosFiltradosAsync(idioma, plataforma, genero, clasificacion, page, pageSize))
                .ReturnsAsync((juegos, totalJuegos));
            _mapper.Setup(m => m.Map<IEnumerable<JuegoDTO>>(juegos)).Returns(juegosDto);

            // Act
            var (resultJuegos, resultTotal) = await _juegoService.GetJuegosFiltradosAsync(idioma, plataforma, genero, clasificacion, page, pageSize);

            // Assert: Verifica los resultados
            Assert.NotNull(resultJuegos); // Los juegos no deben ser nulos
            Assert.Equal(juegosDto.Count(), resultJuegos.Count()); // El número de juegos en la página debe coincidir
            Assert.Equal(totalJuegos, resultTotal); // El total de juegos debe coincidir
            Assert.IsAssignableFrom<IEnumerable<JuegoDTO>>(resultJuegos); // Verifica el tipo de retorno

            // Verifica el mapeo correcto
            for (int i = 0; i < juegosDto.Count(); i++)
            {
                Assert.Equal(juegosDto.ElementAt(i).Id, resultJuegos.ElementAt(i).Id);
                Assert.Equal(juegosDto.ElementAt(i).Titulo, resultJuegos.ElementAt(i).Titulo);
                Assert.Equal(juegosDto.ElementAt(i).Precio, resultJuegos.ElementAt(i).Precio);
                Assert.Equal(juegosDto.ElementAt(i).Descripcion, resultJuegos.ElementAt(i).Descripcion);
                Assert.Equal(juegosDto.ElementAt(i).Desarrollador, resultJuegos.ElementAt(i).Desarrollador);
                Assert.Equal(juegosDto.ElementAt(i).Editor, resultJuegos.ElementAt(i).Editor);
                Assert.Equal(juegosDto.ElementAt(i).FechaDeLanzamiento, resultJuegos.ElementAt(i).FechaDeLanzamiento);
                Assert.Equal(juegosDto.ElementAt(i).Clasificacion, resultJuegos.ElementAt(i).Clasificacion);
                Assert.Equal(juegosDto.ElementAt(i).Generos, resultJuegos.ElementAt(i).Generos);
                Assert.Equal(juegosDto.ElementAt(i).Plataformas, resultJuegos.ElementAt(i).Plataformas);
                Assert.Equal(juegosDto.ElementAt(i).Idiomas, resultJuegos.ElementAt(i).Idiomas);
                Assert.Equal(juegosDto.ElementAt(i).Imagenes, resultJuegos.ElementAt(i).Imagenes);
                Assert.Equal(juegosDto.ElementAt(i).Videos, resultJuegos.ElementAt(i).Videos);
            }

            // Verifica que el repositorio y el mapper se llamaron correctamente
            _mockRepository.Verify(repo => repo.GetJuegosFiltradosAsync(idioma, plataforma, genero, clasificacion, page, pageSize), Times.Once);
            _mapper.Verify(m => m.Map<IEnumerable<JuegoDTO>>(juegos), Times.Once);
        }

        [Fact]
        public async Task GetJuegosPaginadosAsyncTests()
        {
            // Arrange
            int cursor = 0;
            int limit = 10;
            var juegos = CreateJuegoList(); 
            int totalJuegos = juegos.Count;
            var juegosDto = CreateJuegoDTOList();

            _mockRepository.Setup(repo => repo.GetJuegosPaginadosAsync(cursor, limit)).ReturnsAsync((juegos, totalJuegos));
            _mapper.Setup(m => m.Map<IEnumerable<JuegoDTO>>(juegos)).Returns(juegosDto);

            // Act
            var (resultJuegos, resultTotal) = await _juegoService.GetJuegosPaginadosAsync(cursor, limit);

            // Assert
            Assert.NotNull(resultJuegos); // Los juegos no deben ser nulos
            Assert.Equal(juegosDto.Count(), resultJuegos.Count()); // La cantidad de juegos en la página debe coincidir
            Assert.Equal(totalJuegos, resultTotal); // El total de juegos debe coincidir

            // Verifica el mapeo correcto
            for (int i = 0; i < juegosDto.Count(); i++)
            {
                Assert.Equal(juegosDto.ElementAt(i).Id, resultJuegos.ElementAt(i).Id);
                Assert.Equal(juegosDto.ElementAt(i).Titulo, resultJuegos.ElementAt(i).Titulo);
                Assert.Equal(juegosDto.ElementAt(i).Precio, resultJuegos.ElementAt(i).Precio);
                Assert.Equal(juegosDto.ElementAt(i).Descripcion, resultJuegos.ElementAt(i).Descripcion);
                Assert.Equal(juegosDto.ElementAt(i).Desarrollador, resultJuegos.ElementAt(i).Desarrollador);
                Assert.Equal(juegosDto.ElementAt(i).Editor, resultJuegos.ElementAt(i).Editor);
                Assert.Equal(juegosDto.ElementAt(i).FechaDeLanzamiento, resultJuegos.ElementAt(i).FechaDeLanzamiento);
                Assert.Equal(juegosDto.ElementAt(i).Clasificacion, resultJuegos.ElementAt(i).Clasificacion);
                Assert.Equal(juegosDto.ElementAt(i).Generos, resultJuegos.ElementAt(i).Generos);
                Assert.Equal(juegosDto.ElementAt(i).Plataformas, resultJuegos.ElementAt(i).Plataformas);
                Assert.Equal(juegosDto.ElementAt(i).Idiomas, resultJuegos.ElementAt(i).Idiomas);
                Assert.Equal(juegosDto.ElementAt(i).Imagenes, resultJuegos.ElementAt(i).Imagenes);
                Assert.Equal(juegosDto.ElementAt(i).Videos, resultJuegos.ElementAt(i).Videos);
            }

            // Verifica que el repositorio y el mapper se llamaron correctamente
            _mockRepository.Verify(repo => repo.GetJuegosPaginadosAsync(cursor, limit), Times.Once);
            _mapper.Verify(m => m.Map<IEnumerable<JuegoDTO>>(juegos), Times.Once);
        }

        // Métodos auxiliares para crear instancias simples de Juego y JuegoDTO

        // Método auxiliar para crear una instancia de Juego
        private Juego CreateSimpleJuego(int id = 1, string titulo = "Super Mario", string genero = "Plataformas")
        {
            return new Juego
            {
                Id = id,
                Titulo = titulo,
                Precio = 60,
                Descripcion = "Un juego clásico de plataformas",
                Desarrollador = "Nintendo",
                Editor = "Nintendo",
                ClasificacionId = 1,  // Asumiendo que 1 es la clave foránea para la clasificación 'E'
                Clasificacion = new Clasificacion { Id = 1, Nombre = "E" },
                FechaDeLanzamiento = DateTime.Now,
                Generos = new List<Genero> { new Genero { Id = 1, Nombre = genero } },  // Asignamos una entidad de tipo Genero
                Plataformas = new List<Plataforma> { new Plataforma { Id = 1, Nombre = "Nintendo" } },  // Asignamos una entidad de tipo Plataforma
                Idiomas = new List<Idioma> { new Idioma { Id = 1, Nombre = "Español" } },  // Asignamos una entidad de tipo Idioma
                Imagenes = new List<Imagen> { new Imagen { Id = 1, Url = "http://imagen.com/supermario.jpg" } },
                Videos = new List<Video> { new Video { Id = 1, Url = "http://video.com/supermario.mp4" } }
            };
        }

        // Método auxiliar para crear una instancia de JuegoDTO
        private JuegoDTO CreateSimpleJuegoDTO(int id = 1, string titulo = "Super Mario", string genero = "Plataformas")
        {
            return new JuegoDTO
            {
                Id = id,
                Titulo = titulo,
                Precio = 60,
                Descripcion = "Un juego clásico de plataformas",
                Desarrollador = "Nintendo",
                Editor = "Nintendo",
                Clasificacion = "E",
                FechaDeLanzamiento = DateTime.Now,
                Generos = new List<string> { genero },  // Utilizamos una colección de strings para los nombres de géneros
                Plataformas = new List<string> { "Nintendo" },  // Utilizamos una colección de strings para los nombres de plataformas
                Idiomas = new List<string> { "Español" },  // Utilizamos una colección de strings para los nombres de idiomas
                Imagenes = new List<string> { "http://imagen.com/supermario.jpg" },
                Videos = new List<string> { "http://video.com/supermario.mp4" }
            };
        }

        // Método auxiliar para crear una instancia de CreateJuegoDTO
        private CreateJuegoDTO CreateSimpleCreateJuego(string titulo = "Super Mario", string genero = "Plataformas")
        {
            return new CreateJuegoDTO
            {
                Titulo = titulo,
                Precio = 60,
                Portada = "http://imagen.com/portada.jpg",
                Descripcion = "Un juego clásico de plataformas",
                Desarrollador = "Nintendo",
                Editor = "Nintendo",
                Clasificacion = "E",
                FechaDeLanzamiento = DateTime.Now,
                Generos = new List<string> { genero },  // Utilizamos una colección de strings para los nombres de géneros
                Plataformas = new List<string> { "Nintendo" },  // Utilizamos una colección de strings para los nombres de plataformas
                Idiomas = new List<string> { "Español" },  // Utilizamos una colección de strings para los nombres de idiomas
                Imagenes = new List<string> { "http://imagen.com/supermario.jpg" },
                Videos = new List<string> { "http://video.com/supermario.mp4" }
            };
        }

        // Método auxiliar para crear una lista de instancias de Juego
        private List<Juego> CreateJuegoList(int cantidad = 5)
        {
            var juegos = new List<Juego>();
            var plataformasDisponibles = new List<Plataforma> { new Plataforma { Id = 1, Nombre = "Nintendo" }, new Plataforma { Id = 2, Nombre = "PlayStation" }, new Plataforma { Id = 3, Nombre = "PC" } };
            var generosDisponibles = new List<Genero> { new Genero { Id = 1, Nombre = "Acción" }, new Genero { Id = 2, Nombre = "Aventura" }, new Genero { Id = 3, Nombre = "Plataformas" } };
            var idiomasDisponibles = new List<Idioma> { new Idioma { Id = 1, Nombre = "Español" }, new Idioma { Id = 2, Nombre = "Inglés" }, new Idioma { Id = 3, Nombre = "Francés" } };
            var clasificacionesDisponibles = new List<Clasificacion> { new Clasificacion { Id = 1, Nombre = "E" }, new Clasificacion { Id = 2, Nombre = "T" }, new Clasificacion { Id = 3, Nombre = "M" } };

            for (int i = 1; i <= cantidad; i++)
            {
                juegos.Add(new Juego
                {
                    Id = i,
                    Titulo = $"Juego {i}",
                    Descripcion = $"Descripción del Juego {i}",
                    Precio = i * 10,
                    Desarrollador = $"Desarrollador {i}",
                    Editor = $"Editor {i}",
                    ClasificacionId = clasificacionesDisponibles[i % clasificacionesDisponibles.Count].Id,
                    Clasificacion = clasificacionesDisponibles[i % clasificacionesDisponibles.Count],
                    FechaDeLanzamiento = DateTime.Now.AddMonths(-i),
                    Generos = new List<Genero> { generosDisponibles[i % generosDisponibles.Count] },
                    Plataformas = new List<Plataforma> { plataformasDisponibles[i % plataformasDisponibles.Count] },
                    Idiomas = new List<Idioma> { idiomasDisponibles[i % idiomasDisponibles.Count] },
                    Imagenes = new List<Imagen> { new Imagen { Id = i, Url = $"https://imagen-juego-{i}.com" } },
                    Videos = new List<Video> { new Video { Id = i, Url = $"https://video-juego-{i}.com" } }
                });
            }

            return juegos;
        }

        // Método auxiliar para crear una lista de instancias de JuegoDTO
        private List<JuegoDTO> CreateJuegoDTOList(int cantidad = 5)
        {
            var juegosDTO = new List<JuegoDTO>();
            var plataformasDisponibles = new List<string> { "Nintendo", "PlayStation", "PC" };
            var generosDisponibles = new List<string> { "Acción", "Aventura", "Plataformas" };
            var idiomasDisponibles = new List<string> { "Español", "Inglés", "Francés" };
            var clasificacionesDisponibles = new List<string> { "E", "T", "M" };

            for (int i = 1; i <= cantidad; i++)
            {
                juegosDTO.Add(new JuegoDTO
                {
                    Id = i,
                    Titulo = $"Juego {i}",
                    Descripcion = $"Descripción del JuegoDTO {i}",
                    Precio = i * 10,
                    Desarrollador = $"DesarrolladorDTO {i}",
                    Editor = $"EditorDTO {i}",
                    Clasificacion = clasificacionesDisponibles[i % clasificacionesDisponibles.Count],
                    FechaDeLanzamiento = DateTime.Now.AddMonths(-i),
                    Generos = new List<string> { generosDisponibles[i % generosDisponibles.Count] },
                    Plataformas = new List<string> { plataformasDisponibles[i % plataformasDisponibles.Count] },
                    Idiomas = new List<string> { idiomasDisponibles[i % idiomasDisponibles.Count] },
                    Imagenes = new List<string> { $"https://imagen-juego-{i}.com" },
                    Videos = new List<string> { $"https://video-juego-{i}.com" }
                });
            }

            return juegosDTO;
        }
    }
}
