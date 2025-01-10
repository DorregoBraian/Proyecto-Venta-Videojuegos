using API_Rest_de_Videos_Juegos.Controllers;
using Aplicacion.DTOs;
using Aplicacion.IServicios;
using AutoMapper;
using Dominio.Entidades;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Controllers
{
    public class GeneroControllerTests
    {
        private readonly Mock<IGeneroService> _mockGeneroService; // Mock del servicio de géneros
        private readonly Mock<IMapper> _mockMapper; // Mock del mapeador
        private readonly GeneroController _mockGeneroController; // Instancia del controlador a probar

        public GeneroControllerTests()
        {
            _mockGeneroService = new Mock<IGeneroService>(); // Inicializa el mock del servicio
            _mockMapper = new Mock<IMapper>(); // Inicializa el mock del mapeador
            _mockGeneroController = new GeneroController(_mockGeneroService.Object, _mockMapper.Object); // Crea la instancia del controlador
        }

        [Fact]
        public async Task GetAllGenerosTests()
        {
            // Arrange
            var generosList = CreateGeneroDtoList();

            _mockGeneroService.Setup(service => service.GetAllGenerosAsync()).ReturnsAsync(generosList);

            // Act
            var result = await _mockGeneroController.GetAllGeneros();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<GeneroDTO>>(okResult.Value);
            Assert.Equal(generosList.Count, returnValue.Count());
        }

        [Fact]
        public async Task GetGeneroByIdTests()
        {
            // Arrange
            var generoDto = CreateGeneroDto();

            _mockGeneroService.Setup(service => service.GetGeneroByIdAsync(1)).ReturnsAsync(generoDto);

            // Act
            var result = await _mockGeneroController.GetGeneroById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GeneroDTO>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(generoDto, okResult.Value);
            Assert.Equal(generoDto.Id, returnValue.Id);
            Assert.Equal(generoDto.Nombre, returnValue.Nombre);
        }

        [Fact]
        public async Task CreateGeneroTests()
        {
            // Arrange
            var generoDtoNuevo = new GeneroDTO { Id = 1, Nombre = "Acción" };

            _mockGeneroService.Setup(service => service.GetGeneroByNombreAsync(generoDtoNuevo.Nombre)).ReturnsAsync((GeneroDTO)null);
            _mockGeneroService.Setup(service => service.CreateGeneroAsync(generoDtoNuevo)).ReturnsAsync(generoDtoNuevo);

            // Act
            var result = await _mockGeneroController.CreateGenero(generoDtoNuevo);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.IsType<GeneroDTO>(generoDtoNuevo);
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal(generoDtoNuevo, createdResult.Value);
        }

        [Fact]
        public async Task UpdateGeneroTests()
        {
            // Arrange
            var generoDto = CreateGeneroDto();
            var generoDtoNuevo = new GeneroDTO { Id = 1, Nombre = "Acción" };

            _mockGeneroService.Setup(service => service.GetGeneroByIdAsync(generoDto.Id)).ReturnsAsync(generoDto);
            _mockGeneroService.Setup(service => service.UpdateGeneroByIdAsync(1, generoDtoNuevo)).ReturnsAsync(generoDtoNuevo);

            // Act
            var result = await _mockGeneroController.UpdateGenero(1, generoDtoNuevo);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<GeneroDTO>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(generoDtoNuevo, okResult.Value);
        }

        [Fact]
        public async Task DeleteGeneroTests()
        {
            // Arrange
            var generoDto = CreateGeneroDto();

            _mockGeneroService.Setup(service => service.GetGeneroByIdAsync(generoDto.Id)).ReturnsAsync(generoDto);
            _mockGeneroService.Setup(service => service.DeleteGeneroByIdAsync(generoDto.Id)).ReturnsAsync(generoDto);

            // Act
            var result = await _mockGeneroController.DeleteGenero(generoDto.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<GeneroDTO>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(generoDto, okResult.Value);
        }


        // Método auxiliar para crear un géneroDto
        private GeneroDTO CreateGeneroDto(int Id = 1, string Nombre = "Accion")
        {
            return new GeneroDTO { Id = 1, Nombre = "Acción" };
        }

        // Método auxiliar para crear una lista de DTOs de géneros
        private List<GeneroDTO> CreateGeneroDtoList()
        {
            return new List<GeneroDTO>
            {
                new GeneroDTO { Id = 1, Nombre = "Acción" },
                new GeneroDTO { Id = 2, Nombre = "Aventura" },
                new GeneroDTO { Id = 3, Nombre = "RPG" },
                new GeneroDTO { Id = 4, Nombre = "Deportes" },
                new GeneroDTO { Id = 5, Nombre = "Terror" }
            };
        }
    }
}
