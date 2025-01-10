using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.IRepositorios
{
    public interface IIdiomaRepository
    {
        Task<IEnumerable<Idioma>> ReadAllIdiomasAsync();
        Task<Idioma> ReadIdiomaByIdAsync(int id);
        Task<Idioma> ReadIdiomaByNameAsync(string nombre);
        Task CreateIdiomaAsync(Idioma idioma);
        Task UpdateIdiomaAsync(Idioma idioma);
        Task DeleteIdiomaAsync(int id);
    }
}
