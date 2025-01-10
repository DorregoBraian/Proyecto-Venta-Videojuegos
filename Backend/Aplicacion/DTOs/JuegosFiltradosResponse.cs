using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class JuegosFiltradosResponse
    {
        public IEnumerable<JuegoDTO> Juegos { get; set; }
        public int TotalJuegos { get; set; }
    }
}
