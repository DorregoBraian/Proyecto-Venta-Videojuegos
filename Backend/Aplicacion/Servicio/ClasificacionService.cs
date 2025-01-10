using Aplicacion.DTOs;
using Aplicacion.IServicios;
using AutoMapper;
using Dominio.Entidades;
using Dominio.IRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicio
{
    public class ClasificacionService : IClasificacionService
    {
        private readonly IClasificacionRepository _clasificacionRepository;
        private readonly IMapper _mapper;

        public ClasificacionService(IClasificacionRepository clasificacionRepository, IMapper mapper)
        {
            _clasificacionRepository = clasificacionRepository;
            _mapper = mapper;
        }

        // Obtiene todas las clasificaciones 
        public async Task<IEnumerable<ClasificacionDTO>> GetAllClasificacionesAsync()
        {
            var clasificaciones = await _clasificacionRepository.ReadAllClasificacionesAsync();
            if (clasificaciones != null)
            {
                return _mapper.Map<IEnumerable<ClasificacionDTO>>(clasificaciones);
            }
            throw new KeyNotFoundException("No se encontró ninguna clasificacion");
        }

        // Obtiene una clasificación por su ID
        public async Task<ClasificacionDTO> GetClasificacionByIdAsync(int id)
        {
            if (id > 0)
            {
                var clasificacion = await _clasificacionRepository.ReadClasificacionByIdAsync(id);
                if (clasificacion != null)
                {
                    return _mapper.Map<ClasificacionDTO>(clasificacion);
                }
                throw new ArgumentNullException(nameof(id), $"No se encontró una clasificacion con el ID {id}.");
            }
            throw new ArgumentNullException($"El ID es null o 0");
        }

        // Obtiene una clasificación por su nombre
        public async Task<ClasificacionDTO> GetClasificacionByNombreAsync(string nombre)
        {
            if (nombre != null)
            {
                var clasificacion = await _clasificacionRepository.ReadClasificacionByNameAsync(nombre);
                if (clasificacion != null)
                {
                    return _mapper.Map<ClasificacionDTO>(clasificacion);
                }
                throw new ArgumentNullException(nameof(nombre), $"No se encontró una clasificacion con el nombre {nombre}.");
            }
            throw new ArgumentNullException(nameof(nombre), "El nombre de la clasificación es null.");
        }

        // Crea una nueva clasificación a partir de un DTO
        public async Task<ClasificacionDTO> CreateClasificacionAsync(ClasificacionDTO clasificacionDto)
        {
            if (clasificacionDto != null)
            {
                var clasificacion = _mapper.Map<Clasificacion>(clasificacionDto);
                await _clasificacionRepository.CreateClasificacionAsync(clasificacion);
                return clasificacionDto;

            }
            throw new ArgumentNullException(nameof(clasificacionDto), "La clasificación ingresada es nula.");
        }

        // Actualiza una clasificación existente por su ID
        public async Task<ClasificacionDTO> UpdateClasificacionByIdAsync(int id, ClasificacionDTO clasificacionDto)
        {
            if (clasificacionDto != null && id > 0)
            {
                var clasificacionExistente = await _clasificacionRepository.ReadClasificacionByIdAsync(id);
                if (clasificacionExistente != null)
                {
                    _mapper.Map(clasificacionDto, clasificacionExistente);
                    await _clasificacionRepository.UpdateClasificacionAsync(clasificacionExistente);
                    return clasificacionDto;
                }
                throw new ArgumentNullException($"No se encontró una clasificacion con el ID {id}.");
            }
            throw new ArgumentNullException(nameof(clasificacionDto), "La clasificación y/o el ID es null o 0.");
        }

        // Actualiza una clasificación existente por su nombre
        public async Task<ClasificacionDTO> UpdateClasificacionByNombreAsync(string nombre, ClasificacionDTO clasificacionDto)
        {
            if (clasificacionDto != null && nombre != null)
            {
                var clasificacionExistente = await _clasificacionRepository.ReadClasificacionByNameAsync(nombre);
                if (clasificacionExistente != null)
                {
                    _mapper.Map(clasificacionDto, clasificacionExistente);
                    await _clasificacionRepository.UpdateClasificacionAsync(clasificacionExistente);
                    return clasificacionDto;
                }
                throw new ArgumentNullException($"No se encontró una clasificacion con el ID {nombre}.");
            }
            throw new ArgumentNullException(nameof(clasificacionDto), "La clasificación y/o el nombre es null.");
        }

        // Elimina una clasificación por su ID
        public async Task<ClasificacionDTO> DeleteClasificacionByIdAsync(int id)
        {
            if (id > 0)
            {
                var clasificacion = await _clasificacionRepository.ReadClasificacionByIdAsync(id);
                if (clasificacion != null)
                {
                    await _clasificacionRepository.DeleteClasificacionAsync(clasificacion.Id);
                    return _mapper.Map<ClasificacionDTO>(clasificacion);
                }
                throw new ArgumentNullException($"No se encontró una clasificacion con el ID {id}.");
            }
            throw new ArgumentNullException(nameof(id), $"El id ingresado es invalido {id}.");
        }

        // Elimina una clasificación por su nombre
        public async Task<ClasificacionDTO> DeleteClasificacionByNombreAsync(string nombre)
        {
            if (nombre != null)
            {
                var clasificacion = await _clasificacionRepository.ReadClasificacionByNameAsync(nombre);
                if (clasificacion != null)
                {
                    await _clasificacionRepository.DeleteClasificacionAsync(clasificacion.Id);
                    return _mapper.Map<ClasificacionDTO>(clasificacion);
                }
                throw new ArgumentNullException($"No se encontró una clasificacion con el nombre {nombre}.");
            }
            throw new ArgumentNullException(nameof(nombre), $"El nombre ingresado es invalido {nombre}.");
        }
    }
}
