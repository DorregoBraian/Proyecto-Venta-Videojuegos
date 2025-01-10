using API_Rest_de_Videos_Juegos.Controllers;
using Aplicacion.DTOs;
using Aplicacion.IServicios;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.Controllers
{
    public class UsuarioControllerTests
    {
        private readonly Mock<IUsuarioService> _mockUsuarioService;
        private readonly UsuarioController _mockUsuarioController;

        public UsuarioControllerTests()
        {
            _mockUsuarioService = new Mock<IUsuarioService>();
            _mockUsuarioController = new UsuarioController(_mockUsuarioService.Object);
        }

        [Fact]
        public async Task ObtenerDatosDelUsuario_ReturnsOkResult()
        {
            // Arrange
            int userId = 1;
            var registrarDto = new RegistrarUsuarioDTO { Nombre = "John", Email = "john@example.com", Password = "password123" };

            _mockUsuarioService.Setup(service => service.ObtenerDatosDelUsuario(userId)).ReturnsAsync(registrarDto);

            // Act
            var result = await _mockUsuarioController.ObtenerDatosDelUsuario(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var usuarioResult = Assert.IsType<RegistrarUsuarioDTO>(okResult.Value);
            Assert.NotNull(usuarioResult);
            Assert.Equal(200,okResult.StatusCode);
            Assert.Equal(registrarDto, usuarioResult);
            Assert.Equal(registrarDto.Nombre, usuarioResult.Nombre);
            Assert.Equal(registrarDto.Email, usuarioResult.Email);
            Assert.Equal(registrarDto.Password, usuarioResult.Password);
        }

        [Fact]
        public async Task RegistrarUsuario_ReturnsOkResult()
        {
            // Arrange
            var registrarDto = new RegistrarUsuarioDTO { Nombre = "John", Email = "john@example.com", Password = "password123" };

            _mockUsuarioService.Setup(service => service.RegistrarUsuarioAsync(It.IsAny<RegistrarUsuarioDTO>())).ReturnsAsync(registrarDto);
            // Act
            var result = await _mockUsuarioController.RegistrarUsuario(registrarDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<string>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal("Usuario registrado exitosamente.", okResult.Value);
        }

        [Fact]
        public async Task LoginUsuario_ReturnsOkResult()
        {
            // Arrange
            var loginDto = new LoginUsuarioDTO { Email = "john@example.com", Password = "password123" };
            var mensaje = "Login exitoso.";
            var userId = 1;

            _mockUsuarioService.Setup(service => service.LoginUsuarioAsync(loginDto)).ReturnsAsync((mensaje, userId));

            // Act
            var result = await _mockUsuarioController.LoginUsuario(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseValue = Assert.IsType<LoginResponse>(okResult.Value);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.Equal(mensaje, responseValue.Message);
            Assert.Equal(userId, responseValue.UserId);

        }

        [Fact]
        public async Task RecuperarContrasena_ReturnsOkResult()
        {
            // Arrange
            var recuperarDto = new RecuperarContrasenaDTO { Email = "john@example.com", nuevaContrasena = "newPassword123" };

            _mockUsuarioService.Setup(service => service.RecuperarContrasenaAsync(recuperarDto.Email, recuperarDto.nuevaContrasena))
                .ReturnsAsync("Se a enviado un Correo con la nueva contraseña a su email");

            // Act
            var result = await _mockUsuarioController.RecuperarContrasena(recuperarDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}

