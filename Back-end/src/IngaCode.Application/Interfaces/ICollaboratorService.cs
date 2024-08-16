using IngaCode.Application.DTOs;

namespace IngaCode.Application.Interfaces;

public interface ICollaboratorService
{
    Task<CollaboratorDto> GetCollaboratorByIdAsync(Guid id);
    Task<IEnumerable<CollaboratorDto>> GetAllCollaboratorsAsync();
}
