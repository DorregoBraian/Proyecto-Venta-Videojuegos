using Dominio.Entidades;
using Dominio.IRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Repositorios
{
    public class JuegoRepository : IJuegoRepository
    {
        private readonly ApplicationDbContext _context;

        public JuegoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtiene un juego por su ID 
        public async Task<Juego> ReadGameByIdAsync(int id)
        {
            // Devuelve el primer registro que coincide con el ID incluyendo las relaciones
            return await _context.Juegos
                .Include(j => j.Generos)
                .Include(j => j.Plataformas)
                .Include(j => j.Clasificacion)
                .Include(j => j.Imagenes)
                .Include(j => j.Videos)
                .Include(j => j.Idiomas)
                .FirstOrDefaultAsync(j => j.Id == id);
        }

        // Obtiene un juego por su nombre
        public async Task<IEnumerable<Juego>> ReadGameByNameAsync(string nombre)
        {
            // Devuelve el primer registro que coincide con el nombre incluyendo las relaciones
            return await _context.Juegos
                .Include(j => j.Generos)
                .Include(j => j.Plataformas)
                .Include(j => j.Clasificacion)
                .Include(j => j.Imagenes)
                .Include(j => j.Videos)
                .Include(j => j.Idiomas)
                .Where(j => EF.Functions.Like(j.Titulo.ToLower(), $"%{nombre}%")) // Coincidencia parcial
                .ToListAsync();
        }

        // Obtiene todos los juegos incluyendo las relaciones
        public async Task<IEnumerable<Juego>> ReadAllGameAsync()
        {
            // Devuelve una lista de juegos incluyendo las relaciones
            return await _context.Juegos
                .Include(j => j.Generos)
                .Include(j => j.Plataformas)
                .Include(j => j.Clasificacion)
                .Include(j => j.Imagenes)
                .Include(j => j.Videos)
                .Include(j => j.Idiomas)
                .ToListAsync();
        }

        // Crea un nuevo juego
        public async Task<Juego> CreateGameAsync(Juego juego)
        {
            await _context.Juegos.AddAsync(juego);
            await _context.SaveChangesAsync();
            return juego;
        }

        // Actualiza un juego existente
        public async Task<Juego> UpdateGameAsync(Juego juego)
        {
            _context.Juegos.Update(juego);
            await _context.SaveChangesAsync();
            return juego;
        }

        // Elimina un juego por su ID
        public async Task DeleteGameByIdAsync(int id)
        {
            var juego = await ReadGameByIdAsync(id);
            if (juego != null)
            {
                _context.Juegos.Remove(juego);
                await _context.SaveChangesAsync();
            }
        }

        // Metodo de Paguinacion
        public async Task<(IEnumerable<Juego> juegos,int totalJuegos)> GetJuegosPaginadosAsync(int cursor, int limit)
        {
            var query = _context.Juegos
                .Include(j => j.Generos)
                .Include(j => j.Plataformas)
                .Include(j => j.Clasificacion)
                .Include(j => j.Imagenes)
                .Include(j => j.Videos)
                .Include(j => j.Idiomas)
                .AsQueryable();

            // Total de juegos (sin límite de paginación)
            int totalJuegos = await _context.Juegos.CountAsync();

            // Obtener la lista de juegos paginados
            var juegos = await query
                .OrderBy(j => j.Id) // Ordenar por Id
                .Skip((cursor - 1) * limit) // Salta los registros según la página.
                .Take(limit) // Salta los registros según la página.
                .ToListAsync(); // Convierte los resultados en una lista.

            return (juegos, totalJuegos);
        }


        // Obtener juegos filtrados por género
        public async Task<IEnumerable<Juego>> GetJuegosByGeneroAsync(string genero)
        {
            // Devuelve una lista de juegos segun el genero incluyendo las relaciones
            return await _context.Juegos
                .Include(j => j.Generos)
                .Include(j => j.Plataformas)
                .Include(j => j.Clasificacion)
                .Include(j => j.Imagenes)
                .Include(j => j.Videos)
                .Include(j => j.Idiomas)
                .Where(j => j.Generos.Any(g => g.Nombre == genero))
                .ToListAsync();
        }

        // Obtener juegos filtrados por plataforma
        public async Task<IEnumerable<Juego>> GetJuegosByPlataformaAsync(string plataforma)
        {
            // Devuelve una lista de juegos segun el plataforma incluyendo las relaciones
            return await _context.Juegos
                .Include(j => j.Generos)
                .Include(j => j.Plataformas)
                .Include(j => j.Clasificacion)
                .Include(j => j.Imagenes)
                .Include(j => j.Videos)
                .Include(j => j.Idiomas)
                .Where(j => j.Plataformas.Any(p => p.Nombre == plataforma))
                .ToListAsync();
        }

        // Obtener juegos filtrados por idioma
        public async Task<IEnumerable<Juego>> GetJuegosByIdiomaAsync(string idioma)
        {
            // Devuelve una lista de juegos segun el idioma incluyendo las relaciones
            return await _context.Juegos
                .Include(j => j.Generos)
                .Include(j => j.Plataformas)
                .Include(j => j.Clasificacion)
                .Include(j => j.Imagenes)
                .Include(j => j.Videos)
                .Include(j => j.Idiomas)
                .Where(j => j.Idiomas.Any(i => i.Nombre == idioma))
                .ToListAsync();
        }

        // Obtener juegos filtrados por clasificación
        public async Task<IEnumerable<Juego>> GetJuegosByClasificacionAsync(string clasificacion)
        {
            // Devuelve una lista de juegos segun el clasificación incluyendo las relaciones
            return await _context.Juegos
                .Include(j => j.Generos)
                .Include(j => j.Plataformas)
                .Include(j => j.Clasificacion)
                .Include(j => j.Imagenes)
                .Include(j => j.Videos)
                .Include(j => j.Idiomas)
                .Where(j => j.Clasificacion.Nombre == clasificacion)
                .ToListAsync();
        }

        // Obtener juegos filtrados por género,plataforma,idioma y clasificación paginados
        public async Task<(IEnumerable<Juego> juegos, int totalJuegos)> GetJuegosFiltradosAsync(string idioma, string plataforma, string genero, string clasificacion, int page, int pageSize)
        {
            var query = _context.Juegos
                .Include(j => j.Idiomas)
                .Include(j => j.Plataformas)
                .Include(j => j.Generos)
                .Include(j => j.Imagenes)
                .Include(j => j.Videos)
                .Include(j => j.Clasificacion)
                .AsQueryable();

            // Filtrar por Idioma
            if (!string.IsNullOrEmpty(idioma))
            {
                query = query.Where(j => j.Idiomas.Any(i => i.Nombre.ToLower() == idioma.ToLower()));
            }

            // Filtrar por Plataforma
            if (!string.IsNullOrEmpty(plataforma))
            {
                query = query.Where(j => j.Plataformas.Any(p => p.Nombre.ToLower() == plataforma.ToLower()));
            }

            // Filtrar por Género
            if (!string.IsNullOrEmpty(genero))
            {
                query = query.Where(j => j.Generos.Any(g => g.Nombre.ToLower() == genero.ToLower()));
            }

            // Filtro por clasificación
            if (!string.IsNullOrEmpty(clasificacion))
            {
                query = query.Where(j => j.Clasificacion.Nombre == clasificacion); 
            }

            // Calcular el total de elementos antes de aplicar paginación
            var totalJuegos = await query.CountAsync();

            // Obtener los elementos paginados
            var juegos = await query
                .OrderBy(j => j.Id)// Ordenar por Id
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (juegos,totalJuegos); // Devolver como una tupla


        }
    }
}
