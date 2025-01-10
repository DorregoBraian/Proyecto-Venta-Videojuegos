using API_Rest_de_Videos_Juegos;
using Aplicacion.IServicios;
using Aplicacion.Servicio;
using Dominio.IRepositorios;
using Dominio.IServicios;
using Infraestructura;
using Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Agrego la Base de datos (ConnectionString)
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionSQLServer");
// para SQL Serve
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
    //options.UseSqlServer(connectionString));

// Para Postgres
var connectionString2 = builder.Configuration.GetConnectionString("DefaultConnectionPosgre");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString2));


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

// --------------- Imagen ---------------
builder.Services.AddScoped<IIdiomaService, IdiomaService>();
builder.Services.AddScoped<IIdiomaRepository, IdiomaRepository>();

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

// Habilitar User Secrets
builder.Configuration.AddUserSecrets<Program>();

// Configurar controladores
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Usar CORS en la aplicación
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
