﻿// <auto-generated />
using System;
using Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infraestructura.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Dominio.Entidades.Clasificacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Clasificaciones");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descripcion = "Adecuado para todas las edades",
                            Nombre = "PEGI 3"
                        },
                        new
                        {
                            Id = 2,
                            Descripcion = "Adecuado para mayores de 7 años",
                            Nombre = "PEGI 7"
                        },
                        new
                        {
                            Id = 3,
                            Descripcion = "Adecuado para mayores de 12 años",
                            Nombre = "PEGI 12"
                        },
                        new
                        {
                            Id = 4,
                            Descripcion = "Adecuado para mayores de 16 años",
                            Nombre = "PEGI 16"
                        },
                        new
                        {
                            Id = 5,
                            Descripcion = "Adecuado para mayores de 18 años",
                            Nombre = "PEGI 18"
                        });
                });

            modelBuilder.Entity("Dominio.Entidades.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Generos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "Accion"
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "Aventura"
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "Fantasia"
                        },
                        new
                        {
                            Id = 4,
                            Nombre = "Terror"
                        },
                        new
                        {
                            Id = 5,
                            Nombre = "Survival"
                        },
                        new
                        {
                            Id = 6,
                            Nombre = "Survival Horror"
                        },
                        new
                        {
                            Id = 7,
                            Nombre = "RPG"
                        },
                        new
                        {
                            Id = 8,
                            Nombre = "Shooter"
                        },
                        new
                        {
                            Id = 9,
                            Nombre = "Estrategia"
                        },
                        new
                        {
                            Id = 10,
                            Nombre = "Simulacion"
                        },
                        new
                        {
                            Id = 11,
                            Nombre = "Deportes"
                        },
                        new
                        {
                            Id = 12,
                            Nombre = "Carreras"
                        },
                        new
                        {
                            Id = 13,
                            Nombre = "Lucha"
                        },
                        new
                        {
                            Id = 14,
                            Nombre = "Plataformas"
                        },
                        new
                        {
                            Id = 15,
                            Nombre = "Puzle"
                        },
                        new
                        {
                            Id = 16,
                            Nombre = "Sandbox"
                        },
                        new
                        {
                            Id = 17,
                            Nombre = "Musica"
                        },
                        new
                        {
                            Id = 18,
                            Nombre = "Arcade"
                        },
                        new
                        {
                            Id = 19,
                            Nombre = "Indie"
                        },
                        new
                        {
                            Id = 20,
                            Nombre = "Multijugador"
                        });
                });

            modelBuilder.Entity("Dominio.Entidades.Idioma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Idiomas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "Ingles"
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "Español"
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "Frances"
                        },
                        new
                        {
                            Id = 4,
                            Nombre = "Aleman"
                        },
                        new
                        {
                            Id = 5,
                            Nombre = "Italiano"
                        },
                        new
                        {
                            Id = 6,
                            Nombre = "Portugues"
                        },
                        new
                        {
                            Id = 7,
                            Nombre = "Ruso"
                        },
                        new
                        {
                            Id = 8,
                            Nombre = "Japones"
                        },
                        new
                        {
                            Id = 9,
                            Nombre = "Chino"
                        });
                });

            modelBuilder.Entity("Dominio.Entidades.Imagen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("JuegoId")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("JuegoId");

                    b.ToTable("Imagenes");
                });

            modelBuilder.Entity("Dominio.Entidades.Juego", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClasificacionId")
                        .HasColumnType("integer");

                    b.Property<string>("Desarrollador")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Editor")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaDeLanzamiento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Portada")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Precio")
                        .HasColumnType("integer");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClasificacionId");

                    b.ToTable("Juegos");
                });

            modelBuilder.Entity("Dominio.Entidades.Plataforma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Plataformas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Nombre = "PlayStation"
                        },
                        new
                        {
                            Id = 2,
                            Nombre = "Xbox"
                        },
                        new
                        {
                            Id = 3,
                            Nombre = "Nintendo Switch"
                        },
                        new
                        {
                            Id = 4,
                            Nombre = "PC"
                        },
                        new
                        {
                            Id = 5,
                            Nombre = "Game Boy Advance"
                        },
                        new
                        {
                            Id = 6,
                            Nombre = "Android"
                        },
                        new
                        {
                            Id = 7,
                            Nombre = "iOS"
                        },
                        new
                        {
                            Id = 8,
                            Nombre = "Steam Deck"
                        },
                        new
                        {
                            Id = 9,
                            Nombre = "Google Stadia"
                        });
                });

            modelBuilder.Entity("Dominio.Entidades.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ResetToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("TokenExpiracion")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Usuarios", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("JuegoId")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("JuegoId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("GeneroJuego", b =>
                {
                    b.Property<int>("GenerosId")
                        .HasColumnType("integer");

                    b.Property<int>("JuegosId")
                        .HasColumnType("integer");

                    b.HasKey("GenerosId", "JuegosId");

                    b.HasIndex("JuegosId");

                    b.ToTable("JuegoGenero", (string)null);
                });

            modelBuilder.Entity("IdiomaJuego", b =>
                {
                    b.Property<int>("IdiomasId")
                        .HasColumnType("integer");

                    b.Property<int>("JuegosId")
                        .HasColumnType("integer");

                    b.HasKey("IdiomasId", "JuegosId");

                    b.HasIndex("JuegosId");

                    b.ToTable("JuegoIdioma", (string)null);
                });

            modelBuilder.Entity("JuegoPlataforma", b =>
                {
                    b.Property<int>("JuegosId")
                        .HasColumnType("integer");

                    b.Property<int>("PlataformasId")
                        .HasColumnType("integer");

                    b.HasKey("JuegosId", "PlataformasId");

                    b.HasIndex("PlataformasId");

                    b.ToTable("JuegoPlataforma", (string)null);
                });

            modelBuilder.Entity("Dominio.Entidades.Imagen", b =>
                {
                    b.HasOne("Dominio.Entidades.Juego", "Juego")
                        .WithMany("Imagenes")
                        .HasForeignKey("JuegoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Juego");
                });

            modelBuilder.Entity("Dominio.Entidades.Juego", b =>
                {
                    b.HasOne("Dominio.Entidades.Clasificacion", "Clasificacion")
                        .WithMany("Juegos")
                        .HasForeignKey("ClasificacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clasificacion");
                });

            modelBuilder.Entity("Dominio.Entidades.Video", b =>
                {
                    b.HasOne("Dominio.Entidades.Juego", "Juego")
                        .WithMany("Videos")
                        .HasForeignKey("JuegoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Juego");
                });

            modelBuilder.Entity("GeneroJuego", b =>
                {
                    b.HasOne("Dominio.Entidades.Genero", null)
                        .WithMany()
                        .HasForeignKey("GenerosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entidades.Juego", null)
                        .WithMany()
                        .HasForeignKey("JuegosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IdiomaJuego", b =>
                {
                    b.HasOne("Dominio.Entidades.Idioma", null)
                        .WithMany()
                        .HasForeignKey("IdiomasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entidades.Juego", null)
                        .WithMany()
                        .HasForeignKey("JuegosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JuegoPlataforma", b =>
                {
                    b.HasOne("Dominio.Entidades.Juego", null)
                        .WithMany()
                        .HasForeignKey("JuegosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dominio.Entidades.Plataforma", null)
                        .WithMany()
                        .HasForeignKey("PlataformasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Dominio.Entidades.Clasificacion", b =>
                {
                    b.Navigation("Juegos");
                });

            modelBuilder.Entity("Dominio.Entidades.Juego", b =>
                {
                    b.Navigation("Imagenes");

                    b.Navigation("Videos");
                });
#pragma warning restore 612, 618
        }
    }
}
