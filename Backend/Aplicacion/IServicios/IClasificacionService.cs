using Aplicacion.DTOs;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.IServicios
{
    public interface IClasificacionService
    {
        Task<IEnumerable<ClasificacionDTO>> GetAllClasificacionesAsync();
        Task<ClasificacionDTO> GetClasificacionByIdAsync(int id);
        Task<ClasificacionDTO> GetClasificacionByNombreAsync(string nombre);
        Task<ClasificacionDTO> CreateClasificacionAsync(ClasificacionDTO clasificacionDto);
        Task<ClasificacionDTO> UpdateClasificacionByIdAsync(int id, ClasificacionDTO clasificacionDto);
        Task<ClasificacionDTO> UpdateClasificacionByNombreAsync(string nombre, ClasificacionDTO clasificacionDto);
        Task<ClasificacionDTO> DeleteClasificacionByIdAsync(int id);
        Task<ClasificacionDTO> DeleteClasificacionByNombreAsync(string nombre);
    }
}
