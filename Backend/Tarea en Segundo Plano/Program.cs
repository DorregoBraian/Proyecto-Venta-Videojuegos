using API_Rest_de_Videos_Juegos;
using Dominio.IRepositorios;
using Infraestructura;
using Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;
using Tarea_en_Segundo_Plano;
using Tarea_en_Segundo_Plano.Data;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Registrar ApplicationDbContext con PostgreSQL
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnectionPosgre")));

        // Registrar las dependencias (repositorios)
        services.AddScoped<IJuegoRepository, JuegoRepository>();
        services.AddScoped<IGeneroRepository, GeneroRepository>();
        services.AddScoped<IPlataformaRepository, PlataformaRepository>();
        services.AddScoped<IClasificacionRepository, ClasificacionRepository>();
        services.AddScoped<IIdiomaRepository, IdiomaRepository>();

        // Registrar el DataSeeder
        services.AddTransient<DataSeeder>();

        // Registrar AutoMapper
        services.AddAutoMapper(typeof(AutoMapperPerfile));

        // Registrar el Worker como HostedService
        //services.AddSingleton<Worker>();

        // Registrar Worker como HostedService (esto es necesario para que se ejecute en segundo plano)
        services.AddHostedService<Worker>();

    })
    .Build();

await host.RunAsync();


