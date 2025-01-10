using Dominio.Entidades;
using Infraestructura;
using Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Testing.RepositoryTests
{
    public class IdiomaRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly IdiomaRepository _repository;

        // Configuración inicial antes de cada prueba
        public IdiomaRepositoryTests()
        {
            // Configura el contexto de la base de datos en memoria
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "IdiomaTestDB")
                .Options;

            // Crea una nueva instancia del contexto en memoria
            _context = new ApplicationDbContext(options);

            // Inicia el repositorio con el contexto
            _repository = new IdiomaRepository(_context);
        }

        // Test para leer todos los idiomas
        [Fact]
        public async Task ReadAllIdiomasAsync_ShouldReturnAllIdiomas_WhenIdiomasExist()
        {
            // Arrange: Creamos idiomas de prueba
            var idioma1 = CreateSampleIdioma();
            var idioma2 = new Idioma { Id = 2, Nombre = "Inglés" };

            // Agregamos los idiomas a la base de datos
            await _repository.CreateIdiomaAsync(idioma1);
            await _repository.CreateIdiomaAsync(idioma2);

            // Act: Ejecutamos el método que queremos probar
            var result = await _repository.ReadAllIdiomasAsync();

            // Assert: Comprobamos que se devuelven todos los idiomas
            Assert.Equal(2, result.Count());
        }

        // Test para leer un idioma por ID
        [Fact]
        public async Task ReadIdiomaByIdAsync_ShouldReturnIdioma_WhenIdiomaExists()
        {
            // Arrange: Creamos un idioma de prueba
            var idioma = CreateSampleIdioma();
            await _repository.CreateIdiomaAsync(idioma);

            // Act: Ejecutamos el método que queremos probar
            var result = await _repository.ReadIdiomaByIdAsync(idioma.Id);

            // Assert: Comprobamos que el resultado es el esperado
            Assert.NotNull(result);
            Assert.Equal(idioma.Nombre, result.Nombre);
        }

        // Test para leer un idioma por nombre
        [Fact]
        public async Task ReadIdiomaByNameAsync_ShouldReturnIdioma_WhenIdiomaExists()
        {
            // Arrange: Creamos un idioma de prueba
            var idioma = CreateSampleIdioma();
            await _repository.CreateIdiomaAsync(idioma);

            // Act: Ejecutamos el método de búsqueda por nombre
            var result = await _repository.ReadIdiomaByNameAsync(idioma.Nombre);

            // Assert: Verificamos que el resultado sea correcto
            Assert.NotNull(result);
            Assert.Equal(idioma.Nombre, result.Nombre);
        }

        // Test para crear un nuevo idioma
        [Fact]
        public async Task CreateIdiomaAsync_ShouldAddIdiomaToDatabase()
        {
            // Arrange: Creamos un idioma para agregarlo a la base de datos
            var idioma = CreateSampleIdioma();

            // Act: Intentamos crear el idioma en la base de datos
            await _repository.CreateIdiomaAsync(idioma);

            // Assert: Comprobamos que el idioma fue añadido correctamente
            var result = await _context.Idiomas.FindAsync(idioma.Id);
            Assert.NotNull(result);
            Assert.Equal(idioma.Nombre, result.Nombre);
        }

        // Test para actualizar un idioma existente
        [Fact]
        public async Task UpdateIdiomaAsync_ShouldUpdateIdioma_WhenIdiomaExists()
        {
            // Arrange: Creamos un idioma de prueba
            var idioma = CreateSampleIdioma();
            await _repository.CreateIdiomaAsync(idioma);

            // Modificamos el nombre del idioma
            idioma.Nombre = "Español Modificado";

            // Act: Intentamos actualizar el idioma en la base de datos
            await _repository.UpdateIdiomaAsync(idioma);

            // Assert: Comprobamos que el idioma fue actualizado correctamente
            var result = await _repository.ReadIdiomaByIdAsync(idioma.Id);
            Assert.NotNull(result);
            Assert.Equal(idioma.Nombre, result.Nombre);
        }

        // Test para eliminar un idioma por ID
        [Fact]
        public async Task DeleteIdiomaAsync_ShouldRemoveIdioma_WhenIdiomaExists()
        {
            // Arrange: Creamos un idioma de prueba
            var idioma = CreateSampleIdioma();
            await _repository.CreateIdiomaAsync(idioma);

            // Act: Eliminamos el idioma por su ID
            await _repository.DeleteIdiomaAsync(idioma.Id);

            // Assert: Verificamos que el idioma ha sido eliminado
            var result = await _context.Idiomas.FindAsync(idioma.Id);
            Assert.Null(result);
        }

        // Método para crear un idioma de ejemplo
        private Idioma CreateSampleIdioma()
        {
            return new Idioma
            {
                Id = 1,
                Nombre = "Español"
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
