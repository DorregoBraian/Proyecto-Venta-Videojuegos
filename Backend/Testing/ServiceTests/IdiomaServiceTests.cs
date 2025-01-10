using Aplicacion.DTOs;
using Aplicacion.IServicios;
using Aplicacion.Servicio;
using AutoMapper;
using Dominio.Entidades;
using Dominio.IRepositorios;
using Moq;

namespace Testing.ServiceTests
{
    public class IdiomaServiceTests
    {
        private readonly IdiomaService _idiomaService;
        private readonly Mock<IIdiomaRepository> _mockIdiomaRepository;
        private readonly Mock<IMapper> _mockMapper;

        // Constructor para inicializar los mocks y la instancia del servicio
        public IdiomaServiceTests()
        {
            // Se crean los mocks del repositorio y del mapeador
            _mockIdiomaRepository = new Mock<IIdiomaRepository>();
            _mockMapper = new Mock<IMapper>();

            // Se inyectan los mocks en el servicio para que se puedan testear sin depender de las implementaciones reales
            _idiomaService = new IdiomaService(_mockIdiomaRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllIdiomasAsyncTests()
        {
            // Arrange
            var idiomaList = CreateSimpleIdiomaList();
            var idiomaDtoList = CreateSimpleIdiomaDTOList();

            _mockIdiomaRepository.Setup(repo => repo.ReadAllIdiomasAsync()).ReturnsAsync(idiomaList);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<IdiomaDTO>>(idiomaList)).Returns(idiomaDtoList);

            // Act
            var result = await _idiomaService.GetAllIdiomasAsync();

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(idiomaDtoList, result); // Verifica que los elementos coincidan
            Assert.Equal(idiomaDtoList.Count, result.Count()); // Verifica que el conteo coincida
            Assert.IsType<List<IdiomaDTO>>(result); // Verifica que el tipo sea correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockIdiomaRepository.Verify(repo => repo.ReadAllIdiomasAsync(), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<IEnumerable<IdiomaDTO>>(idiomaList), Times.Once);
        }

        [Fact]
        public async Task GetIdiomaByIdAsyncTests()
        {
            // Arrange
            var idioma = CreateSimpleIdioma();
            var idiomaDto = CreateSimpleIdiomaDTO();

            _mockIdiomaRepository.Setup(repo => repo.ReadIdiomaByIdAsync(idioma.Id)).ReturnsAsync(idioma);
            _mockMapper.Setup(mapper => mapper.Map<IdiomaDTO>(idioma)).Returns(idiomaDto);

            // Act
            var result = await _idiomaService.GetIdiomaByIdAsync(idioma.Id);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(idiomaDto.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(idiomaDto.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.IsType<IdiomaDTO>(result); // Verifica que el tipo sea correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockIdiomaRepository.Verify(repo => repo.ReadIdiomaByIdAsync(idioma.Id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<IdiomaDTO>(idioma), Times.Once);
        }

        [Fact]
        public async Task GetIdiomaByNombreAsyncTests()
        {
            // Arrange
            var idioma = CreateSimpleIdioma();
            var idiomaDto = CreateSimpleIdiomaDTO();

            _mockIdiomaRepository.Setup(repo => repo.ReadIdiomaByNameAsync(idioma.Nombre)).ReturnsAsync(idioma);
            _mockMapper.Setup(mapper => mapper.Map<IdiomaDTO>(idioma)).Returns(idiomaDto);

            // Act
            var result = await _idiomaService.GetIdiomaByNombreAsync(idioma.Nombre);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(idiomaDto.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(idiomaDto.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.IsType<IdiomaDTO>(result); // Verifica que el tipo sea correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockIdiomaRepository.Verify(repo => repo.ReadIdiomaByNameAsync(idioma.Nombre), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<IdiomaDTO>(idioma), Times.Once);
        }

        [Fact]
        public async Task CreateIdiomaAsyncTests()
        {
            // Arrange
            var idiomaDTO = CreateSimpleIdiomaDTO();
            var idioma = CreateSimpleIdioma();

            _mockMapper.Setup(mapper => mapper.Map<Idioma>(idiomaDTO)).Returns(idioma);
            _mockIdiomaRepository.Setup(repo => repo.CreateIdiomaAsync(idioma)).Returns(Task.CompletedTask);

            // Act
            var result = await _idiomaService.CreateIdiomaAsync(idiomaDTO);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(idiomaDTO.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(idiomaDTO.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.IsType<IdiomaDTO>(result); // Verifica que el tipo sea correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockMapper.Verify(mapper => mapper.Map<Idioma>(idiomaDTO), Times.Once);
            _mockIdiomaRepository.Verify(repo => repo.CreateIdiomaAsync(idioma), Times.Once);
        }

        [Fact]
        public async Task UpdateIdiomaByIdAsyncTests()
        {
            // Arrange
            var idiomaDTO = CreateSimpleIdiomaDTO();
            var idioma = CreateSimpleIdioma();

            _mockIdiomaRepository.Setup(repo => repo.ReadIdiomaByIdAsync(idiomaDTO.Id)).ReturnsAsync(idioma);
            _mockMapper.Setup(mapper => mapper.Map(idiomaDTO, idioma));
            _mockIdiomaRepository.Setup(repo => repo.UpdateIdiomaAsync(idioma)).Returns(Task.CompletedTask);

            // Act
            var result = await _idiomaService.UpdateIdiomaByIdAsync(idiomaDTO.Id,idiomaDTO);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(idiomaDTO.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(idiomaDTO.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.IsType<IdiomaDTO>(result); // Verifica que el tipo sea correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockIdiomaRepository.Verify(repo => repo.ReadIdiomaByIdAsync(idiomaDTO.Id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map(idiomaDTO, idioma), Times.Once);
            _mockIdiomaRepository.Verify(repo => repo.UpdateIdiomaAsync(idioma), Times.Once);
        }

        [Fact]
        public async Task UpdateIdiomaByNombreAsyncTests()
        {
            // Arrange
            var idiomaDTO = CreateSimpleIdiomaDTO();
            var idioma = CreateSimpleIdioma();

            _mockIdiomaRepository.Setup(repo => repo.ReadIdiomaByNameAsync(idiomaDTO.Nombre)).ReturnsAsync(idioma);
            _mockMapper.Setup(mapper => mapper.Map(idiomaDTO, idioma));
            _mockIdiomaRepository.Setup(repo => repo.UpdateIdiomaAsync(idioma)).Returns(Task.CompletedTask);

            // Act
            var result = await _idiomaService.UpdateIdiomaByNombreAsync(idiomaDTO.Nombre, idiomaDTO);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(idiomaDTO.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(idiomaDTO.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.IsType<IdiomaDTO>(result); // Verifica que el tipo sea correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockIdiomaRepository.Verify(repo => repo.ReadIdiomaByNameAsync(idiomaDTO.Nombre), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map(idiomaDTO, idioma), Times.Once);
            _mockIdiomaRepository.Verify(repo => repo.UpdateIdiomaAsync(idioma), Times.Once);
        }

        [Fact]
        public async Task DeleteIdiomaByIdAsyncTests()
        {
            // Arrange
            var idioma = CreateSimpleIdioma();
            var idiomaDTO = CreateSimpleIdiomaDTO();

            _mockIdiomaRepository.Setup(repo => repo.ReadIdiomaByIdAsync(idioma.Id)).ReturnsAsync(idioma);
            _mockIdiomaRepository.Setup(repo => repo.DeleteIdiomaAsync(idioma.Id)).Returns(Task.CompletedTask);
            _mockMapper.Setup(mapper => mapper.Map<IdiomaDTO>(idioma)).Returns(idiomaDTO);

            // Act
            var result = await _idiomaService.DeleteIdiomaByIdAsync(idioma.Id);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(idiomaDTO.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(idiomaDTO.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.IsType<IdiomaDTO>(result); // Verifica que el tipo sea correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockIdiomaRepository.Verify(repo => repo.ReadIdiomaByIdAsync(idioma.Id), Times.Once);
            _mockIdiomaRepository.Verify(repo => repo.DeleteIdiomaAsync(idioma.Id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<IdiomaDTO>(idioma), Times.Once);
        }

        [Fact]
        public async Task DeleteIdiomaByNombreAsyncTests()
        {
            // Arrange
            var idioma = CreateSimpleIdioma();
            var idiomaDTO = CreateSimpleIdiomaDTO();

            _mockIdiomaRepository.Setup(repo => repo.ReadIdiomaByNameAsync(idioma.Nombre)).ReturnsAsync(idioma);
            _mockIdiomaRepository.Setup(repo => repo.DeleteIdiomaAsync(idioma.Id)).Returns(Task.CompletedTask);
            _mockMapper.Setup(mapper => mapper.Map<IdiomaDTO>(idioma)).Returns(idiomaDTO);

            // Act
            var result = await _idiomaService.DeleteIdiomaByNombreAsync(idioma.Nombre);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(idiomaDTO.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(idiomaDTO.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.IsType<IdiomaDTO>(result); // Verifica que el tipo sea correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockIdiomaRepository.Verify(repo => repo.ReadIdiomaByNameAsync(idioma.Nombre), Times.Once);
            _mockIdiomaRepository.Verify(repo => repo.DeleteIdiomaAsync(idioma.Id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<IdiomaDTO>(idioma), Times.Once);
        }

        // Métodos auxiliares para crear instancias simples de Idioma y IdiomaDTO

        // Crea una entidad de Idioma con datos por defecto o modificados
        private Idioma CreateSimpleIdioma(int id = 1, string nombre = "Español")
        {
            return new Idioma
            {
                Id = id,
                Nombre = nombre
            };
        }

        // Crea un DTO de Idioma con datos por defecto o modificados
        private IdiomaDTO CreateSimpleIdiomaDTO(int id = 1, string nombre = "Español")
        {
            return new IdiomaDTO
            {
                Id = id,
                Nombre = nombre
            };
        }

        // Crea una lista de idiomas de prueba
        private List<Idioma> CreateSimpleIdiomaList()
        {
            return new List<Idioma>
            {
                CreateSimpleIdioma(id: 1, nombre: "Español"),
                CreateSimpleIdioma(id: 2, nombre: "Inglés"),
                CreateSimpleIdioma(id: 3, nombre: "Francés")
            };
        }

        // Crea una lista de DTOs de idiomas de prueba
        private List<IdiomaDTO> CreateSimpleIdiomaDTOList()
        {
            return new List<IdiomaDTO>
            {
                CreateSimpleIdiomaDTO(id: 1, nombre: "Español"),
                CreateSimpleIdiomaDTO(id: 2, nombre: "Inglés"),
                CreateSimpleIdiomaDTO(id: 3, nombre: "Francés")
            };
        }
    }
}
