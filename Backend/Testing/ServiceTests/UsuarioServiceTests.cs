using Aplicacion.DTOs;
using Aplicacion.Servicio;
using AutoMapper;
using Dominio.Entidades;
using Dominio.IRepositorios;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Testing.ServiceTests
{
    public class UsuarioServiceTests
    {
        private readonly Mock<IUsuarioRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly UsuarioService _usuarioService;
        private readonly Mock<SmtpClient> _mockSmtpClient;


        public UsuarioServiceTests()
        {
            // Inicializar mocks
            _mockRepository = new Mock<IUsuarioRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockConfiguration = new Mock<IConfiguration>();

            // Configurar valores predeterminados para el mock de configuración
            _mockConfiguration.Setup(config => config["EmailDeGemail"]).Returns("miemail@gmail.com");
            _mockConfiguration.Setup(config => config["PasswordDeGemail"]).Returns("micontraseña");

            // Inicializar el servicio con los mocks
            _usuarioService = new UsuarioService(_mockRepository.Object, _mockMapper.Object, _mockConfiguration.Object);
        }

        [Fact]
        public async Task ObtenerDatosDelUsuario_DeberiaDevolverUsuario_CuandoIdEsValido()
        {
            // Arrange
            int id = 1;
            var usuario = new Usuario { Id = id, Nombre = "John", Email = "john.doe@test.com", };
            var usuarioDTO = new RegistrarUsuarioDTO { Nombre = "John", Email = "john.doe@test.com" };

            _mockRepository.Setup(repo => repo.ObtenerPorIdAsync(id)).ReturnsAsync(usuario);
            _mockMapper.Setup(mapper => mapper.Map<RegistrarUsuarioDTO>(usuario)).Returns(usuarioDTO);

            // Act
            var resultado = await _usuarioService.ObtenerDatosDelUsuario(id);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(usuarioDTO.Nombre, resultado.Nombre);
            Assert.Equal(usuarioDTO.Email, resultado.Email);
            Assert.IsType<RegistrarUsuarioDTO>(resultado);

            // Verificar que se llamó el repositorio y el mapeo correctamente
            _mockRepository.Verify(repo => repo.ObtenerPorIdAsync(id), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<RegistrarUsuarioDTO>(usuario), Times.Once);
        }

        [Fact]
        public async Task LoginUsuarioAsyncTests()
        {
            // Arrange
            var loginDto = new LoginUsuarioDTO { Email = "test@test.com", Password = "password123" };
            var hashedPassword = HashearContrasena("password123");
            var usuario = new Usuario { Id = 1, Email = "test@test.com", PasswordHash = hashedPassword };

            _mockRepository.Setup(repo => repo.ObtenerPorEmailAsync(loginDto.Email)).ReturnsAsync(usuario);

            var usuarioService = new UsuarioService(_mockRepository.Object, null, null);

            // Act
            var (mensaje, userId) = await usuarioService.LoginUsuarioAsync(loginDto);

            // Assert
            Assert.Equal("Inicio de sesión exitoso.", mensaje);
            Assert.Equal(usuario.Id, userId);
            Assert.IsType<(string mensaje, int userId)>(( mensaje, userId));

            // Verificar que se llamó el repositorio correctamente
            _mockRepository.Verify(repo => repo.ObtenerPorEmailAsync(loginDto.Email), Times.Once);

        }

        [Fact]
        public async Task RegistrarUsuarioAsyncTests()
        {
            // Arrange
            var registrarDto = new RegistrarUsuarioDTO { Email = "test@correo.com", Password = "contraseña123" };

            _mockRepository.Setup(repo => repo.EmailExisteAsync(registrarDto.Email)).ReturnsAsync(false);
            _mockMapper.Setup(m => m.Map<Usuario>(It.IsAny<RegistrarUsuarioDTO>())).Returns(new Usuario());

            // Act
            var result = await _usuarioService.RegistrarUsuarioAsync(registrarDto);

            // Assert
            Assert.NotNull(result); // Verificar que el resultado no es nulo
            Assert.Equal(registrarDto, result); // Verificar que el DTO de registro es igual al resultado
            Assert.IsType<RegistrarUsuarioDTO>(result); // Verificar que el resultado es del tipo correcto

            // Verificar que se llamó el repositorio y el mapeo correctamente
            _mockRepository.Verify(repo => repo.EmailExisteAsync(registrarDto.Email), Times.Once);
            _mockRepository.Verify(repo => repo.AgregarAsync(It.IsAny<Usuario>()), Times.Once);
        }

        /*[Fact]
        public async Task RecuperarContrasenaAsyncTests()
        {
            // Arrange
            var email = "test@correo.com";
            var nuevaContrasena = "nuevaContrasena123";
            var usuario = new Usuario { Email = email, Nombre = "Juan" };

            // Setup mocks
            _mockRepository.Setup(repo => repo.ObtenerPorEmailAsync(email)).ReturnsAsync(usuario);
            _mockRepository.Setup(repo => repo.ActualizarAsync(It.IsAny<Usuario>())).Returns(Task.CompletedTask);

            // Act
            var result = await _usuarioService.RecuperarContrasenaAsync(email, nuevaContrasena);

            // Assert

            // Verificar el mensaje de retorno
            Assert.Equal("Se a enviado un Correo con la nueva contraseña a su email", result);            

            // Verificar que la contraseña fue correctamente hasheada
            var hashedPassword = _mockRepository.Invocations[0].Arguments[0] as Usuario;
            Assert.NotNull(hashedPassword); // Verificar que el resultado no es nulo
            Assert.NotEqual(nuevaContrasena, hashedPassword.PasswordHash); // La contraseña debería estar hasheada

            // Verificar que el token y la expiración se generaron correctamente
            Assert.NotNull(usuario.ResetToken);
            Assert.True(usuario.TokenExpiracion > DateTime.UtcNow); // El token debe tener una fecha de expiración futura
        }*/


        /*[Fact]
        public async Task EnviarCorreoTests()
        {
            // Arrange
            string destinatario = "test@example.com";
            string asunto = "Asunto de prueba";
            string mensaje = "<h1>Mensaje de prueba</h1>";

            // Caso 1: Configuración válida
            _mockConfiguration.Setup(config => config["EmailDeGemail"]).Returns("miemail@gmail.com");
            _mockConfiguration.Setup(config => config["PasswordDeGemail"]).Returns("micontraseña");

            var usuarioService = new UsuarioService(null, null, _mockConfiguration.Object);

            var smtpClientMock = new Mock<SmtpClient>("smtp.gmail.com", 587)
            {
                CallBase = true
            };

            smtpClientMock.Setup(client => client.SendMailAsync(It.IsAny<MailMessage>()))
                          .Returns(Task.CompletedTask);

            // Act & Assert - Caso exitoso
            await _usuarioService.EnviarCorreo(destinatario, asunto, mensaje);

            smtpClientMock.Verify(client => client.SendMailAsync(It.Is<MailMessage>(m =>
                m.From.Address == "miemail@gmail.com" &&
                m.To.Contains(new MailAddress(destinatario)) &&
                m.Subject == asunto &&
                m.Body == mensaje &&
                m.IsBodyHtml == true
            )), Times.Once);

            // Caso 2: Error al enviar el correo
            smtpClientMock.Reset();
            smtpClientMock.Setup(client => client.SendMailAsync(It.IsAny<MailMessage>())).ThrowsAsync(new Exception("Error de prueba al enviar correo"));

            var exception = await Assert.ThrowsAsync<Exception>(() => usuarioService.EnviarCorreo(destinatario, asunto, mensaje));

            Assert.Equal("Error de prueba al enviar correo", exception.Message);

            smtpClientMock.Verify(client => client.SendMailAsync(It.IsAny<MailMessage>()), Times.Once);

            // Caso 3: Credenciales faltantes
            _mockConfiguration.Setup(config => config["EmailDeGemail"]).Returns((string)null);
            _mockConfiguration.Setup(config => config["PasswordDeGemail"]).Returns((string)null);

            var usuarioServiceSinCredenciales = new UsuarioService(null, null, _mockConfiguration.Object);

            var exceptionCredenciales = await Assert.ThrowsAsync<ArgumentNullException>(() =>
                usuarioServiceSinCredenciales.EnviarCorreo(destinatario, asunto, mensaje));

            Assert.NotNull(exceptionCredenciales);
            Assert.Contains("EmailDeGemail", exceptionCredenciales.Message);
        }*/

        // ---------------- Metodos Auxiliares ----------------

        // Método para hacer el hash de la contraseña (SHA256)
        private string HashearContrasena(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashed = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashed); // Retornar la contraseña en hash
            }
        }

        // Método para Generar un Token
        private string GenerarToken()
        {
            var tokenBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokenBytes);
            }
            return Convert.ToBase64String(tokenBytes);
        }

    }
}
