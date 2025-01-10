using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class ImagenDTO
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int JuegoId { get; set; }
    }
}
