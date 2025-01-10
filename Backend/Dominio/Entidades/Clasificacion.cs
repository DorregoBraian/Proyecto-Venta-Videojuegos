using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Clasificacion
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } 
        public string Descripcion { get; set; }

        // Clave foránea para Juego
        // Relación uno a muchos con Juego (una clasificación puede estar en varios juegos)
        public ICollection<Juego> Juegos { get; set; }
    }
}
