using System;
using Dominio.Entidades;
using Infraestructura;
using Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Testing.RepositoryTests
{
    public class PlataformaRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly PlataformaRepository _repository;

        // Configuración inicial antes de cada prueba
        public PlataformaRepositoryTests()
        {
            // Configura el contexto de la base de datos en memoria
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PlataformaTestDB")
                .Options;

            // Crea una nueva instancia del contexto en memoria
            _context = new ApplicationDbContext(options);

            // Inicia el repositorio con el contexto
            _repository = new PlataformaRepository(_context);
        }

        // Test para leer todas las plataformas
        [Fact]
        public async Task ReadAllPlataformasAsyncTests()
        {
            // Arrange: Creamos plataformas de prueba
            var plataforma1 = CreateSamplePlataforma();
            var plataforma2 = new Plataforma { Id = 2, Nombre = "PlayStation" };

            // Agregamos las plataformas a la base de datos
            await _repository.CreatePlataformaAsync(plataforma1);
            await _repository.CreatePlataformaAsync(plataforma2);

            // Act: Ejecutamos el método que queremos probar
            var result = await _repository.ReadAllPlataformasAsync();

            // Assert: Comprobamos que se devuelven todas las plataformas
            Assert.Equal(2, result.Count());
        }

        // Test para leer una plataforma por ID
        [Fact]
        public async Task ReadPlataformaByIdAsyncTests()
        {
            // Arrange: Creamos una plataforma de prueba
            var plataforma = CreateSamplePlataforma();
            await _repository.CreatePlataformaAsync(plataforma);

            // Act: Ejecutamos el método que queremos probar
            var result = await _repository.ReadPlataformaByIdAsync(plataforma.Id);

            // Assert: Comprobamos que el resultado es el esperado
            Assert.NotNull(result);
            Assert.Equal(plataforma.Nombre, result.Nombre);
        }

        // Test para leer una plataforma por nombre
        [Fact]
        public async Task ReadPlataformaByNameAsyncTests()
        {
            // Arrange: Creamos una plataforma de prueba
            var plataforma = CreateSamplePlataforma();
            await _repository.CreatePlataformaAsync(plataforma);

            // Act: Ejecutamos el método de búsqueda por nombre
            var result = await _repository.ReadPlataformaByNameAsync(plataforma.Nombre);

            // Assert: Verificamos que el resultado sea correcto
            Assert.NotNull(result);
            Assert.Equal(plataforma.Nombre, result.Nombre);
        }

        // Test para crear una nueva plataforma
        [Fact]
        public async Task CreatePlataformaAsyncTests()
        {
            // Arrange: Creamos una plataforma para agregarla a la base de datos
            var plataforma = CreateSamplePlataforma();

            // Act: Intentamos crear la plataforma en la base de datos
            await _repository.CreatePlataformaAsync(plataforma);

            // Assert: Comprobamos que la plataforma fue añadida correctamente
            var result = await _context.Plataformas.FindAsync(plataforma.Id);
            Assert.NotNull(result);
            Assert.Equal(plataforma.Nombre, result.Nombre);
        }

        // Test para eliminar una plataforma por ID
        [Fact]
        public async Task DeletePlataformaByIdAsyncTests()
        {
            // Arrange: Creamos una plataforma de prueba
            var plataforma = CreateSamplePlataforma();
            await _repository.CreatePlataformaAsync(plataforma);

            // Act: Eliminamos la plataforma por su ID
            await _repository.DeletePlataformaAsync(plataforma.Id);

            // Assert: Verificamos que la plataforma ha sido eliminada
            var result = await _context.Plataformas.FindAsync(plataforma.Id);
            Assert.Null(result);
        }

        // Método para crear una plataforma de ejemplo
        private Plataforma CreateSamplePlataforma()
        {
            return new Plataforma
            {
                Id = 1,
                Nombre = "PC"
            };
        }

        // Método Dispose para limpiar los recursos después de cada prueba
        public void Dispose()
        {
            _context.Database.EnsureDeleted(); // Elimina la base de datos en memoria al finalizar las pruebas
            _context.Dispose();
        }
    }
}
