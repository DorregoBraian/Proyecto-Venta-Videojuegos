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
    public class PlataformaRepository : IPlataformaRepository
    {
        private readonly ApplicationDbContext _context;

        public PlataformaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtiene todas las plataformas
        public async Task<IEnumerable<Plataforma>> ReadAllPlataformasAsync()
        {
            // Devuelve una lista con todos las plataformas
            return await _context.Plataformas.ToListAsync();
        }

        // Obtiene una plataforma por su ID
        public async Task<Plataforma> ReadPlataformaByIdAsync(int id)
        {
            // Devuelve el primer registro que coincide con el ID
            return await _context.Plataformas.FindAsync(id);
        }

        // Obtiene una plataforma por su nombre
        public async Task<Plataforma> ReadPlataformaByNameAsync(string nombre)
        {
            // Devuelve el primer registro que coincide con el nombre
            return await _context.Plataformas.FirstOrDefaultAsync(p => p.Nombre.ToLower() == nombre.ToLower());
        }

        // Crea una nueva plataforma
        public async Task CreatePlataformaAsync(Plataforma plataforma)
        {
            await _context.Plataformas.AddAsync(plataforma); // Agrega la nueva plataforma al contexto
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
        }

        // Actualiza una plataforma existente
        public async Task UpdatePlataformaAsync(Plataforma plataforma)
        {
            _context.Plataformas.Update(plataforma); // Actualiza la plataforma en el contexto
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
        }

        // Elimina una plataforma por su ID
        public async Task DeletePlataformaAsync(int id)
        {
            var plataforma = await ReadPlataformaByIdAsync(id); // Busca la plataforma por ID
            if (plataforma != null)
            {
                _context.Plataformas.Remove(plataforma); // Elimina la plataforma si existe
                await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
            }
        }
    }
}
