
using Aplicacion.DTOs;
using Aplicacion.Servicio;
using AutoMapper;
using Dominio.Entidades;
using Dominio.IRepositorios;
using Moq;

namespace Testing.ServiceTests
{
    public class ClasificacionServiceTests
    {
        private readonly ClasificacionService _clasificacionService; // Servicio que se está probando
        private readonly Mock<IClasificacionRepository> _mockClasificacionRepository; // Mock del repositorio
        private readonly Mock<IMapper> _mockMapper; // Mock del mapeador

        // Constructor para inicializar los mocks y la instancia del servicio
        public ClasificacionServiceTests()
        {
            // Se crean los mocks del repositorio y del mapeador
            _mockClasificacionRepository = new Mock<IClasificacionRepository>();
            _mockMapper = new Mock<IMapper>();

            // Se inyectan los mocks en el servicio para que se puedan testear sin depender de las implementaciones reales
            _clasificacionService = new ClasificacionService(_mockClasificacionRepository.Object, _mockMapper.Object);
        }

        // Prueba del método GetAllClasificacionesAsync
        [Fact]
        public async Task GetAllClasificacionesAsyncTests()
        {
            // Arrange
            var clasificaciones = CreateSimpleClasificacionList();
            var clasificacionesDto = CreateSimpleClasificacionDTOList();

            _mockClasificacionRepository.Setup(repo => repo.ReadAllClasificacionesAsync()).ReturnsAsync(clasificaciones);
            _mockMapper.Setup(mapper => mapper.Map<IEnumerable<ClasificacionDTO>>(clasificaciones)).Returns(clasificacionesDto);

            // Act
            var result = await _clasificacionService.GetAllClasificacionesAsync();

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(clasificacionesDto, result); // Verifica que los elementos coincidan
            Assert.Equal(clasificacionesDto.Count, result.Count()); // Verifica que el conteo sea igual
            Assert.IsType<List<ClasificacionDTO>>(result); // Verifica que el tipo sea el correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockClasificacionRepository.Verify(repo => repo.ReadAllClasificacionesAsync(), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<IEnumerable<ClasificacionDTO>>(clasificaciones), Times.Once);

        }

        // Prueba del método GetClasificacionByIdAsync
        [Fact]
        public async Task GetClasificacionByIdAsyncTests()
        {
            // Arrange
            var clasificacion = CreateSimpleClasificacion();
            var clasificacionDto = CreateSimpleClasificacionDTO();

            _mockClasificacionRepository.Setup(repo => repo.ReadClasificacionByIdAsync(clasificacion.Id)).ReturnsAsync(clasificacion);
            _mockMapper.Setup(mapper => mapper.Map<ClasificacionDTO>(clasificacion)).Returns(clasificacionDto);

            // Act
            var result = await _clasificacionService.GetClasificacionByIdAsync(clasificacion.Id);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(clasificacionDto.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(clasificacionDto.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.Equal(clasificacionDto.Descripcion, result.Descripcion); // Verifica que la descripción coincida
            Assert.IsType<ClasificacionDTO>(result); // Verifica que el tipo sea el correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockClasificacionRepository.Verify(repo => repo.ReadClasificacionByIdAsync(clasificacion.Id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<ClasificacionDTO>(clasificacion), Times.Once);
        }

        // Prueba del método GetClasificacionByNombreAsync
        [Fact]
        public async Task GetClasificacionByNombreAsyncTests()
        {
            // Arrange
            var clasificacion = CreateSimpleClasificacion();
            var clasificacionDto = CreateSimpleClasificacionDTO();

            _mockClasificacionRepository.Setup(repo => repo.ReadClasificacionByNameAsync(clasificacion.Nombre)).ReturnsAsync(clasificacion);
            _mockMapper.Setup(mapper => mapper.Map<ClasificacionDTO>(clasificacion)).Returns(clasificacionDto);

            // Act
            var result = await _clasificacionService.GetClasificacionByNombreAsync(clasificacion.Nombre);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(clasificacionDto.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(clasificacionDto.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.Equal(clasificacionDto.Descripcion, result.Descripcion); // Verifica que la descripción coincida
            Assert.IsType<ClasificacionDTO>(result); // Verifica que el tipo sea el correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockClasificacionRepository.Verify(repo => repo.ReadClasificacionByNameAsync(clasificacion.Nombre), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<ClasificacionDTO>(clasificacion), Times.Once);
        }


        [Fact]
        public async Task CreateClasificacionAsyncTests()
        {
            // Arrange
            var clasificacionDto = CreateSimpleClasificacionDTO();
            var clasificacion = CreateSimpleClasificacion();

            _mockClasificacionRepository.Setup(repo => repo.CreateClasificacionAsync(clasificacion)).Returns(Task.CompletedTask);
            _mockMapper.Setup(mapper => mapper.Map<Clasificacion>(clasificacionDto)).Returns(clasificacion);

            // Act
            var result = await _clasificacionService.CreateClasificacionAsync(clasificacionDto);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(clasificacionDto.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(clasificacionDto.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.Equal(clasificacionDto.Descripcion, result.Descripcion); // Verifica que la descripción coincida
            Assert.IsType<ClasificacionDTO>(result); // Verifica que el tipo sea el correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockClasificacionRepository.Verify(repo => repo.CreateClasificacionAsync(clasificacion), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<Clasificacion>(clasificacionDto), Times.Once);
        }

        [Fact]
        public async Task UpdateClasificacionByIdAsyncTests()
        {
            // Arrange
            var clasificacionDto = CreateSimpleClasificacionDTO();
            var clasificacionExistente = CreateSimpleClasificacion();

            _mockClasificacionRepository.Setup(repo => repo.ReadClasificacionByIdAsync(clasificacionDto.Id)).ReturnsAsync(clasificacionExistente);
            _mockMapper.Setup(mapper => mapper.Map(clasificacionDto, clasificacionExistente));
            _mockClasificacionRepository.Setup(repo => repo.UpdateClasificacionAsync(clasificacionExistente)).Returns(Task.CompletedTask);

            // Act
            var result = await _clasificacionService.UpdateClasificacionByIdAsync(clasificacionDto.Id, clasificacionDto);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(clasificacionDto.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(clasificacionDto.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.Equal(clasificacionDto.Descripcion, result.Descripcion); // Verifica que la descripción coincida
            Assert.IsType<ClasificacionDTO>(result); // Verifica que el tipo sea el correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockClasificacionRepository.Verify(repo => repo.ReadClasificacionByIdAsync(clasificacionDto.Id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map(clasificacionDto, clasificacionExistente), Times.Once);
            _mockClasificacionRepository.Verify(repo => repo.UpdateClasificacionAsync(clasificacionExistente), Times.Once);
        }

        [Fact]
        public async Task UpdateClasificacionByNombreAsyncTests()
        {
            // Arrange
            var clasificacionDto = CreateSimpleClasificacionDTO();
            var clasificacionExistente = CreateSimpleClasificacion();

            _mockClasificacionRepository.Setup(r => r.ReadClasificacionByNameAsync(clasificacionDto.Nombre)).ReturnsAsync(clasificacionExistente);
            _mockClasificacionRepository.Setup(r => r.UpdateClasificacionAsync(clasificacionExistente)).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map(clasificacionDto, clasificacionExistente));

            // Act
            var result = await _clasificacionService.UpdateClasificacionByNombreAsync(clasificacionDto.Nombre, clasificacionDto);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(clasificacionDto.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(clasificacionDto.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.Equal(clasificacionDto.Descripcion, result.Descripcion); // Verifica que la descripción coincida
            Assert.IsType<ClasificacionDTO>(result); // Verifica que el tipo sea el correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockClasificacionRepository.Verify(r => r.ReadClasificacionByNameAsync(clasificacionDto.Nombre), Times.Once);
            _mockClasificacionRepository.Verify(r => r.UpdateClasificacionAsync(clasificacionExistente), Times.Once);
            _mockMapper.Verify(m => m.Map(clasificacionDto, clasificacionExistente), Times.Once);
        }

        [Fact]
        public async Task DeleteClasificacionByIdAsyncTests()
        {
            // Arrange
            var clasificacion = CreateSimpleClasificacion();

            _mockClasificacionRepository.Setup(repo => repo.ReadClasificacionByIdAsync(clasificacion.Id)).ReturnsAsync(clasificacion);
            _mockClasificacionRepository.Setup(repo => repo.DeleteClasificacionAsync(clasificacion.Id)).Returns(Task.CompletedTask);
            _mockMapper.Setup(mapper => mapper.Map<ClasificacionDTO>(clasificacion)).Returns(CreateSimpleClasificacionDTO());

            // Act
            var result = await _clasificacionService.DeleteClasificacionByIdAsync(clasificacion.Id);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(clasificacion.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(clasificacion.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.Equal(clasificacion.Descripcion, result.Descripcion); // Verifica que la descripción coincida
            Assert.IsType<ClasificacionDTO>(result); // Verifica que el tipo sea el correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockClasificacionRepository.Verify(repo => repo.ReadClasificacionByIdAsync(clasificacion.Id), Times.Once);
            _mockClasificacionRepository.Verify(repo => repo.DeleteClasificacionAsync(clasificacion.Id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<ClasificacionDTO>(clasificacion), Times.Once);
        }

        [Fact]
        public async Task DeleteClasificacionByNombreAsyncTests()
        {
            // Arrange
            var clasificacion = CreateSimpleClasificacion();

            _mockClasificacionRepository.Setup(repo => repo.ReadClasificacionByNameAsync(clasificacion.Nombre)).ReturnsAsync(clasificacion);
            _mockClasificacionRepository.Setup(repo => repo.DeleteClasificacionAsync(clasificacion.Id)).Returns(Task.CompletedTask);
            _mockMapper.Setup(mapper => mapper.Map<ClasificacionDTO>(clasificacion)).Returns(CreateSimpleClasificacionDTO());

            // Act
            var result = await _clasificacionService.DeleteClasificacionByNombreAsync(clasificacion.Nombre);

            // Assert
            Assert.NotNull(result); // Verifica que el resultado no sea nulo
            Assert.Equal(clasificacion.Id, result.Id); // Verifica que el ID coincida
            Assert.Equal(clasificacion.Nombre, result.Nombre); // Verifica que el nombre coincida
            Assert.Equal(clasificacion.Descripcion, result.Descripcion); // Verifica que la descripción coincida
            Assert.IsType<ClasificacionDTO>(result); // Verifica que el tipo sea el correcto

            // Verifica que los métodos del repositorio y mapeador se llamaron correctamente
            _mockClasificacionRepository.Verify(repo => repo.ReadClasificacionByNameAsync(clasificacion.Nombre), Times.Once);
            _mockClasificacionRepository.Verify(repo => repo.DeleteClasificacionAsync(clasificacion.Id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<ClasificacionDTO>(clasificacion), Times.Once);
        }

        // Métodos auxiliares para crear instancias simples de Genero y GeneroDTO

        // Método de ayuda para crear una Clasificación de ejemplo
        private Clasificacion CreateSimpleClasificacion(int id = 1, string nombre = "Acción", string descripcion = "Adecuado para todas las edades")
        {
            return new Clasificacion
            {
                Id = id,
                Nombre = nombre,
                Descripcion = descripcion,
            };
        }

        // Método de ayuda para crear un ClasificacionDTO de ejemplo
        private ClasificacionDTO CreateSimpleClasificacionDTO(int id = 1, string nombre = "Acción", string descripcion = "Adecuado para todas las edades")
        {
            return new ClasificacionDTO
            {
                Id = id,
                Nombre = nombre,
                Descripcion = descripcion,
            };
        }

        // Crea una lista de géneros de prueba
        private List<Clasificacion> CreateSimpleClasificacionList()
        {
            return new List<Clasificacion>
            {
                CreateSimpleClasificacion(id : 1, nombre : "PEGI 3", descripcion : "Adecuado para todas las edades"),
                CreateSimpleClasificacion(id : 2, nombre : "PEGI 7", descripcion : "Adecuado para mayores de 7 años"),
                CreateSimpleClasificacion(id : 3, nombre : "PEGI 12", descripcion : "Adecuado para mayores de 12 años"),
                CreateSimpleClasificacion(id : 4, nombre : "PEGI 16", descripcion : "Adecuado para mayores de 16 años" ),
                CreateSimpleClasificacion(id : 5, nombre : "PEGI 18", descripcion : "Adecuado para mayores de 18 años" )
            };
        }

        // Crea una lista de DTOs de géneros de prueba
        private List<ClasificacionDTO> CreateSimpleClasificacionDTOList()
        {
            return new List<ClasificacionDTO>
            {
                CreateSimpleClasificacionDTO(id : 1, nombre : "PEGI 3", descripcion : "Adecuado para todas las edades"),
                CreateSimpleClasificacionDTO(id : 2, nombre : "PEGI 7", descripcion : "Adecuado para mayores de 7 años"),
                CreateSimpleClasificacionDTO(id : 3, nombre : "PEGI 12", descripcion : "Adecuado para mayores de 12 años"),
                CreateSimpleClasificacionDTO(id : 4, nombre : "PEGI 16", descripcion : "Adecuado para mayores de 16 años" ),
                CreateSimpleClasificacionDTO(id : 5, nombre : "PEGI 18", descripcion : "Adecuado para mayores de 18 años" )

            };
        }
    }
}
