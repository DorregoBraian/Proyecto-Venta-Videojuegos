using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.IRepositorios
{
    public interface IGeneroRepository
    {
        Task<IEnumerable<Genero>> ReadAllGenerosAsync(); 
        Task<Genero> ReadGeneroByIdAsync(int id);
        Task<Genero> ReadGeneroByNameAsync(string nombre); 
        Task CreateGeneroAsync(Genero genero);
        Task UpdateGeneroAsync(Genero genero); 
        Task DeleteGeneroAsync(int id); 
    }
}
