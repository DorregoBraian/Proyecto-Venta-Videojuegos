using Dominio.IRepositorios;
using Tarea_en_Segundo_Plano.Data;

namespace Tarea_en_Segundo_Plano
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker ejecutando la carga de datos: {time}", DateTimeOffset.Now);

                try
                {
                    // Crear un alcance para resolver el DataSeeder y sus dependencias Scoped
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        // Obtener el repositorio Scoped dentro del alcance
                        var juegoRepository = scope.ServiceProvider.GetRequiredService<IJuegoRepository>();

                        // Resolver el DataSeeder con sus dependencias Scoped
                        var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();

                        // Llamar al método SeedDataAsync
                        await dataSeeder.SeedDataAsync(
                            "C:/Users/Rayan/source/repos/API Rest de Videos Juegos/Tarea en Segundo Plano/Data/DataGame.json");
                    }

                    // Esperar 1 minuto antes de la siguiente ejecución
                    //await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error durante la ejecución del Worker.");
                }
            }
        }
    }
}