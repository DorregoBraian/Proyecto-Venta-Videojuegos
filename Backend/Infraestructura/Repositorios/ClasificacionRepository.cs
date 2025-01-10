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
    public class ClasificacionRepository : IClasificacionRepository
    {
        private readonly ApplicationDbContext _context;

        public ClasificacionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtiene todas las Clasificaciones
        public async Task<IEnumerable<Clasificacion>> ReadAllClasificacionesAsync()
        {
            // Devuelve una lista con todas las Clasificaciones
            return await _context.Clasificaciones.ToListAsync();
        }

        // Obtiene una clasificación por su ID
        public async Task<Clasificacion> ReadClasificacionByIdAsync(int id)
        {
            // Devuelve el primer registro que coincide con el ID
            return await _context.Clasificaciones.FindAsync(id);
        }

        // Obtiene una clasificación por su nombre
        public async Task<Clasificacion> ReadClasificacionByNameAsync(string nombre)
        {
            // Devuelve el primer registro que coincide con el nombre
            return await _context.Clasificaciones.FirstOrDefaultAsync(c => c.Nombre.ToLower() == nombre.ToLower());
        }

        // Crea una nueva clasificación
        public async Task CreateClasificacionAsync(Clasificacion clasificacion)
        {
            await _context.Clasificaciones.AddAsync(clasificacion); // Agrega la nueva clasificación al contexto
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
        }

        // Actualiza una clasificación existente
        public async Task UpdateClasificacionAsync(Clasificacion clasificacion)
        {
            _context.Clasificaciones.Update(clasificacion); // Actualiza la clasificación en el contexto
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
        }

        // Elimina una clasificación por su ID
        public async Task DeleteClasificacionAsync(int id)
        {
            var clasificacion = await ReadClasificacionByIdAsync(id); // Busca la clasificación por ID
            if (clasificacion != null)
            {
                _context.Clasificaciones.Remove(clasificacion); // Elimina la clasificación si existe
                await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos
            }
        }
    }
}
