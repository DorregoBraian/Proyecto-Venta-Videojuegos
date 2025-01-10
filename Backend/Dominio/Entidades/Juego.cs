using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class Juego
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Portada { get; set; }
        public int Precio { get; set; }
        public string Descripcion { get; set; }
        public string Desarrollador { get; set; }
        public string Editor { get; set; }
        public int ClasificacionId { get; set; }  // Clave foránea hacia Clasificacion
        public Clasificacion Clasificacion { get; set; }// Relación con Clasificacion
        public DateTime FechaDeLanzamiento { get; set; }
        public ICollection<Idioma> Idiomas { get; set; } // Relación con Idioma
        public ICollection<Genero> Generos { get; set; } // Relación con Géneros
        public ICollection<Plataforma> Plataformas { get; set; } // Relación con Plataformas
        public ICollection<Imagen> Imagenes { get; set; } // Relación con Imágenes
        public ICollection<Video> Videos { get; set; } // Relación con Imágenes
    }
}
