using IngaCode.Domain.Entities;

namespace IngaCode.Domain.Interfaces
{
    public interface ICollaboratorRepository : IRepository<Collaborator>
    {
        Task<Collaborator> GetByNameAsync(string name);
    }
}
