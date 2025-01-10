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
    public class IdiomaService : IIdiomaService
    {
        private readonly IIdiomaRepository _idiomaRepository;
        private readonly IMapper _mapper;

        public IdiomaService(IIdiomaRepository idiomaRepository, IMapper mapper)
        {
            _idiomaRepository = idiomaRepository;
            _mapper = mapper;
        }

        // Obtiene todos los idiomas
        public async Task<IEnumerable<IdiomaDTO>> GetAllIdiomasAsync()
        {
            var idiomas = await _idiomaRepository.ReadAllIdiomasAsync();
            if (idiomas != null)
            {
                return _mapper.Map<IEnumerable<IdiomaDTO>>(idiomas);
            }
            throw new KeyNotFoundException("No se encontro ningun idiomas");
        }

        // Obtiene un idioma por su ID
        public async Task<IdiomaDTO> GetIdiomaByIdAsync(int id)
        {
            if (id > 0)
            {
                var idioma = await _idiomaRepository.ReadIdiomaByIdAsync(id);
                if (idioma != null)
                {
                    return _mapper.Map<IdiomaDTO>(idioma);
                }
                throw new ArgumentNullException(nameof(id), $"No se encontro un idioma con el ID {id}.");
            }
            throw new ArgumentNullException($"El ID es null o 0");
        }

        // Obtiene un idioma por su nombre
        public async Task<IdiomaDTO> GetIdiomaByNombreAsync(string nombre)
        {
            if (nombre != null)
            {
                var idioma = await _idiomaRepository.ReadIdiomaByNameAsync(nombre);
                if (idioma != null)
                {
                    return _mapper.Map<IdiomaDTO>(idioma);
                }
                throw new ArgumentNullException(nameof(nombre), $"No se encontro un idioma con el nombre {nombre}.");
            }
            throw new ArgumentNullException($"El nombre es null");
        }

        // Crea un nuevo idioma a partir de un DTO
        public async Task<IdiomaDTO> CreateIdiomaAsync(IdiomaDTO idiomaDto)
        {
            if (idiomaDto != null)
            {
                var idioma = _mapper.Map<Idioma>(idiomaDto);
                await _idiomaRepository.CreateIdiomaAsync(idioma);
                return idiomaDto;
            }
            throw new ArgumentNullException(nameof(idiomaDto), "El idioma ingresada es nula.");
        }

        // Actualiza un idioma existente por su ID
        public async Task<IdiomaDTO> UpdateIdiomaByIdAsync(int id, IdiomaDTO idiomaDto)
        {
            if (idiomaDto != null && id > 0)
            {
                var idiomaExistente = await _idiomaRepository.ReadIdiomaByIdAsync(id);
                if (idiomaExistente != null)
                {
                    _mapper.Map(idiomaDto, idiomaExistente);
                    await _idiomaRepository.UpdateIdiomaAsync(idiomaExistente);
                    return idiomaDto;
                }
                throw new ArgumentNullException($"No se encontró un idioma con el ID {id}.");
            }
            throw new ArgumentNullException(nameof(idiomaDto), "El idioma y/o el ID es null o 0.");
        }

        // Actualiza un idioma existente por su nombre
        public async Task<IdiomaDTO> UpdateIdiomaByNombreAsync(string nombre, IdiomaDTO idiomaDto)
        {
            if (idiomaDto != null && nombre != null)
            {
                var idiomaExistente = await _idiomaRepository.ReadIdiomaByNameAsync(nombre);
                if (idiomaExistente != null)
                {
                    _mapper.Map(idiomaDto, idiomaExistente);
                    await _idiomaRepository.UpdateIdiomaAsync(idiomaExistente);
                    return idiomaDto;
                }
                throw new ArgumentNullException($"No se encontró un idioma con el nombre {nombre}.");
            }
            throw new ArgumentNullException(nameof(idiomaDto), "El idioma y/o el nombre es null o 0.");
        }

        // Elimina un idioma por su ID
        public async Task<IdiomaDTO> DeleteIdiomaByIdAsync(int id)
        {
            if (id > 0)
            {
                var idioma = await _idiomaRepository.ReadIdiomaByIdAsync(id);
                if (idioma != null)
                {
                    await _idiomaRepository.DeleteIdiomaAsync(idioma.Id);
                    return _mapper.Map<IdiomaDTO>(idioma);
                }
                throw new ArgumentNullException($"No se encontró un idioma con el ID {id}.");
            }
            throw new ArgumentNullException($"El ID es null o 0");
        }

        // Elimina un idioma por su nombre
        public async Task<IdiomaDTO> DeleteIdiomaByNombreAsync(string nombre)
        {
            if (nombre  != null)
            {
                var idioma = await _idiomaRepository.ReadIdiomaByNameAsync(nombre);
                if (idioma != null)
                {
                    await _idiomaRepository.DeleteIdiomaAsync(idioma.Id);
                    return _mapper.Map<IdiomaDTO>(idioma);
                }
                throw new ArgumentNullException($"No se encontró un idioma con el nombre {nombre}.");
            }
            throw new ArgumentNullException($"El nombre es null");
        }
    }
}
