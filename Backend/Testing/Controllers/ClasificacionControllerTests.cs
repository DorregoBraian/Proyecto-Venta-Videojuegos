using API_Rest_de_Videos_Juegos.Controllers;
using Aplicacion.DTOs;
using Aplicacion.IServicios;
using AutoMapper;
using Dominio.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Testing.Controllers
{
    public class ClasificacionControllerTests
    {
        private readonly Mock<IClasificacionService> _mockClasificacionService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ClasificacionController _mockClasificacionController;

        public ClasificacionControllerTests()
        {
            _mockClasificacionService = new Mock<IClasificacionService>();
            _mockMapper = new Mock<IMapper>();
            _mockClasificacionController = new ClasificacionController(_mockClasificacionService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllClasificacionesTests()
        {
            // Arrange
            var listClasificacionesDto = CrearListaClasificacionesDto();

            _mockClasificacionService.Setup(service => service.GetAllClasificacionesAsync()).ReturnsAsync(listClasificacionesDto);

            // Act
            var result = await _mockClasificacionController.GetAllClasificaciones();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<ClasificacionDTO>>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(listClasificacionesDto, okResult.Value);
            Assert.Equal(listClasificacionesDto.Count, returnValue.Count()); 
        }

        [Fact]
        public async Task GetClasificacionByIdTests()
        {
            // Arrange
            var clasificacionDto = CrearClasificacionDto();

            _mockClasificacionService.Setup(service => service.GetClasificacionByIdAsync(1)).ReturnsAsync(clasificacionDto);

            // Act
            var result = await _mockClasificacionController.GetClasificacionById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ClasificacionDTO>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(clasificacionDto, okResult.Value);
            Assert.Equal(clasificacionDto.Id, returnValue.Id);
            Assert.Equal(clasificacionDto.Nombre, returnValue.Nombre);
            Assert.Equal(clasificacionDto.Descripcion, returnValue.Descripcion);
        }

        [Fact]
        public async Task CreateClasificacionTests()
        {
            // Arrange
            var clasificacionNuevo = new ClasificacionDTO { Id = 2, Nombre = "PEGI 50", Descripcion = "Adecuado para mayores de 50 años" };

            _mockClasificacionService.Setup(service => service.GetClasificacionByNombreAsync(clasificacionNuevo.Nombre)).ReturnsAsync((ClasificacionDTO)null); 
            _mockClasificacionService.Setup(service => service.CreateClasificacionAsync(clasificacionNuevo)).ReturnsAsync(clasificacionNuevo); 

            // Act
            var result = await _mockClasificacionController.CreateClasificacion(clasificacionNuevo);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.IsType<ClasificacionDTO>(createdResult.Value);
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal(clasificacionNuevo, createdResult.Value);

        }


        [Fact]
        public async Task UpdateClasificacionTests()
        {
            // Arrange
            var clasificacionDto = CrearClasificacionDto();
            var clasificacionDtoNuevo = new ClasificacionDTO { Id= 1, Nombre = "PEGI 3", Descripcion = "Todas las edades"};

            _mockClasificacionService.Setup(service => service.GetClasificacionByIdAsync(clasificacionDtoNuevo.Id)).ReturnsAsync(clasificacionDto);
            _mockClasificacionService.Setup(service => service.UpdateClasificacionByIdAsync(1, clasificacionDtoNuevo)).ReturnsAsync(clasificacionDtoNuevo);

            // Act
            var result = await _mockClasificacionController.UpdateClasificacion(1, clasificacionDtoNuevo);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<ClasificacionDTO>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(clasificacionDtoNuevo, okResult.Value);
        }


        [Fact]
        public async Task DeleteClasificacionTests()
        {
            // Arrange
            var clasificacionDto = CrearClasificacionDto();

            _mockClasificacionService.Setup(service => service.GetClasificacionByIdAsync(clasificacionDto.Id)).ReturnsAsync(clasificacionDto);
            _mockClasificacionService.Setup(service => service.DeleteClasificacionByIdAsync(clasificacionDto.Id)).ReturnsAsync(clasificacionDto);

            // Act
            var result = await _mockClasificacionController.DeleteClasificacion(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<ClasificacionDTO>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(clasificacionDto, okResult.Value);
        }

        // ----------------- Métodos Auxiliares -----------------

        // Método auxiliar para crear un DTO de Clasificacion de ejemplo
        private ClasificacionDTO CrearClasificacionDto(int id = 1, string nombre = "PEGI 3", string descripcion = "Adecuado para todas las edades")
        {
            return new ClasificacionDTO { Id = id, Nombre = nombre, Descripcion = descripcion };
        }

        // Método auxiliar para crear una lista de ClasificacionDTOs
        private List<ClasificacionDTO> CrearListaClasificacionesDto()
        {
            return new List<ClasificacionDTO>
            {
                CrearClasificacionDto(id : 1, nombre : "PEGI 3", descripcion : "Adecuado para todas las edades"),
                CrearClasificacionDto(id : 2, nombre : "PEGI 7", descripcion : "Adecuado para mayores de 7 años"),
                CrearClasificacionDto(id : 3, nombre : "PEGI 12", descripcion : "Adecuado para mayores de 12 años"),
                CrearClasificacionDto(id : 4, nombre : "PEGI 16", descripcion : "Adecuado para mayores de 16 años" ),
                CrearClasificacionDto(id : 5, nombre : "PEGI 18", descripcion : "Adecuado para mayores de 18 años" )

            };
        }
    }
}
