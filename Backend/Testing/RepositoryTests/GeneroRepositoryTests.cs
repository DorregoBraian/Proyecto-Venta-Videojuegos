using Dominio.Entidades;
using Infraestructura;
using Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Testing.RepositoryTests
{
    public class GeneroRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly GeneroRepository _repository;

        // Configuración inicial antes de cada prueba
        public GeneroRepositoryTests()
        {
            // Configura el contexto de la base de datos en memoria
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "GeneroTestDB")
                .Options;

            // Crea una nueva instancia del contexto en memoria
            _context = new ApplicationDbContext(options);

            // Inicia el repositorio con el contexto
            _repository = new GeneroRepository(_context);
        }

        // Test para leer todos los géneros
        [Fact]
        public async Task ReadAllGenerosAsync_ShouldReturnAllGeneros_WhenGenerosExist()
        {
            // Arrange: Creamos géneros de prueba
            var genero1 = CreateSampleGenero();
            var genero2 = new Genero { Id = 2, Nombre = "Aventura" };

            // Agregamos los géneros a la base de datos
            await _repository.CreateGeneroAsync(genero1);
            await _repository.CreateGeneroAsync(genero2);

            // Act: Ejecutamos el método que queremos probar
            var result = await _repository.ReadAllGenerosAsync();

            // Assert: Comprobamos que se devuelven todos los géneros
            Assert.Equal(2, result.Count());
        }

        // Test para leer un género por ID
        [Fact]
        public async Task ReadGeneroByIdAsync_ShouldReturnGenero_WhenGeneroExists()
        {
            // Arrange: Creamos un género de prueba
            var genero = CreateSampleGenero();
            await _repository.CreateGeneroAsync(genero);

            // Act: Ejecutamos el método que queremos probar
            var result = await _repository.ReadGeneroByIdAsync(genero.Id);

            // Assert: Comprobamos que el resultado es el esperado
            Assert.NotNull(result);
            Assert.Equal(genero.Nombre, result.Nombre);
        }

        // Test para leer un género por nombre
        [Fact]
        public async Task ReadGeneroByNameAsync_ShouldReturnGenero_WhenGeneroExists()
        {
            // Arrange: Creamos un género de prueba
            var genero = CreateSampleGenero();
            await _repository.CreateGeneroAsync(genero);

            // Act: Ejecutamos el método de búsqueda por nombre
            var result = await _repository.ReadGeneroByNameAsync(genero.Nombre);

            // Assert: Verificamos que el resultado sea correcto
            Assert.NotNull(result);
            Assert.Equal(genero.Nombre, result.Nombre);
        }

        // Test para crear un nuevo género
        [Fact]
        public async Task CreateGeneroAsync_ShouldAddGeneroToDatabase()
        {
            // Arrange: Creamos un género para agregarlo a la base de datos
            var genero = CreateSampleGenero();

            // Act: Intentamos crear el género en la base de datos
            await _repository.CreateGeneroAsync(genero);

            // Assert: Comprobamos que el género fue añadido correctamente
            var result = await _context.Generos.FindAsync(genero.Id);
            Assert.NotNull(result);
            Assert.Equal(genero.Nombre, result.Nombre);
        }

        // Test para actualizar un género existente
        [Fact]
        public async Task UpdateGeneroAsync_ShouldUpdateGenero_WhenGeneroExists()
        {
            // Arrange: Creamos un género de prueba
            var genero = CreateSampleGenero();
            await _repository.CreateGeneroAsync(genero);

            // Modificamos el nombre del género
            genero.Nombre = "Acción Modificado";

            // Act: Intentamos actualizar el género en la base de datos
            await _repository.UpdateGeneroAsync(genero);

            // Assert: Comprobamos que el género fue actualizado correctamente
            var result = await _repository.ReadGeneroByIdAsync(genero.Id);
            Assert.NotNull(result);
            Assert.Equal(genero.Nombre, result.Nombre);
        }

        // Test para eliminar un género por ID
        [Fact]
        public async Task DeleteGeneroAsync_ShouldRemoveGenero_WhenGeneroExists()
        {
            // Arrange: Creamos un género de prueba
            var genero = CreateSampleGenero();
            await _repository.CreateGeneroAsync(genero);

            // Act: Eliminamos el género por su ID
            await _repository.DeleteGeneroAsync(genero.Id);

            // Assert: Verificamos que el género ha sido eliminado
            var result = await _context.Generos.FindAsync(genero.Id);
            Assert.Null(result);
        }

        // Método para crear un género de ejemplo
        private Genero CreateSampleGenero()
        {
            return new Genero
            {
                Id = 1, 
                Nombre = "Acción"
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
