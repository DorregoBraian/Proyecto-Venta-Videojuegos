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
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _generoRepository;
        private readonly IMapper _mapper;

        public GeneroService(IGeneroRepository generoRepository, IMapper mapper)
        {
            _generoRepository = generoRepository;
            _mapper = mapper;
        }

        // Obtiene todos los géneros
        public async Task<IEnumerable<GeneroDTO>> GetAllGenerosAsync()
        {
            var generos = await _generoRepository.ReadAllGenerosAsync();
            if (generos != null)
            {
                return _mapper.Map<IEnumerable<GeneroDTO>>(generos);
            }
            throw new KeyNotFoundException("No se encontró ningún género");
        }

        // Obtiene un género por su ID
        public async Task<GeneroDTO> GetGeneroByIdAsync(int id)
        {
            if (id > 0)
            {
                var genero = await _generoRepository.ReadGeneroByIdAsync(id);
                if (genero != null)
                {
                    return _mapper.Map<GeneroDTO>(genero);
                }
                throw new ArgumentNullException(nameof(id), $"No se encontró una genero con el ID {id}.");
            }
            throw new ArgumentNullException($"El ID es null o 0.");
        }

        // Obtiene un género por su nombre
        public async Task<GeneroDTO> GetGeneroByNombreAsync(string nombre)
        {
            if (nombre != null)
            {
                var genero = await _generoRepository.ReadGeneroByNameAsync(nombre);
                if (genero != null)
                {
                    return _mapper.Map<GeneroDTO>(genero);
                }
                throw new ArgumentNullException(nameof(nombre), $"No se encontró un género con el nombre {nombre}.");
            }
            throw new ArgumentNullException(nameof(nombre), "El nombre del género es null.");
        }

        // Crea un nuevo género a partir de un DTO
        public async Task<GeneroDTO> CreateGeneroAsync(GeneroDTO generoDto)
        {
            if (generoDto != null)
            {
                var genero = _mapper.Map<Genero>(generoDto);
                await _generoRepository.CreateGeneroAsync(genero);
                return generoDto;
            }
            throw new ArgumentNullException(nameof(generoDto), "El género ingresado es null.");
        }

        // Actualiza un genero existente mediante su ID
        public async Task<GeneroDTO> UpdateGeneroByIdAsync(int id, GeneroDTO generoDto)
        {
            if (generoDto != null && id > 0)
            {
                var generoExistente = await _generoRepository.ReadGeneroByIdAsync(id);
                if (generoExistente != null)
                {
                    _mapper.Map(generoDto, generoExistente);
                    await _generoRepository.UpdateGeneroAsync(generoExistente);
                    return generoDto;
                }
                throw new ArgumentNullException($"No se encontró un genero con el ID {id}.");
            }
            throw new ArgumentNullException(nameof(id), "El genero y/o el ID es null o 0.");
        }

        // Actualiza una plataforma existente mediante su Nombre
        public async Task<GeneroDTO> UpdateGeneroByNombreAsync(string nombre, GeneroDTO generoDto)
        {
            if (generoDto != null && nombre != null)
            {
                var generoExistente = await _generoRepository.ReadGeneroByNameAsync(nombre);
                if (generoExistente != null)
                {
                    _mapper.Map(generoDto, generoExistente);
                    await _generoRepository.UpdateGeneroAsync(generoExistente);
                    return generoDto;
                }
                throw new ArgumentNullException($"No se encontró un genero con el nombre {nombre}.");
            }
            throw new ArgumentNullException(nameof(nombre), "El genero y/o el nombre es null.");
        }

        // Elimina un género usando su nombre
        public async Task<GeneroDTO> DeleteGeneroByNombreAsync(string nombre)
        {
            if (nombre != null)
            {
                var genero = await _generoRepository.ReadGeneroByNameAsync(nombre);
                if (genero != null)
                {
                    await _generoRepository.DeleteGeneroAsync(genero.Id);
                    return _mapper.Map<GeneroDTO>(genero);
                }
                throw new ArgumentNullException($"No se encontró un genero con el nombre {nombre}.");
            }
            throw new ArgumentNullException(nameof(nombre), "El nombre del género es null.");
        }

        // Elimina un género usando su ID
        public async Task<GeneroDTO> DeleteGeneroByIdAsync(int id)
        {
            if (id > 0)
            {
                var genero = await _generoRepository.ReadGeneroByIdAsync(id);
                if (genero != null)
                {
                    await _generoRepository.DeleteGeneroAsync(genero.Id);
                    return _mapper.Map<GeneroDTO>(genero);
                }
                throw new ArgumentNullException($"No se encontró un genero con el ID {id}.");
            }
            throw new ArgumentNullException(nameof(id), $"El ID ingresado es invalido {id}.");
        }
    }
}
