using IngaCode.Domain.Entities;

namespace IngaCode.Domain.Interfaces;

public interface ICollaboratorRepository
{
    Task<Collaborator> GetByNameAsync(string name);
    Task<Collaborator> GetByUserIdAsync(Guid UserId);
}