using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class FiltroJuegosDto
    {
        public string? Genero { get; set; }
        public string? Idioma { get; set; }
        public string? Plataforma { get; set; }
        public string? Clasificacion { get; set; }
    }

}
