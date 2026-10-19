using Dominio.IRepositorios;
using Tarea_en_Segundo_Plano.Data;

namespace Tarea_en_Segundo_Plano
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _configuration;
        private readonly IHostApplicationLifetime _lifetime;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory, IConfiguration configuration, IHostApplicationLifetime lifetime)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _configuration = configuration;
            _lifetime = lifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker de carga de datos iniciado: {time}", DateTimeOffset.Now);

            try
            {
                using var scope = _serviceScopeFactory.CreateScope();

                // Resolver el DataSeeder con sus dependencias Scoped
                var dataSeeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();

                // Ruta del JSON: primero configuración (JsonFilePath), si no, la carpeta de salida
                var jsonFilePath = _configuration["JsonFilePath"];
                if (string.IsNullOrWhiteSpace(jsonFilePath))
                {
                    jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "DataGame.json");
                }

                // Se ejecuta UNA sola vez y el worker finaliza
                await dataSeeder.SeedDataAsync(jsonFilePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante la ejecución del Worker.");
            }

            _logger.LogInformation("Worker finalizado. Deteniendo la aplicación.");

            // Termina el proceso al finalizar (útil como job de una sola ejecución)
            _lifetime.StopApplication();
        }
    }
}
