using Aplicacion.DTOs;
using AutoMapper;
using Dominio.Entidades;


namespace API_Rest_de_Videos_Juegos
{
    public class AutoMapperPerfile : Profile
    {
        public AutoMapperPerfile()
        {
            // Mapeo de Juego a JuegoDTO
            CreateMap<Juego, JuegoDTO>()
                .ForMember(dest => dest.Clasificacion, opt => opt.MapFrom(src => src.Clasificacion.Nombre))
                .ForMember(dest => dest.Generos, opt => opt.MapFrom(src => src.Generos.Select(g => g.Nombre).ToList()))
                .ForMember(dest => dest.Plataformas, opt => opt.MapFrom(src => src.Plataformas.Select(p => p.Nombre).ToList()))
                .ForMember(dest => dest.Imagenes, opt => opt.MapFrom(src => src.Imagenes.Select(i => i.Url).ToList()))
                .ForMember(dest => dest.Videos, opt => opt.MapFrom(src => src.Videos.Select(v => v.Url).ToList()))
                .ForMember(dest => dest.Idiomas, opt => opt.MapFrom(src => src.Idiomas.Select(i => i.Nombre).ToList()));
            
            // Mapeo de JuegoDTO a Juego
            CreateMap<JuegoDTO, Juego>()
                .ForMember(dest => dest.Imagenes, opt => opt.MapFrom(src => src.Imagenes.Select(url => new Imagen { Url = url }).ToList()))
                .ForMember(dest => dest.Videos, opt => opt.MapFrom(src => src.Videos.Select(url => new Video { Url = url }).ToList()))
                .ForMember(dest => dest.Clasificacion, opt => opt.MapFrom(src => ObtenerClasificacionPorNombre(src.Clasificacion)))
                .ForMember(dest => dest.Generos, opt => opt.MapFrom(src => src.Generos.Select(nombre => new Genero { Nombre = nombre }).ToList()))
                .ForMember(dest => dest.Plataformas, opt => opt.MapFrom(src => src.Plataformas.Select(nombre => new Plataforma { Nombre = nombre }).ToList()))
                .ForMember(dest => dest.Idiomas, opt => opt.MapFrom(src => src.Idiomas.Select(nombre => new Idioma { Nombre = nombre }).ToList()));

            CreateMap<Plataforma, PlataformaDTO>().ReverseMap(); // Mapear de Plataforma a PlataformaDTO y viceversa
            CreateMap<Genero, GeneroDTO>().ReverseMap();// Mapeo de Genero a GeneroDto y viceversa
            CreateMap<Idioma, IdiomaDTO>().ReverseMap();// Mapeo de Idioma a IdiomaDTO y viceversa
            CreateMap<Clasificacion, ClasificacionDTO>().ReverseMap();// Mapeo de Clasificacion a ClasificacionDto y viceversa
            CreateMap<RegistrarUsuarioDTO, Usuario>().ReverseMap();
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
