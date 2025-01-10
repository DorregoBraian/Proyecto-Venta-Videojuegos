using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.IRepositorios
{
    public interface IJuegoRepository
    {
        Task<Juego> ReadGameByIdAsync(int id);
        Task<IEnumerable<Juego>> ReadGameByNameAsync(string nombre);
        Task<IEnumerable<Juego>> ReadAllGameAsync();
        Task<Juego> CreateGameAsync(Juego juego);
        Task<Juego> UpdateGameAsync(Juego juego);
        Task DeleteGameByIdAsync(int id);
        Task<(IEnumerable<Juego> juegos, int totalJuegos)> GetJuegosPaginadosAsync(int cursor, int limit);

        // Filtros
        Task<IEnumerable<Juego>> GetJuegosByGeneroAsync(string genero);
        Task<IEnumerable<Juego>> GetJuegosByPlataformaAsync(string plataforma);
        Task<IEnumerable<Juego>> GetJuegosByIdiomaAsync(string idioma);
        Task<IEnumerable<Juego>> GetJuegosByClasificacionAsync(string clasificacion);
        Task<(IEnumerable<Juego> juegos, int totalJuegos)> GetJuegosFiltradosAsync(string idioma, string plataforma, string genero, string clasificacion, int page, int pageSize);
    }
}
