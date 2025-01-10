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
    public class IdiomaControllerTests
    {
        private readonly Mock<IIdiomaService> _mockIdiomaService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IdiomaController _mockIdiomaController;

        public IdiomaControllerTests()
        {
            _mockIdiomaService = new Mock<IIdiomaService>();
            _mockMapper = new Mock<IMapper>();
            _mockIdiomaController = new IdiomaController(_mockIdiomaService.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetIdiomasTests()
        {
            // Arrange
            var ListIdiomas = CreateIdiomaDTOList();

            _mockIdiomaService.Setup(service => service.GetAllIdiomasAsync()).ReturnsAsync(ListIdiomas);

            // Act
            var result = await _mockIdiomaController.GetIdiomas();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<IdiomaDTO>>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200,okResult.StatusCode);
            Assert.Equal(ListIdiomas, okResult.Value);
            Assert.Equal(ListIdiomas.Count,returnValue.Count());
        }

        [Fact]
        public async Task GetIdiomaByIdTests()
        {
            // Arrange
            var idiomaDto = CreateIdiomaDTO();

            _mockIdiomaService.Setup(service => service.GetIdiomaByIdAsync(idiomaDto.Id)).ReturnsAsync(idiomaDto);

            // Act
            var result = await _mockIdiomaController.GetIdiomaById(idiomaDto.Id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<IdiomaDTO>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(returnValue, okResult.Value);
            Assert.Equal(returnValue.Id, idiomaDto.Id);
            Assert.Equal(returnValue.Nombre, idiomaDto.Nombre);
        }


       [Fact]
        public async Task CreateIdiomaTests()
        {
            // Arrange
            var idiomaDto = new IdiomaDTO { Id = 1, Nombre = "Arabe" };

            _mockIdiomaService.Setup(service => service.GetIdiomaByNombreAsync(idiomaDto.Nombre)).ReturnsAsync((IdiomaDTO)null);
            _mockIdiomaService.Setup(service => service.CreateIdiomaAsync(idiomaDto)).ReturnsAsync(idiomaDto);

            // Act
            var result = await _mockIdiomaController.CreateIdioma(idiomaDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.IsType<IdiomaDTO>(createdResult.Value);
            Assert.NotNull(createdResult);
            Assert.Equal(201,createdResult.StatusCode);
            Assert.Equal(idiomaDto, createdResult.Value);
        }

         [Fact]
        public async Task DeleteIdiomaTests()
        {
            // Arrange
            var idiomaDto = CreateIdiomaDTO();

            _mockIdiomaService.Setup(service => service.GetIdiomaByIdAsync(idiomaDto.Id)).ReturnsAsync(idiomaDto);
            _mockIdiomaService.Setup(service => service.DeleteIdiomaByIdAsync(idiomaDto.Id)).ReturnsAsync(idiomaDto);

            // Act
            var result = await _mockIdiomaController.DeleteIdioma(idiomaDto.Id);

            // Assert
            var createdResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<IdiomaDTO>(createdResult.Value);
            Assert.NotNull(createdResult);
            Assert.Equal(200, createdResult.StatusCode);
            Assert.Equal(idiomaDto, createdResult.Value);
        }


        // Método auxiliar para crear una lista de IdiomaDTO, usado en pruebas que requieren listas de datos
        private List<IdiomaDTO> CreateIdiomaDTOList()
        {
            return new List<IdiomaDTO>
            {
                new IdiomaDTO { Id = 1, Nombre = "Ingles" },
                new IdiomaDTO { Id = 2, Nombre = "Español" },
                new IdiomaDTO { Id = 3, Nombre = "Frances" },
                new IdiomaDTO { Id = 4, Nombre = "Aleman" },
                new IdiomaDTO { Id = 5, Nombre = "Italiano" },
                new IdiomaDTO { Id = 6, Nombre = "Portugues" },
                new IdiomaDTO { Id = 7, Nombre = "Ruso" },
                new IdiomaDTO { Id = 8, Nombre = "Japones" },
                new IdiomaDTO { Id = 9, Nombre = "Chino" }
            };
        }

        // Método auxiliar para crear un solo IdiomaDTO, usado en pruebas donde se necesita un único idioma
        private IdiomaDTO CreateIdiomaDTO()
        {
            return new IdiomaDTO { Id = 1, Nombre = "Ingles" };
        }
    }
}
