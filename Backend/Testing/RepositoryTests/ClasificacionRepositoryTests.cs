using Dominio.Entidades;
using Infraestructura;
using Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace Testing.RepositoryTests
{
    public class ClasificacionRepositoryTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ClasificacionRepository _repository;

        // Configuración inicial antes de cada prueba
        public ClasificacionRepositoryTests()
        {
            // Configura el contexto de la base de datos en memoria
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "ClasificacionTestDB")
                .Options;

            // Crea una nueva instancia del contexto en memoria
            _context = new ApplicationDbContext(options);

            // Inicia el repositorio con el contexto
            _repository = new ClasificacionRepository(_context);
        }

        // Test para leer todas las clasificaciones
        [Fact]
        public async Task ReadAllClasificacionesAsync_ShouldReturnAllClasificaciones_WhenClasificacionesExist()
        {
            // Arrange: Creamos clasificaciones de prueba
            var clasificacion1 = CreateSampleClasificacion();
            var clasificacion2 = new Clasificacion { Id = 2, Nombre = "PEGI 7", Descripcion = "Adecuado para mayores de 7 años" };

            // Agregamos las clasificaciones a la base de datos
            await _repository.CreateClasificacionAsync(clasificacion1);
            await _repository.CreateClasificacionAsync(clasificacion2);

            // Act: Ejecutamos el método que queremos probar
            var result = await _repository.ReadAllClasificacionesAsync();

            // Assert: Comprobamos que se devuelven todas las clasificaciones
            Assert.Equal(2, result.Count());
        }

        // Test para leer una clasificación por ID
        [Fact]
        public async Task ReadClasificacionByIdAsync_ShouldReturnClasificacion_WhenClasificacionExists()
        {
            // Arrange: Creamos una clasificación de prueba
            var clasificacion = CreateSampleClasificacion();
            await _repository.CreateClasificacionAsync(clasificacion);

            // Act: Ejecutamos el método que queremos probar
            var result = await _repository.ReadClasificacionByIdAsync(clasificacion.Id);

            // Assert: Comprobamos que el resultado es el esperado
            Assert.NotNull(result);
            Assert.Equal(clasificacion.Nombre, result.Nombre);
        }

        // Test para leer una clasificación por nombre
        [Fact]
        public async Task ReadClasificacionByNameAsync_ShouldReturnClasificacion_WhenClasificacionExists()
        {
            // Arrange: Creamos una clasificación de prueba
            var clasificacion = CreateSampleClasificacion();
            await _repository.CreateClasificacionAsync(clasificacion);

            // Act: Ejecutamos el método de búsqueda por nombre
            var result = await _repository.ReadClasificacionByNameAsync(clasificacion.Nombre);

            // Assert: Verificamos que el resultado sea correcto
            Assert.NotNull(result);
            Assert.Equal(clasificacion.Nombre, result.Nombre);
        }

        // Test para crear una nueva clasificación
        [Fact]
        public async Task CreateClasificacionAsync_ShouldAddClasificacionToDatabase()
        {
            // Arrange: Creamos una clasificación para agregarla a la base de datos
            var clasificacion = CreateSampleClasificacion();

            // Act: Intentamos crear la clasificación en la base de datos
            await _repository.CreateClasificacionAsync(clasificacion);

            // Assert: Comprobamos que la clasificación fue añadida correctamente
            var result = await _context.Clasificaciones.FindAsync(clasificacion.Id);
            Assert.NotNull(result);
            Assert.Equal(clasificacion.Nombre, result.Nombre);
        }

        // Test para actualizar una clasificación existente
        [Fact]
        public async Task UpdateClasificacionAsync_ShouldUpdateClasificacion_WhenClasificacionExists()
        {
            // Arrange: Creamos una clasificación de prueba
            var clasificacion = CreateSampleClasificacion();
            await _repository.CreateClasificacionAsync(clasificacion);

            // Modificamos el nombre de la clasificación
            clasificacion.Nombre = "Aventura Modificada";

            // Act: Intentamos actualizar la clasificación en la base de datos
            await _repository.UpdateClasificacionAsync(clasificacion);

            // Assert: Comprobamos que la clasificación fue actualizada correctamente
            var result = await _repository.ReadClasificacionByIdAsync(clasificacion.Id);
            Assert.NotNull(result);
            Assert.Equal(clasificacion.Nombre, result.Nombre);
        }

        // Test para eliminar una clasificación por ID
        [Fact]
        public async Task DeleteClasificacionAsync_ShouldRemoveClasificacion_WhenClasificacionExists()
        {
            // Arrange: Creamos una clasificación de prueba
            var clasificacion = CreateSampleClasificacion();
            await _repository.CreateClasificacionAsync(clasificacion);

            // Act: Eliminamos la clasificación por su ID
            await _repository.DeleteClasificacionAsync(clasificacion.Id);

            // Assert: Verificamos que la clasificación ha sido eliminada
            var result = await _context.Clasificaciones.FindAsync(clasificacion.Id);
            Assert.Null(result);
        }

        // Método para crear una clasificación de ejemplo
        private Clasificacion CreateSampleClasificacion()
        {
            return new Clasificacion
            {
                Id = 1,
                Nombre = "PEGI 3",
                Descripcion = "Adecuado para todas las edades"
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
