using API_Rest_de_Videos_Juegos;
using Aplicacion.IServicios;
using Aplicacion.Servicio;
using Dominio.IRepositorios;
using Dominio.IServicios;
using Infraestructura;
using Infraestructura.Repositorios;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Habilitar User Secrets (en Render las credenciales se pasan por variables de entorno)
builder.Configuration.AddUserSecrets<Program>();

// Agrego la Base de datos (ConnectionString de PostgreSQL / Neon)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionPosgre");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Registrar servicios y repositorios

// --------------- Juego ----------------
builder.Services.AddScoped<IJuegoService, JuegoService>();
builder.Services.AddScoped<IJuegoRepository, JuegoRepository>();

// --------------- Genero ---------------
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();

// --------------- Plataforma -----------
builder.Services.AddScoped<IPlataformaService, PlataformaService>();
builder.Services.AddScoped<IPlataformaRepository, PlataformaRepository>();

// --------------- Clasificacion ---------------
builder.Services.AddScoped<IClasificacionService, ClasificacionService>();
builder.Services.AddScoped<IClasificacionRepository, ClasificacionRepository>();

// --------------- Idioma ---------------
builder.Services.AddScoped<IIdiomaService, IdiomaService>();
builder.Services.AddScoped<IIdiomaRepository, IdiomaRepository>();

// --------------- Usuario ---------------
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// Agrego el AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperPerfile).Assembly);

// Configurar controladores
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
});

// Render termina el TLS en su proxy, por lo que la app recibe HTTP.
// Con esto ASP.NET Core respeta X-Forwarded-Proto y evita bucles de redirección.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

var app = builder.Build();

// Debe ir ANTES de UseHttpsRedirection
app.UseForwardedHeaders();

// Swagger (lo dejamos activo también en producción para mostrar la API)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

// Usar CORS en la aplicación
app.UseCors("AllowAll");

app.MapControllers();

// Health check para Render
app.MapGet("/health", () => Results.Ok(new { status = "ok", time = DateTimeOffset.UtcNow }));

// Aplicar migraciones al arrancar solo si se indica por configuración
// (variable de entorno: RunMigrationsOnStartup=true)
if (app.Configuration.GetValue<bool>("RunMigrationsOnStartup"))
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();
