using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    public class JuegoJsonModel
    {
        public string Titulo { get; set; }
        public decimal Precio { get; set; }
        public string Portada { get; set; }
        public string Descripcion { get; set; }
        public string Desarrollador { get; set; }
        public string Editor { get; set; }
        public string Clasificacion { get; set; }
        public string FechaDeLanzamiento { get; set; }
        public List<string> Idioma { get; set; }
        public List<string> Generos { get; set; }
        public List<string> Plataformas { get; set; }
        public List<string> Imagenes { get; set; }
        public List<string> Videos { get; set; }
    }
}
