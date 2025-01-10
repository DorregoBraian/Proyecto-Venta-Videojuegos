using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.IRepositorios
{
    public interface IPlataformaRepository
    {
        Task<IEnumerable<Plataforma>> ReadAllPlataformasAsync();
        Task<Plataforma> ReadPlataformaByIdAsync(int id); 
        Task<Plataforma> ReadPlataformaByNameAsync(string nombre); 
        Task CreatePlataformaAsync(Plataforma plataforma); 
        Task UpdatePlataformaAsync(Plataforma plataforma);
        Task DeletePlataformaAsync(int id); 
    }
}
