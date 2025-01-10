using Dominio.Entidades;
using Dominio.IRepositorios;
using Microsoft.EntityFrameworkCore;


namespace Infraestructura.Repositorios
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly ApplicationDbContext _context;

        public GeneroRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtiene todos los géneros
        public async Task<IEnumerable<Genero>> ReadAllGenerosAsync()
        {
            // Devuelve una lista de todos los géneros
            return await _context.Generos.ToListAsync();
        }

        // Devuelve el primer registro que coincide con el ID
        public async Task<Genero> ReadGeneroByIdAsync(int id)
        {
            return await _context.Generos.FindAsync(id);
        }

        // Obtiene un género por su nombre
        public async Task<Genero> ReadGeneroByNameAsync(string nombre)
        {
            //Devuelve el primer registro que coincide con el nombre
            return await _context.Generos.FirstOrDefaultAsync(g => g.Nombre.ToLower() == nombre.ToLower());
        }

        // Crea un nuevo género
        public async Task CreateGeneroAsync(Genero genero)
        {
            await _context.Generos.AddAsync(genero); // Agrega el nuevo género al contexto
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
        }

        // Actualiza un género existente
        public async Task UpdateGeneroAsync(Genero genero)
        {
            _context.Generos.Update(genero); // Actualiza el género en el contexto
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
        }

        // Elimina un género por su ID
        public async Task DeleteGeneroAsync(int id)
        {
            var genero = await ReadGeneroByIdAsync(id); // Busca el género por ID
            if (genero != null)
            {
                _context.Generos.Remove(genero); // Elimina el género si existe
                await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
            }
        }
    }
}
