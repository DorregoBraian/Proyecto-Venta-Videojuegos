using Aplicacion.DTOs;
using Aplicacion.Servicio;
using AutoMapper;
using Dominio.Entidades;
using Dominio.IRepositorios;
using Moq;

namespace Testing.ServiceTests
{
    public class PlataformaServiceTests
    {
        private readonly PlataformaService _plataformaService;
        private readonly Mock<IPlataformaRepository> _mockPlataformaRepository;
        private readonly Mock<IMapper> _mockMapper;

        // Constructor para inicializar los mocks y la instancia del servicio
        public PlataformaServiceTests()
        {
            // Se crean los mocks del repositorio y del mapeador
            _mockPlataformaRepository = new Mock<IPlataformaRepository>();
            _mockMapper = new Mock<IMapper>();

            // Se inyectan los mocks en el servicio para que se puedan testear sin depender de las implementaciones reales
            _plataformaService = new PlataformaService(_mockPlataformaRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllPlataformasAsyncTests()
        {
            // Arrange
            var plataformas = CreateSimplePlataformaList();
            var plataformasDTO = CreateSimplePlataformaDTOList();

            _mockPlataformaRepository.Setup(repo => repo.ReadAllPlataformasAsync()).ReturnsAsync(plataformas);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<PlataformaDTO>>(plataformas)).Returns(plataformasDTO);

            // Act
            var result = await _plataformaService.GetAllPlataformasAsync();

            // Assert
            Assert.NotNull(result); // Comprueba que el resultado no es nulo
            Assert.Equal(plataformasDTO, result); // Verifica que los elementos coincidan
            Assert.Equal(plataformasDTO.Count, result.Count()); // Verifica que la cantidad de elementos coincida
            Assert.IsType<List<PlataformaDTO>>(result); // Verifica que el tipo de retorno sea el esperado

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockPlataformaRepository.Verify(repo => repo.ReadAllPlataformasAsync(), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<IEnumerable<PlataformaDTO>>(plataformas), Times.Once);
        }

        [Fact]
        public async Task GetPlataformaByIdAsyncTests()
        {
            // Arrange
            var plataforma = CreateSimplePlataforma();
            var plataformaDTO = CreateSimplePlataformaDTO();

            _mockPlataformaRepository.Setup(repo => repo.ReadPlataformaByIdAsync(plataforma.Id)).ReturnsAsync(plataforma);
            _mockMapper.Setup(mapper => mapper.Map<PlataformaDTO>(plataforma)).Returns(plataformaDTO);

            // Act
            var result = await _plataformaService.GetPlataformaByIdAsync(plataforma.Id);

            // Assert
            Assert.NotNull(result); // Comprueba que el resultado no es nulo
            Assert.Equal(plataformaDTO, result); // Verifica que los elementos coincidan
            Assert.Equal(plataformaDTO.Id, result.Id); // Verifica que los elementos coincidan
            Assert.Equal(plataformaDTO.Nombre, result.Nombre); // Verifica que los elementos coincidan
            Assert.IsType<PlataformaDTO>(result); // Verifica que el tipo de retorno sea el esperado

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockPlataformaRepository.Verify(repo => repo.ReadPlataformaByIdAsync(plataforma.Id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<PlataformaDTO>(plataforma), Times.Once);
        }

        [Fact]
        public async Task GetPlataformaByNombreTests()
        {
            // Arrange
            var plataforma = CreateSimplePlataforma();
            var plataformaDTO = CreateSimplePlataformaDTO();

            _mockPlataformaRepository.Setup(repo => repo.ReadPlataformaByNameAsync(plataforma.Nombre)).ReturnsAsync(plataforma);
            _mockMapper.Setup(mapper => mapper.Map<PlataformaDTO>(plataforma)).Returns(plataformaDTO);

            // Act
            var result = await _plataformaService.GetPlataformaByNombreAsync(plataforma.Nombre);

            // Assert
            Assert.NotNull(result); // Comprueba que el resultado no es nulo
            Assert.Equal(plataformaDTO, result); // Verifica que los elementos coincidan
            Assert.Equal(plataformaDTO.Id, result.Id); // Verifica que los elementos coincidan
            Assert.Equal(plataformaDTO.Nombre, result.Nombre); // Verifica que los elementos coincidan
            Assert.IsType<PlataformaDTO>(result); // Verifica que el tipo de retorno sea el esperado

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockPlataformaRepository.Verify(repo => repo.ReadPlataformaByNameAsync(plataforma.Nombre), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<PlataformaDTO>(plataforma), Times.Once);
        }


        [Fact]
        public async Task CreatePlataformaAsyncTests()
        {
            // Arrange
            var plataforma = CreateSimplePlataforma();
            var plataformaDTO = CreateSimplePlataformaDTO();

            _mockPlataformaRepository.Setup(repo => repo.CreatePlataformaAsync(plataforma)).Returns(Task.CompletedTask);
            _mockMapper.Setup(mapper => mapper.Map<Plataforma>(plataformaDTO)).Returns(plataforma);

            // Act
            var result = await _plataformaService.CreatePlataformaAsync(plataformaDTO);

            // Assert
            Assert.NotNull(result); // Comprueba que el resultado no es nulo
            Assert.Equal(plataformaDTO, result); // Verifica que los elementos coincidan
            Assert.Equal(plataformaDTO.Id, result.Id); // Verifica que los elementos coincidan
            Assert.Equal(plataformaDTO.Nombre, result.Nombre); // Verifica que los elementos coincidan
            Assert.IsType<PlataformaDTO>(result); // Verifica que el tipo de retorno sea el esperado

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockPlataformaRepository.Verify(repo => repo.CreatePlataformaAsync(plataforma), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<Plataforma>(plataformaDTO), Times.Once);
        }


        [Fact]
        public async Task UpdatePlataformaByIdAsyncTests()
        {
            // Arrange
            var plataforma = CreateSimplePlataforma();
            var plataformaDTO = CreateSimplePlataformaDTO();

            _mockPlataformaRepository.Setup(repo => repo.ReadPlataformaByIdAsync(plataformaDTO.Id)).ReturnsAsync(plataforma);
            _mockMapper.Setup(mapper => mapper.Map(plataformaDTO, plataforma));
            _mockPlataformaRepository.Setup(repo => repo.UpdatePlataformaAsync(plataforma)).Returns(Task.CompletedTask);

            // Act
            var result = await _plataformaService.UpdatePlataformaByIdAsync(plataformaDTO.Id,plataformaDTO);

            // Assert
            Assert.NotNull(result); // Comprueba que el resultado no es nulo
            Assert.Equal(plataformaDTO, result); // Verifica que los elementos coincidan
            Assert.Equal(plataformaDTO.Id, result.Id); // Verifica que los elementos coincidan
            Assert.Equal(plataformaDTO.Nombre, result.Nombre); // Verifica que los elementos coincidan
            Assert.IsType<PlataformaDTO>(result); // Verifica que el tipo de retorno sea el esperado

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockPlataformaRepository.Verify(repo => repo.ReadPlataformaByIdAsync(plataformaDTO.Id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map(plataformaDTO, plataforma), Times.Once);
            _mockPlataformaRepository.Verify(repo => repo.UpdatePlataformaAsync(plataforma), Times.Once);
        }

        [Fact]
        public async Task UpdatePlataformaByNombreAsyncTests()
        {
            // Arrange
            var plataforma = CreateSimplePlataforma();
            var plataformaDTO = CreateSimplePlataformaDTO();

            _mockPlataformaRepository.Setup(repo => repo.ReadPlataformaByNameAsync(plataformaDTO.Nombre)).ReturnsAsync(plataforma);
            _mockMapper.Setup(mapper => mapper.Map(plataformaDTO, plataforma));
            _mockPlataformaRepository.Setup(repo => repo.UpdatePlataformaAsync(plataforma)).Returns(Task.CompletedTask);

            // Act
            var result = await _plataformaService.UpdatePlataformaByNombreAsync(plataformaDTO.Nombre, plataformaDTO);

            // Assert
            Assert.NotNull(result); // Comprueba que el resultado no es nulo
            Assert.Equal(plataformaDTO, result); // Verifica que los elementos coincidan
            Assert.Equal(plataformaDTO.Id, result.Id); // Verifica que los elementos coincidan
            Assert.Equal(plataformaDTO.Nombre, result.Nombre); // Verifica que los elementos coincidan
            Assert.IsType<PlataformaDTO>(result); // Verifica que el tipo de retorno sea el esperado

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockPlataformaRepository.Verify(repo => repo.ReadPlataformaByNameAsync(plataformaDTO.Nombre), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map(plataformaDTO, plataforma), Times.Once);
            _mockPlataformaRepository.Verify(repo => repo.UpdatePlataformaAsync(plataforma), Times.Once);
        }

        [Fact]
        public async Task DeletePlataformaByIdAsyncTests()
        {
            // Arrange
            var plataforma = CreateSimplePlataforma();

            _mockPlataformaRepository.Setup(repo => repo.ReadPlataformaByIdAsync(plataforma.Id)).ReturnsAsync(plataforma);
            _mockPlataformaRepository.Setup(repo => repo.DeletePlataformaAsync(plataforma.Id)).Returns(Task.CompletedTask);
            _mockMapper.Setup(mapper => mapper.Map<PlataformaDTO>(plataforma)).Returns(CreateSimplePlataformaDTO());

            // Act
            var result = await _plataformaService.DeletePlataformaByIdAsync(plataforma.Id);

            // Assert
            Assert.NotNull(result); // Comprueba que el resultado no es nulo
            Assert.Equal(plataforma.Id, result.Id); // Verifica que los elementos coincidan
            Assert.Equal(plataforma.Nombre, result.Nombre); // Verifica que los elementos coincidan
            Assert.IsType<PlataformaDTO>(result); // Verifica que el tipo de retorno sea el esperado

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockPlataformaRepository.Verify(repo => repo.ReadPlataformaByIdAsync(plataforma.Id), Times.Once);
            _mockPlataformaRepository.Verify(repo => repo.DeletePlataformaAsync(plataforma.Id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<PlataformaDTO>(plataforma), Times.Once);
        }

        [Fact]
        public async Task DeletePlataformaByNombreAsyncTests()
        {
            // Arrange
            var plataforma = CreateSimplePlataforma();

            _mockPlataformaRepository.Setup(repo => repo.ReadPlataformaByNameAsync(plataforma.Nombre)).ReturnsAsync(plataforma);
            _mockPlataformaRepository.Setup(repo => repo.DeletePlataformaAsync(plataforma.Id)).Returns(Task.CompletedTask);
            _mockMapper.Setup(mapper => mapper.Map<PlataformaDTO>(plataforma)).Returns(CreateSimplePlataformaDTO());

            // Act
            var result = await _plataformaService.DeletePlataformaByNombreAsync(plataforma.Nombre);

            // Assert
            Assert.NotNull(result); // Comprueba que el resultado no es nulo
            Assert.Equal(plataforma.Id, result.Id); // Verifica que los elementos coincidan
            Assert.Equal(plataforma.Nombre, result.Nombre); // Verifica que los elementos coincidan
            Assert.IsType<PlataformaDTO>(result); // Verifica que el tipo de retorno sea el esperado

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockPlataformaRepository.Verify(repo => repo.ReadPlataformaByNameAsync(plataforma.Nombre), Times.Once);
            _mockPlataformaRepository.Verify(repo => repo.DeletePlataformaAsync(plataforma.Id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<PlataformaDTO>(plataforma), Times.Once);
        }


        // Métodos auxiliares para crear instancias simples de Plataforma y PlataformaDTO

        // Crea una entidad de Plataforma con datos por defecto o modificados
        private Plataforma CreateSimplePlataforma(int id = 1, string nombre = "PlayStation")
        {
            return new Plataforma
            {
                Id = id,
                Nombre = nombre
            };
        }

        // Crea un DTO de Plataforma con datos por defecto o modificados
        private PlataformaDTO CreateSimplePlataformaDTO(int id = 1, string nombre = "PlayStation")
        {
            return new PlataformaDTO
            {
                Id = id,
                Nombre = nombre
            };
        }

        // Crea una lista de entidades de Plataforma con datos por defecto
        private List<Plataforma> CreateSimplePlataformaList()
        {
            return new List<Plataforma>
        {
            CreateSimplePlataforma(1, "PlayStation"),
            CreateSimplePlataforma(2, "Xbox"),
            CreateSimplePlataforma(3, "Nintendo Switch")
        };
        }

        // Crea una lista de DTOs de Plataforma con datos por defecto
        private List<PlataformaDTO> CreateSimplePlataformaDTOList()
        {
            return new List<PlataformaDTO>
        {
            CreateSimplePlataformaDTO(1, "PlayStation"),
            CreateSimplePlataformaDTO(2, "Xbox"),
            CreateSimplePlataformaDTO(3, "Nintendo Switch")
        };
        }
    }
}
