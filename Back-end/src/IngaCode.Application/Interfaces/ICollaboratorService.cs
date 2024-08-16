using IngaCode.Application.DTOs.CollaboratorDTOs;

namespace IngaCode.Application.Interfaces
{
    public interface ICollaboratorService
    {
        Task<IEnumerable<CollaboratorDto>> GetAllCollaboratorsAsync();
        Task<CollaboratorDto?> GetCollaboratorByIdAsync(Guid id);
        Task<CollaboratorDto> CreateCollaboratorAsync(CollaboratorCreateDto dto);
        Task UpdateCollaboratorAsync(Guid id, CollaboratorUpdateDto dto);
        Task DeleteCollaboratorAsync(Guid id);
        Task<CollaboratorDto?> GetCollaboratorByNameAsync(string name);
    }
}
