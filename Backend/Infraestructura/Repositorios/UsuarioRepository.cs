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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtengo un usuaio por su Email
        public async Task<Usuario> ObtenerPorEmailAsync(string email)
        {
            // Devuelve el primer registro que coincide con el email
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        // Obtengo un usuaio por su ID
        public async Task<Usuario> ObtenerPorIdAsync(int id)
        {
            // Devuelve el primer registro que coincide con el ID
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario> ObtenerPorTokenAsync(string token)
        {
            var decodedToken = Uri.UnescapeDataString(token); // Decodificar el token recibido
            // Devuelve el primer registro que coincide con el token y que la fecha no alla expirado
            return await _context.Usuarios.FirstOrDefaultAsync(u => 
            u.ResetToken == decodedToken && u.TokenExpiracion > DateTime.UtcNow);
        }

        // Crea un nueva usuario
        public async Task AgregarAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        // Actualiza un usuario existente
        public async Task ActualizarAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        // Verifico si el email existe
        public async Task<bool> EmailExisteAsync(string email)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email == email);
        }
    }
}
