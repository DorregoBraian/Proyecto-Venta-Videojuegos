using Aplicacion.DTOs;
using Aplicacion.Servicio;
using AutoMapper;
using Dominio.Entidades;
using Dominio.IRepositorios;
using Moq;

namespace Testing.ServiceTests
{
    public class GeneroServiceTests
    {
        private readonly GeneroService _generoService;
        private readonly Mock<IGeneroRepository> _mockGeneroRepository;
        private readonly Mock<IMapper> _mockMapper;

        // Constructor para inicializar los mocks y la instancia del servicio
        public GeneroServiceTests()
        {
            // Se crean los mocks del repositorio y del mapeador
            _mockGeneroRepository = new Mock<IGeneroRepository>();
            _mockMapper = new Mock<IMapper>();

            // Se inyectan los mocks en el servicio para que se puedan testear sin depender de las implementaciones reales
            _generoService = new GeneroService(_mockGeneroRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllGenerosAsyncTests()
        {
            // Arrange
            var generoList = CreateSimpleGeneroList();
            var generoDtoList = CreateSimpleGeneroDTOList();

            _mockGeneroRepository.Setup(repo => repo.ReadAllGenerosAsync()).ReturnsAsync(generoList);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<GeneroDTO>>(generoList)).Returns(generoDtoList);

            // Act
            var result = await _generoService.GetAllGenerosAsync();

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(generoDtoList, result); // Verifica que los elementos coincidan
            Assert.Equal(generoDtoList.Count, result.Count()); // Verifica que el conteo coincida
            Assert.IsType<List<GeneroDTO>>(result); // Verifica que el tipo sea correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockGeneroRepository.Verify(repo => repo.ReadAllGenerosAsync(), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<IEnumerable<GeneroDTO>>(generoList), Times.Once);
        }

        [Fact]
        public async Task GetGeneroByIdAsyncTests()
        {
            // Arrange
            var genero = CreateSimpleGenero();
            var generoDto = CreateSimpleGeneroDTO();

            _mockGeneroRepository.Setup(repo => repo.ReadGeneroByIdAsync(genero.Id)).ReturnsAsync(genero);
            _mockMapper.Setup(mapper => mapper.Map<GeneroDTO>(genero)).Returns(generoDto);

            // Act
            var result = await _generoService.GetGeneroByIdAsync(genero.Id);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(generoDto.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(generoDto.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.IsType<GeneroDTO>(result); // Verifica que el tipo sea el correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockGeneroRepository.Verify(repo => repo.ReadGeneroByIdAsync(genero.Id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<GeneroDTO>(genero), Times.Once);
        }

        [Fact]
        public async Task GetGeneroByNombreAsyncTests()
        {
            // Arrange
            var genero = CreateSimpleGenero();
            var generoDto = CreateSimpleGeneroDTO();

            _mockGeneroRepository.Setup(repo => repo.ReadGeneroByNameAsync(genero.Nombre)).ReturnsAsync(genero);
            _mockMapper.Setup(mapper => mapper.Map<GeneroDTO>(genero)).Returns(generoDto);

            // Act
            var result = await _generoService.GetGeneroByNombreAsync(genero.Nombre);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(generoDto.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.IsType<GeneroDTO>(result); // Verifica que el tipo sea el correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockGeneroRepository.Verify(repo => repo.ReadGeneroByNameAsync(genero.Nombre), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<GeneroDTO>(genero), Times.Once);
        }

        [Fact]
        public async Task CreateGeneroAsyncTests()
        {
            // Arrange
            var generoDTO = CreateSimpleGeneroDTO();
            var genero = CreateSimpleGenero();

            _mockMapper.Setup(mapper => mapper.Map<Genero>(generoDTO)).Returns(genero);
            _mockGeneroRepository.Setup(repo => repo.CreateGeneroAsync(genero)).Returns(Task.CompletedTask);

            // Act
            var result = await _generoService.CreateGeneroAsync(generoDTO);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(generoDTO.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(generoDTO.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.IsType<GeneroDTO>(result); // Verifica que el tipo sea el correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockMapper.Verify(mapper => mapper.Map<Genero>(generoDTO), Times.Once);
            _mockGeneroRepository.Verify(repo => repo.CreateGeneroAsync(genero), Times.Once);
        }

        [Fact]
        public async Task UpdateGeneroByIdAsyncTests()
        {
            // Arrange
            var generoDTO = CreateSimpleGeneroDTO();
            var genero = CreateSimpleGenero();

            _mockGeneroRepository.Setup(repo => repo.ReadGeneroByIdAsync(generoDTO.Id)).ReturnsAsync(genero);
            _mockMapper.Setup(mapper => mapper.Map(generoDTO, genero));
            _mockGeneroRepository.Setup(repo => repo.UpdateGeneroAsync(genero)).Returns(Task.CompletedTask);

            // Act
            var result = await _generoService.UpdateGeneroByIdAsync(generoDTO.Id, generoDTO);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(generoDTO.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(generoDTO.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.IsType<GeneroDTO>(result); // Verifica que el tipo sea el correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockGeneroRepository.Verify(repo => repo.ReadGeneroByIdAsync(generoDTO.Id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map(generoDTO, genero), Times.Once);
            _mockGeneroRepository.Verify(repo => repo.UpdateGeneroAsync(genero), Times.Once);
        }

        [Fact]
        public async Task UpdateGeneroByNombreAsyncTests()
        {
            // Arrange
            var generoDTO = CreateSimpleGeneroDTO();
            var genero = CreateSimpleGenero();

            _mockGeneroRepository.Setup(repo => repo.ReadGeneroByNameAsync(generoDTO.Nombre)).ReturnsAsync(genero);
            _mockMapper.Setup(mapper => mapper.Map(generoDTO, genero));
            _mockGeneroRepository.Setup(repo => repo.UpdateGeneroAsync(genero)).Returns(Task.CompletedTask);

            // Act
            var result = await _generoService.UpdateGeneroByNombreAsync(generoDTO.Nombre, generoDTO);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(generoDTO.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(generoDTO.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.IsType<GeneroDTO>(result); // Verifica que el tipo sea el correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockGeneroRepository.Verify(repo => repo.ReadGeneroByNameAsync(generoDTO.Nombre), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map(generoDTO, genero), Times.Once);
            _mockGeneroRepository.Verify(repo => repo.UpdateGeneroAsync(genero), Times.Once);
        }

        [Fact]
        public async Task DeleteGeneroByIdAsyncTests()
        {
            // Arrange
            var genero = CreateSimpleGenero();
            var generoDTO = CreateSimpleGeneroDTO();

            _mockGeneroRepository.Setup(repo => repo.ReadGeneroByIdAsync(genero.Id)).ReturnsAsync(genero);
            _mockGeneroRepository.Setup(repo => repo.DeleteGeneroAsync(genero.Id)).Returns(Task.CompletedTask);
            _mockMapper.Setup(mapper => mapper.Map<GeneroDTO>(genero)).Returns(generoDTO);

            // Act
            var result = await _generoService.DeleteGeneroByIdAsync(genero.Id);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(generoDTO.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(generoDTO.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.IsType<GeneroDTO>(result); // Verifica que el tipo sea el correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockGeneroRepository.Verify(repo => repo.ReadGeneroByIdAsync(genero.Id), Times.Once);
            _mockGeneroRepository.Verify(repo => repo.DeleteGeneroAsync(genero.Id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<GeneroDTO>(genero), Times.Once);
        }

        [Fact]
        public async Task DeleteGeneroByNombreAsyncTests()
        {
            // Arrange
            var genero = CreateSimpleGenero();
            var generoDTO = CreateSimpleGeneroDTO();

            _mockGeneroRepository.Setup(repo => repo.ReadGeneroByNameAsync(genero.Nombre)).ReturnsAsync(genero);
            _mockGeneroRepository.Setup(repo => repo.DeleteGeneroAsync(genero.Id)).Returns(Task.CompletedTask);
            _mockMapper.Setup(mapper => mapper.Map<GeneroDTO>(genero)).Returns(generoDTO);

            // Act
            var result = await _generoService.DeleteGeneroByNombreAsync(genero.Nombre);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(generoDTO.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(generoDTO.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.IsType<GeneroDTO>(result); // Verifica que el tipo sea el correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockGeneroRepository.Verify(repo => repo.ReadGeneroByNameAsync(genero.Nombre), Times.Once);
            _mockGeneroRepository.Verify(repo => repo.DeleteGeneroAsync(genero.Id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<GeneroDTO>(genero), Times.Once);
        }

        // Métodos auxiliares para crear instancias simples de Genero y GeneroDTO

        // Crea una entidad de Genero con datos por defecto o modificados
        private Genero CreateSimpleGenero(int id = 1, string nombre = "Acción")
        {
            return new Genero
            {
                Id = id,
                Nombre = nombre
            };
        }

        // Crea un DTO de Genero con datos por defecto o modificados
        private GeneroDTO CreateSimpleGeneroDTO(int id = 1, string nombre = "Acción")
        {
            return new GeneroDTO
            {
                Id = id,
                Nombre = nombre
            };
        }

        // Crea una lista de géneros de prueba
        private List<Genero> CreateSimpleGeneroList()
        {
            return new List<Genero>
            {
                CreateSimpleGenero(id : 1, nombre : "Accion"),
                CreateSimpleGenero(id: 2, nombre: "Aventura"),
                CreateSimpleGenero(id: 3, nombre: "Terror"),
                CreateSimpleGenero(id: 4, nombre: "Survival Horror")
            };
        }

        // Crea una lista de DTOs de géneros de prueba
        private List<GeneroDTO> CreateSimpleGeneroDTOList()
        {
            return new List<GeneroDTO>
            {
                CreateSimpleGeneroDTO(id : 1, nombre : "Accion"),
                CreateSimpleGeneroDTO(id: 2, nombre: "Aventura"),
                CreateSimpleGeneroDTO(id: 3, nombre: "Terror"),
                CreateSimpleGeneroDTO(id: 4, nombre: "Survival Horror")

            };
        }
    }
}
