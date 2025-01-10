using Dominio.Entidades;
using Infraestructura;
using Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.RepositoryTests
{
    public class UsuarioRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);
            _usuarioRepository = new UsuarioRepository(_context);
        }

        [Fact]
        public async Task ObtenerPorEmailAsync_ShouldReturnUsuario_WhenEmailExists()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Nombre = "Braian", Email = "braian@test.com", PasswordHash = "hash123" };
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            // Act
            var result = await _usuarioRepository.ObtenerPorEmailAsync("braian@test.com");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Braian", result.Nombre);
        }

        [Fact]
        public async Task ObtenerPorEmailAsync_ShouldReturnNull_WhenEmailDoesNotExist()
        {
            // Act
            var result = await _usuarioRepository.ObtenerPorEmailAsync("nonexistent@test.com");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ObtenerPorTokenAsync_ShouldReturnUsuario_WhenTokenIsValid()
        {
            // Arrange
            var token = "validToken";
            var usuario = new Usuario
            {
                Id = 1,
                Nombre = "Braian",
                Email = "braian@test.com",
                PasswordHash = "hashedpassword123",
                ResetToken = token,
                TokenExpiracion = DateTime.UtcNow.AddMinutes(10)
            };
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            // Act
            var result = await _usuarioRepository.ObtenerPorTokenAsync(token);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Braian", result.Nombre);
        }

        [Fact]
        public async Task ObtenerPorTokenAsync_ShouldReturnNull_WhenTokenIsInvalid()
        {
            // Act
            var result = await _usuarioRepository.ObtenerPorTokenAsync("invalidToken");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ObtenerPorTokenAsync_ShouldReturnNull_WhenTokenHasExpired()
        {
            // Arrange
            var token = "expiredToken";
            var usuario = new Usuario
            {
                Id = 1,
                Nombre = "Braian",
                Email = "braian@test.com",
                PasswordHash = "hashedpassword123",
                ResetToken = token,
                TokenExpiracion = DateTime.UtcNow.AddMinutes(-10)
            };
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            // Act
            var result = await _usuarioRepository.ObtenerPorTokenAsync(token);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AgregarAsync_ShouldAddUsuario()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Nombre = "Braian", Email = "braian@test.com", PasswordHash = "hash123" };

            // Act
            await _usuarioRepository.AgregarAsync(usuario);

            // Assert
            var addedUsuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == "braian@test.com");
            Assert.NotNull(addedUsuario);
            Assert.Equal("Braian", addedUsuario.Nombre);
        }

        [Fact]
        public async Task ActualizarAsync_ShouldUpdateUsuario()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Nombre = "Braian", Email = "braian@test.com", PasswordHash = "hash123" };
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            usuario.Nombre = "Braian Updated";

            // Act
            await _usuarioRepository.ActualizarAsync(usuario);

            // Assert
            var updatedUsuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == "braian@test.com");
            Assert.NotNull(updatedUsuario);
            Assert.Equal("Braian Updated", updatedUsuario.Nombre);
        }

        [Fact]
        public async Task EmailExisteAsync_ShouldReturnTrue_WhenEmailExists()
        {
            // Arrange
            var usuario = new Usuario { Id = 1, Nombre = "Braian", Email = "braian@test.com", PasswordHash = "hash123" };
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            // Act
            var exists = await _usuarioRepository.EmailExisteAsync("braian@test.com");

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task EmailExisteAsync_ShouldReturnFalse_WhenEmailDoesNotExist()
        {
            // Act
            var exists = await _usuarioRepository.EmailExisteAsync("nonexistent@test.com");

            // Assert
            Assert.False(exists);
        }
    }

}
