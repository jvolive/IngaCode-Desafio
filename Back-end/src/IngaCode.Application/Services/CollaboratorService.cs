using IngaCode.Application.DTOs;
using IngaCode.Application.Interfaces;
using IngaCode.Domain.Interfaces;
using AutoMapper;

namespace IngaCode.Application.Services;

public class CollaboratorService : ICollaboratorService
{
    private readonly ICollaboratorRepository _collaboratorRepository;
    private readonly IMapper _mapper;

    public CollaboratorService(ICollaboratorRepository collaboratorRepository, IMapper mapper)
    {
        _collaboratorRepository = collaboratorRepository;
        _mapper = mapper;
    }

    public async Task<CollaboratorDto> GetCollaboratorByNameAsync(string name)
    {
        var collaborator = await _collaboratorRepository.GetByNameAsync(name);
        return collaborator == null ? null : _mapper.Map<CollaboratorDto>(collaborator);
    }

    public async Task<CollaboratorDto> GetCollaboratorByUserIdAsync(Guid userId)
    {
        var collaborator = await _collaboratorRepository.GetByUserIdAsync(userId);
        return collaborator == null ? null : _mapper.Map<CollaboratorDto>(collaborator);
    }

    public async Task<IEnumerable<CollaboratorDto>> GetAllCollaboratorsAsync()
    {
        var collaborators = await _collaboratorRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CollaboratorDto>>(collaborators);
    }
}

