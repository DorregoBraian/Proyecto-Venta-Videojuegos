using Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.IServicios
{
    public interface IPlataformaService
    {
        Task<IEnumerable<PlataformaDTO>> GetAllPlataformasAsync();
        Task<PlataformaDTO> GetPlataformaByIdAsync(int id);
        Task<PlataformaDTO> GetPlataformaByNombreAsync(string nombre);
        Task<PlataformaDTO> CreatePlataformaAsync(PlataformaDTO plataformaDto);
        Task<PlataformaDTO> UpdatePlataformaByNombreAsync(string nombre, PlataformaDTO plataformaDto);
        Task<PlataformaDTO> UpdatePlataformaByIdAsync(int id, PlataformaDTO plataformaDto);
        Task<PlataformaDTO> DeletePlataformaByNombreAsync(string nombre);
        Task<PlataformaDTO> DeletePlataformaByIdAsync(int id);
    }
}
