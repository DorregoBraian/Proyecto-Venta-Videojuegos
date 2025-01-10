using Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.IServicios
{
    public interface IGeneroService
    {
        Task<IEnumerable<GeneroDTO>> GetAllGenerosAsync();
        Task<GeneroDTO> GetGeneroByIdAsync(int id);
        Task<GeneroDTO> GetGeneroByNombreAsync(string nombre);
        Task<GeneroDTO> CreateGeneroAsync(GeneroDTO generoDto);
        Task<GeneroDTO> UpdateGeneroByNombreAsync(string nombre, GeneroDTO generoDto);
        Task<GeneroDTO> UpdateGeneroByIdAsync(int id, GeneroDTO generoDto);
        Task<GeneroDTO> DeleteGeneroByNombreAsync(string nombre);
        Task<GeneroDTO> DeleteGeneroByIdAsync(int id);

    }
}