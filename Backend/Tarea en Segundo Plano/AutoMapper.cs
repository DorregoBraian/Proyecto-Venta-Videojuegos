using Aplicacion.DTOs;
using AutoMapper;
using Dominio.Entidades;

namespace API_Rest_de_Videos_Juegos
{
    public class AutoMapperPerfile : Profile
    {
        public AutoMapperPerfile()
        {
            // Mapeo de JuegoJsonModel a Juego
            CreateMap<JuegoJsonModel, Juego>()
                .ForMember(dest => dest.Generos, opt => opt.Ignore()) // Se manejará manualmente
                .ForMember(dest => dest.Plataformas, opt => opt.Ignore()) // Se manejará manualmente
                .ForMember(dest => dest.Idiomas, opt => opt.Ignore()) // Se manejará manualmente
                .ForMember(dest => dest.Imagenes, opt => opt.MapFrom(src => src.Imagenes.Select(url => new Imagen { Url = url })))
                .ForMember(dest => dest.Videos, opt => opt.MapFrom(src => src.Videos.Select(url => new Video { Url = url })))
                .ForMember(dest => dest.Clasificacion, opt => opt.MapFrom(src => ObtenerClasificacionPorNombre(src.Clasificacion)));
        }
        private Clasificacion ObtenerClasificacionPorNombre(string nombreClasificacion)
        {
            // Aquí puedes tener lógica para obtener la clasificación por nombre
            return nombreClasificacion switch
            {
                "PEGI 3" => new Clasificacion { Id = 1, Nombre = "PEGI 3" },
                "PEGI 7" => new Clasificacion { Id = 2, Nombre = "PEGI 7" },
                "PEGI 12" => new Clasificacion { Id = 3, Nombre = "PEGI 12" },
                "PEGI 16" => new Clasificacion { Id = 4, Nombre = "PEGI 16" },
                "PEGI 18" => new Clasificacion { Id = 5, Nombre = "PEGI 18" },
                _ => throw new ArgumentException($"El valor '{nombreClasificacion}' no tiene un caso definido en el switch.")
            };
        }
    }
}
