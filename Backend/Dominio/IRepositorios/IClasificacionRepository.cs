using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.IRepositorios
{
    public interface IClasificacionRepository
    {
        Task<IEnumerable<Clasificacion>> ReadAllClasificacionesAsync();
        Task<Clasificacion> ReadClasificacionByIdAsync(int id);
        Task<Clasificacion> ReadClasificacionByNameAsync(string nombre);
        Task CreateClasificacionAsync(Clasificacion clasificacion);
        Task UpdateClasificacionAsync(Clasificacion clasificacion);
        Task DeleteClasificacionAsync(int id);
    }
}
