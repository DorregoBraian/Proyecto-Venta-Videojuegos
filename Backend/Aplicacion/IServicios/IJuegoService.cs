using Aplicacion.DTOs;
using Dominio.Entidades;

namespace Dominio.IServicios
{
    public interface IJuegoService
    {
        Task<IEnumerable<JuegoDTO>> GetAllGamesAsync();
        Task<JuegoDTO> GetGameByIdAsync(int id);
        Task<IEnumerable<JuegoDTO>> GetGameByNameAsync(string nombre);
        Task<JuegoDTO> CreateGameAsync(CreateJuegoDTO juegoDto);
        Task<JuegoDTO> UpdateGameByIdAsync(int id, CreateJuegoDTO createJuegoDTO);
        Task<JuegoDTO> DeleteGameByIdAsync(int id);
        Task<(IEnumerable<JuegoDTO> juegos, int totalJuegos)> GetJuegosPaginadosAsync(int cursor, int limit);

        // Filtros
        Task<IEnumerable<JuegoDTO>> GetJuegosByGeneroAsync(string genero);
        Task<IEnumerable<JuegoDTO>> GetJuegosByPlataformaAsync(string plataforma);
        Task<IEnumerable<JuegoDTO>> GetJuegosByIdiomaAsync(string idioma);
        Task<IEnumerable<JuegoDTO>> GetJuegosByClasificacionAsync(string clasificacion);
        Task<(IEnumerable<JuegoDTO> Juegos, int totalJuegos)> GetJuegosFiltradosAsync(string idioma, string plataforma, string genero, string clasificacion, int page, int pageSize);
    }
}
