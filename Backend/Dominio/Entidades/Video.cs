using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Video
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int JuegoId { get; set; }

        // Relación con Juego
        public Juego Juego { get; set; }
    }
}
