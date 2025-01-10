using Aplicacion.DTOs;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.IServicios
{
    public interface IIdiomaService
    {
        Task<IEnumerable<IdiomaDTO>> GetAllIdiomasAsync();
        Task<IdiomaDTO> GetIdiomaByIdAsync(int id);
        Task<IdiomaDTO> GetIdiomaByNombreAsync(string nombre);
        Task<IdiomaDTO> CreateIdiomaAsync(IdiomaDTO idiomaDto);
        Task<IdiomaDTO> UpdateIdiomaByIdAsync(int id, IdiomaDTO idiomaDto);
        Task<IdiomaDTO> UpdateIdiomaByNombreAsync(string nombre, IdiomaDTO idiomaDto);
        Task<IdiomaDTO> DeleteIdiomaByIdAsync(int id);
        Task<IdiomaDTO> DeleteIdiomaByNombreAsync(string nombre);
    }

}
