using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Usuario
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } 
        public string Email { get; set; }
        public string PasswordHash { get; set; } 
        public string? ResetToken { get; set; }
        public DateTime? TokenExpiracion { get; set; }
    }
}
