using Aplicacion.DTOs;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura
{
    // El DbContext es la clase que representa la sesión con la base de datos y se utiliza para consultar y guardar datos.
    public class ApplicationDbContext : DbContext
    {
        // Constructor del DbContext para configurar la conexión a la base de datos.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // DbSets que representan las tablas en la base de datos.
        public DbSet<Juego> Juegos { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Plataforma> Plataformas { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Idioma> Idiomas { get; set; }
        public DbSet<Clasificacion> Clasificaciones { get; set; }
        public DbSet<Usuario> Usuarios { get; set; } 

        // Método que se ejecuta cuando se está creando el modelo. Aquí definimos relaciones, restricciones, etc.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relación muchos a muchos entre Juego y Genero
            modelBuilder.Entity<Juego>()
                .HasMany(j => j.Generos) // Un juego tiene muchos géneros
                .WithMany(g => g.Juegos) // Un género pertenece a muchos juegos
                .UsingEntity(jg => jg.ToTable("JuegoGenero")); // Define la tabla intermedia y su nombre

            // Relación muchos a muchos entre Juego y Plataforma
            modelBuilder.Entity<Juego>()
                .HasMany(j => j.Plataformas) // Un juego tiene muchas plataformas
                .WithMany(p => p.Juegos) // Una plataforma soporta muchos juegos
                .UsingEntity(jp => jp.ToTable("JuegoPlataforma")); // Define la tabla intermedia y su nombre

            // Relación muchos a muchos entre Juego e Idioma
            modelBuilder.Entity<Juego>()
                .HasMany(j => j.Idiomas) // Un juego tiene muchos idiomas
                .WithMany(p => p.Juegos) // Una idioma esta en muchos juegos
                .UsingEntity(jp => jp.ToTable("JuegoIdioma")); // Define la tabla intermedia y su nombre
            
            // Relación uno a muchos entre Juego y Clasificación
            modelBuilder.Entity<Juego>()
                .HasOne(j => j.Clasificacion)  // Un juego tiene una clasificación
                .WithMany(c => c.Juegos)       // Una clasificación tiene muchos juegos
                .HasForeignKey(j => j.ClasificacionId);  // La clave foránea en Juego

            // Relación uno a muchos entre Juego y Imagen
            modelBuilder.Entity<Juego>()
                .HasMany(j => j.Imagenes) // Un juego tiene muchas imágenes
                .WithOne(i => i.Juego) // Cada imagen pertenece a un solo juego
                .HasForeignKey(i => i.JuegoId) // La clave foránea en la tabla de imágenes apunta a la tabla de juegos
                .OnDelete(DeleteBehavior.Cascade); // Configura el borrado en cascada

            // Relación uno a muchos entre Juego y Videos
            modelBuilder.Entity<Juego>()
                .HasMany(j => j.Videos) // Un juego tiene muchas videos
                .WithOne(i => i.Juego) // Cada video pertenece a un solo juego
                .HasForeignKey(i => i.JuegoId) // La clave foránea en la tabla de videos apunta a la tabla de juegos
                .IsRequired(false)  // Permitir que sea opcional
                .OnDelete(DeleteBehavior.Cascade); // Configura el borrado en cascada

            // Configuración de la tabla Usuarios
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(u => u.PasswordHash)
                    .IsRequired();

                entity.Property(u => u.ResetToken)
                    .IsRequired(false);

                entity.Property(u => u.TokenExpiracion)
                    .IsRequired(false);
            });


            /*modelBuilder.Entity<Usuario>()
                .ToTable("Usuarios")
                .HasKey(u => u.Id);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.PasswordHash)
                .IsRequired();

            modelBuilder.Entity<Usuario>()
                .Property(u => u.ResetToken)
                .IsRequired(false);

            modelBuilder.Entity<Usuario>()
                .Property(u => u.TokenExpiracion)
                .IsRequired(false);*/



            // Insertar datos de Genero
            modelBuilder.Entity<Genero>().HasData(
                new Genero { Id = 1, Nombre = "Accion" },
                new Genero { Id = 2, Nombre = "Aventura" },
                new Genero { Id = 3, Nombre = "Fantasia" },
                new Genero { Id = 4, Nombre = "Terror" },
                new Genero { Id = 5, Nombre = "Survival" },
                new Genero { Id = 6, Nombre = "Survival Horror" },
                new Genero { Id = 7, Nombre = "RPG" },
                new Genero { Id = 8, Nombre = "Shooter" },
                new Genero { Id = 9, Nombre = "Estrategia" },
                new Genero { Id = 10, Nombre = "Simulacion" },
                new Genero { Id = 11, Nombre = "Deportes" },
                new Genero { Id = 12, Nombre = "Carreras" },
                new Genero { Id = 13, Nombre = "Lucha" },
                new Genero { Id = 14, Nombre = "Plataformas" },
                new Genero { Id = 15, Nombre = "Puzle" },
                new Genero { Id = 16, Nombre = "Sandbox" },
                new Genero { Id = 17, Nombre = "Musica" },
                new Genero { Id = 18, Nombre = "Arcade" },
                new Genero { Id = 19, Nombre = "Indie" },
                new Genero { Id= 20, Nombre = "Multijugador" }
            );

            // Insertar datos de Plataforma
            modelBuilder.Entity<Plataforma>().HasData(
                new Plataforma { Id = 1, Nombre = "PlayStation" },
                new Plataforma { Id = 2, Nombre = "Xbox" },
                new Plataforma { Id = 3, Nombre = "Nintendo Switch" },
                new Plataforma { Id = 4, Nombre = "PC" },
                new Plataforma { Id= 5, Nombre = "Game Boy Advance" },
                new Plataforma { Id = 6, Nombre = "Android" },
                new Plataforma { Id = 7, Nombre = "iOS" },
                new Plataforma { Id = 8, Nombre = "Steam Deck" },
                new Plataforma { Id = 9, Nombre = "Google Stadia" }
            );

            // Insertar datos de Clasificaciones PEGI
            modelBuilder.Entity<Clasificacion>().HasData(
                new Clasificacion { Id = 1, Nombre = "PEGI 3", Descripcion = "Adecuado para todas las edades" },
                new Clasificacion { Id = 2, Nombre = "PEGI 7", Descripcion = "Adecuado para mayores de 7 años" },
                new Clasificacion { Id = 3, Nombre = "PEGI 12", Descripcion = "Adecuado para mayores de 12 años" },
                new Clasificacion { Id = 4, Nombre = "PEGI 16", Descripcion = "Adecuado para mayores de 16 años" },
                new Clasificacion { Id = 5, Nombre = "PEGI 18", Descripcion = "Adecuado para mayores de 18 años" }
            );

            // Insertar datos de Idiomas
            modelBuilder.Entity<Idioma>().HasData(
                new Idioma { Id = 1, Nombre = "Ingles" },
                new Idioma { Id = 2, Nombre = "Español" },
                new Idioma { Id = 3, Nombre = "Frances" },
                new Idioma { Id = 4, Nombre = "Aleman" },
                new Idioma { Id = 5, Nombre = "Italiano" },
                new Idioma { Id = 6, Nombre = "Portugues" },
                new Idioma { Id = 7, Nombre = "Ruso" },
                new Idioma { Id = 8, Nombre = "Japones" },
                new Idioma { Id = 9, Nombre = "Chino" } 
            );
        }
    }
}
