using IngaCode.Application.DTOs;

namespace IngaCode.Application.Interfaces
{
    public interface ICollaboratorService
    {
        Task<CollaboratorDto> GetCollaboratorByNameAsync(string name);
        Task<CollaboratorDto> GetCollaboratorByUserIdAsync(Guid userId);
        Task<IEnumerable<CollaboratorDto>> GetAllCollaboratorsAsync();
    }
}
