using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class JuegoDTO
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public int Precio { get; set; }

        public string Portada { get; set; }

        public string Descripcion { get; set; }

        public string Desarrollador { get; set; }

        public string Editor { get; set; }

        public string Clasificacion { get; set; }

        public DateTime FechaDeLanzamiento { get; set; }

        // Colecciones de nombres en lugar de IDs
        public ICollection<string> Generos { get; set; }  // Nombres de géneros

        public ICollection<string> Plataformas { get; set; }  // Nombres de plataformas

        public ICollection<string> Imagenes { get; set; }  // URLs o nombres de imágenes

        public ICollection<string> Videos { get; set; }  // URLs o nombres de videos

        public ICollection<string> Idiomas { get; set; }  // Nombres de idiomas

    }
}
