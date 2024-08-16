using IngaCode.Application.Interfaces;
using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using IngaCode.Application.DTOs.CollaboratorDTOs;
using AutoMapper;

namespace IngaCode.Application.Services
{
    public class CollaboratorService : ICollaboratorService
    {
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly IMapper _mapper;

        public CollaboratorService(ICollaboratorRepository collaboratorRepository, IMapper mapper)
        {
            _collaboratorRepository = collaboratorRepository;
            _mapper = mapper;
        }

        public async Task<CollaboratorDto> CreateCollaboratorAsync(CollaboratorCreateDto dto)
        {
            var collaborator = _mapper.Map<Collaborator>(dto);
            await _collaboratorRepository.AddAsync(collaborator);
            return _mapper.Map<CollaboratorDto>(collaborator);
        }

        public async Task<IEnumerable<CollaboratorDto>> GetAllCollaboratorsAsync()
        {
            var collaborators = await _collaboratorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CollaboratorDto>>(collaborators);
        }

        public async Task<CollaboratorDto?> GetCollaboratorByIdAsync(Guid id)
        {
            var collaborator = await _collaboratorRepository.GetByIdAsync(id);
            return collaborator == null ? null : _mapper.Map<CollaboratorDto>(collaborator);
        }

        public async Task UpdateCollaboratorAsync(Guid id, CollaboratorUpdateDto dto)
        {
            var collaborator = await _collaboratorRepository.GetByIdAsync(id);
            if (collaborator != null)
            {
                _mapper.Map(dto, collaborator);
                await _collaboratorRepository.UpdateAsync(collaborator);
            }
        }

        public async Task DeleteCollaboratorAsync(Guid id)
        {
            await _collaboratorRepository.DeleteAsync(id);
        }

        public async Task<CollaboratorDto?> GetCollaboratorByNameAsync(string name)
        {
            var collaborator = await _collaboratorRepository.GetByNameAsync(name);
            return collaborator == null ? null : _mapper.Map<CollaboratorDto>(collaborator);
        }
    }
}
