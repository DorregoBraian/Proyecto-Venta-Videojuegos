using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Idioma
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Relación con Juegos
        public ICollection<Juego> Juegos { get; set; }

    }
}
