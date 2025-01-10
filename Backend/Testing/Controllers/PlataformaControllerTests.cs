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
    public class PlataformaControllerTests
    {
        private readonly Mock<IPlataformaService> _mockPlataformaService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PlataformaController _mockPlataformaController;

        public PlataformaControllerTests()
        {
            _mockPlataformaService = new Mock<IPlataformaService>();
            _mockMapper = new Mock<IMapper>();
            _mockPlataformaController = new PlataformaController(_mockPlataformaService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllPlataformasTests()
        {
            // Arrange
            var listPlataformas = CrearPlataformasDTOList();

            _mockPlataformaService.Setup(service => service.GetAllPlataformasAsync()).ReturnsAsync(listPlataformas);

            // Act
            var result = await _mockPlataformaController.GetAllPlataformas();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<PlataformaDTO>>(okResult.Value);
            Assert.NotNull(returnValue);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(listPlataformas, okResult.Value);
            Assert.Equal(listPlataformas.Count, returnValue.Count());
        }

        [Fact]
        public async Task GetPlataformaByIdTests()
        {
            // Arrange
            var plataformaDto = CrearPlataformaDTO();

            _mockPlataformaService.Setup(service => service.GetPlataformaByIdAsync(plataformaDto.Id)).ReturnsAsync(plataformaDto);

            // Act
            var result = await _mockPlataformaController.GetPlataformaById(plataformaDto.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<PlataformaDTO>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(plataformaDto, okResult.Value);
            Assert.Equal(plataformaDto.Id, returnValue.Id);
            Assert.Equal(plataformaDto.Nombre, returnValue.Nombre);
        }

        [Fact]
        public async Task CreatePlataformaTests()
        {
            // Arrange
            var nuevaPlataforma = new PlataformaDTO { Id = 10, Nombre = "PC 5" };

            _mockPlataformaService.Setup(service => service.GetPlataformaByNombreAsync(nuevaPlataforma.Nombre)).ReturnsAsync((PlataformaDTO)null);
            _mockPlataformaService.Setup(service => service.CreatePlataformaAsync(nuevaPlataforma)).ReturnsAsync(nuevaPlataforma);

            // Act
            var result = await _mockPlataformaController.CreatePlataforma(nuevaPlataforma);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.IsType<PlataformaDTO>(createdResult.Value);
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal(nuevaPlataforma, createdResult.Value);
        }

        [Fact]
        public async Task UpdatePlataformaTests()
        {
            // Arrange
            var nuevaPlataforma = CrearPlataformaDTO();

            _mockPlataformaService.Setup(service => service.GetPlataformaByIdAsync(1)).ReturnsAsync(nuevaPlataforma);
            _mockPlataformaService.Setup(service => service.UpdatePlataformaByIdAsync(1, nuevaPlataforma)).ReturnsAsync(nuevaPlataforma);

            // Act
            var result = await _mockPlataformaController.UpdatePlataforma(1, nuevaPlataforma);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<PlataformaDTO>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(nuevaPlataforma, okResult.Value);
        }

        [Fact]
        public async Task DeletePlataformaTests()
        {
            // Arrange
            var plataformaDto = CrearPlataformaDTO();

            _mockPlataformaService.Setup(service => service.GetPlataformaByIdAsync(plataformaDto.Id)).ReturnsAsync(plataformaDto);
            _mockPlataformaService.Setup(service => service.DeletePlataformaByIdAsync(plataformaDto.Id)).ReturnsAsync(plataformaDto);

            // Act
            var result = await _mockPlataformaController.DeletePlataforma(plataformaDto.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<PlataformaDTO>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(plataformaDto, okResult.Value);
        }


        // Método auxiliar para crear una lista de PlataformaDTOs de prueba
        private List<PlataformaDTO> CrearPlataformasDTOList()
        {
            return new List<PlataformaDTO>
            {
                new PlataformaDTO { Id = 1, Nombre = "PlayStation" },
                new PlataformaDTO { Id = 2, Nombre = "Xbox" },
                new PlataformaDTO { Id = 3, Nombre = "Nintendo Switch" },
                new PlataformaDTO { Id = 4, Nombre = "PC" },
                new PlataformaDTO { Id= 5, Nombre = "Game Boy Advance" },
                new PlataformaDTO { Id = 6, Nombre = "Android" },
                new PlataformaDTO { Id = 7, Nombre = "iOS" },
                new PlataformaDTO { Id = 8, Nombre = "Steam Deck" },
                new PlataformaDTO { Id = 9, Nombre = "Google Stadia" }
            };
        }

        // Método auxiliar para crear una plataforma DTO individual
        private PlataformaDTO CrearPlataformaDTO()
        {
            return new PlataformaDTO { Id = 4, Nombre = "PC" };
        }
    }
}
