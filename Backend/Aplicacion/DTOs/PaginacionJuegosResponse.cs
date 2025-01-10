using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class PaginacionJuegosResponse
    {
        public IEnumerable<JuegoDTO> Juegos { get; set; }
        public int TotalJuegos { get; set; }
        public int? NextCursor { get; set; }
    }
}
