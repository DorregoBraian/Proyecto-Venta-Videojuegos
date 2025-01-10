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
    public class PlataformaService : IPlataformaService
    {
        private readonly IPlataformaRepository _plataformaRepository;
        private readonly IMapper _mapper;

        public PlataformaService(IPlataformaRepository plataformaRepository, IMapper mapper)
        {
            _plataformaRepository = plataformaRepository;
            _mapper = mapper;
        }

        // Devuelve todas las plataformas
        public async Task<IEnumerable<PlataformaDTO>> GetAllPlataformasAsync()
        {
            var plataformas = await _plataformaRepository.ReadAllPlataformasAsync();
            if (plataformas != null)
            {
                return _mapper.Map<IEnumerable<PlataformaDTO>>(plataformas);
            }
            throw new KeyNotFoundException("No se encontró ninguna plataforma");
        }

        // Obtiene una plataforma por su ID
        public async Task<PlataformaDTO> GetPlataformaByIdAsync(int id)
        {
            if (id > 0)
            {
                var plataforma = await _plataformaRepository.ReadPlataformaByIdAsync(id);
                if (plataforma != null)
                {
                    return _mapper.Map<PlataformaDTO>(plataforma);
                }
                throw new ArgumentNullException(nameof(id), $"No se encontró una plataforma con el ID {id}.");
            }
            throw new ArgumentNullException($"El ID es null o 0");
        }

        // Obtiene una plataforma por nombre
        public async Task<PlataformaDTO> GetPlataformaByNombreAsync(string nombre)
        {
            if (nombre != null)
            {
                var plataforma = await _plataformaRepository.ReadPlataformaByNameAsync(nombre);
                if (plataforma != null)
                {
                    return _mapper.Map<PlataformaDTO>(plataforma);
                }
                throw new ArgumentNullException(nameof(nombre), $"No se encontró una plataforma con el nombre {nombre}.");
            }
            throw new ArgumentNullException($"El nombre es null");
        }

        // Crea una nueva plataforma a partir de un DTO
        public async Task<PlataformaDTO> CreatePlataformaAsync(PlataformaDTO plataformaDto)
        {
            if (plataformaDto != null)
            {
                var plataforma = _mapper.Map<Plataforma>(plataformaDto);
                await _plataformaRepository.CreatePlataformaAsync(plataforma);
                return plataformaDto;
            }
            throw new ArgumentNullException(nameof(plataformaDto), "La plataforma ingresada es nula.");
        }

        // Actualiza una plataforma por su ID
        public async Task<PlataformaDTO> UpdatePlataformaByIdAsync(int id, PlataformaDTO plataformaDto)
        {
            if (plataformaDto != null && id > 0)
            {
                var plataformaExistente = await _plataformaRepository.ReadPlataformaByIdAsync(id);
                if (plataformaExistente != null)
                {
                    _mapper.Map(plataformaDto, plataformaExistente);
                    await _plataformaRepository.UpdatePlataformaAsync(plataformaExistente);
                    return plataformaDto;
                }
                throw new ArgumentNullException($"No se encontró una plataforma con el ID {id}.");
            }
            throw new ArgumentNullException(nameof(plataformaDto), "La plataforma y/o el ID es null o 0.");
        }

        // Actualiza una plataforma por su Nombre
        public async Task<PlataformaDTO> UpdatePlataformaByNombreAsync(string nombre, PlataformaDTO plataformaDto)
        {
            if (plataformaDto != null && nombre != null)
            {
                var plataformaExistente = await _plataformaRepository.ReadPlataformaByNameAsync(nombre);
                if (plataformaExistente != null)
                {
                    _mapper.Map(plataformaDto, plataformaExistente);
                    await _plataformaRepository.UpdatePlataformaAsync(plataformaExistente);
                    return plataformaDto;
                }
                throw new ArgumentNullException($"No se encontró una plataforma con el nombre {nombre}.");
            }
            throw new ArgumentNullException("La plataforma y/o el ID es null o 0.");
        }

        // Elimina una plataforma por su ID
        public async Task<PlataformaDTO> DeletePlataformaByIdAsync(int id)
        {
            if (id > 0)
            {
                var plataforma = await _plataformaRepository.ReadPlataformaByIdAsync(id);
                if (plataforma != null)
                {
                    await _plataformaRepository.DeletePlataformaAsync(plataforma.Id);
                    return _mapper.Map<PlataformaDTO>(plataforma);
                }
                throw new ArgumentNullException($"No se encontró una plataforma con el ID {id}.");
            }
            throw new ArgumentNullException($"El ID es null o 0");
        }

        // Elimina una plataforma por su Nombre
        public async Task<PlataformaDTO> DeletePlataformaByNombreAsync(string nombre)
        {
            if(nombre != null)
            {
                var plataforma = await _plataformaRepository.ReadPlataformaByNameAsync(nombre);
                if (plataforma != null)
                {
                    await _plataformaRepository.DeletePlataformaAsync(plataforma.Id);
                    return _mapper.Map<PlataformaDTO>(plataforma);
                }
                throw new ArgumentNullException($"No se encontró una plataforma con el nombre {nombre}.");
            }
            throw new ArgumentNullException($"El nombre es null");
        }
    }
}
