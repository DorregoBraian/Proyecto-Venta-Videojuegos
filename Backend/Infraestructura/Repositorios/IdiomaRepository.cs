using Dominio.Entidades;
using Dominio.IRepositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Repositorios
{
    public class IdiomaRepository : IIdiomaRepository
    {
        private readonly ApplicationDbContext _context;

        public IdiomaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtiene todos los idiomas
        public async Task<IEnumerable<Idioma>> ReadAllIdiomasAsync()
        {
            // Devuelve una lista con todos los idiomas
            return await _context.Idiomas.ToListAsync();
        }

        // Obtiene un idioma por su ID
        public async Task<Idioma> ReadIdiomaByIdAsync(int id)
        {
            // Devuelve el primer registro que coincide con el ID
            return await _context.Idiomas.FindAsync(id);
        }

        // Obtiene un idioma por su nombre
        public async Task<Idioma> ReadIdiomaByNameAsync(string nombre)
        {
            // Devuelve el primer registro que coincide con el nombre
            return await _context.Idiomas.FirstOrDefaultAsync(i => i.Nombre.ToLower() == nombre.ToLower());
        }

        // Crea un nuevo idioma
        public async Task CreateIdiomaAsync(Idioma idioma)
        {
            await _context.Idiomas.AddAsync(idioma); // Agrega el nuevo idioma al contexto
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
        }

        // Actualiza un idioma existente
        public async Task UpdateIdiomaAsync(Idioma idioma)
        {
            _context.Idiomas.Update(idioma); // Actualiza el idioma en el contexto
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
        }

        // Elimina un idioma por su ID
        public async Task DeleteIdiomaAsync(int id)
        {
            var idioma = await ReadIdiomaByIdAsync(id); // Busca el idioma por ID
            if (idioma != null)
            {
                _context.Idiomas.Remove(idioma); // Elimina el idioma si existe
                await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
            }
        }
    }
}
