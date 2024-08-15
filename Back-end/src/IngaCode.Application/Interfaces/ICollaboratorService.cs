using IngaCode.Domain.Entities;

namespace IngaCode.Application.Interfaces
{
    public interface ICollaboratorService
    {
        Task<IEnumerable<Collaborator>> GetAllCollaboratorsAsync();
        Task<Collaborator> GetCollaboratorByIdAsync(Guid id);
        Task<Collaborator> CreateCollaboratorAsync(Collaborator collaborator);
        Task UpdateCollaboratorAsync(Collaborator collaborator);
        Task DeleteCollaboratorAsync(Guid id);
        Task<Collaborator> GetCollaboratorByNameAsync(string name);
    }
}
