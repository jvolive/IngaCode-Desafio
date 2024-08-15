using IngaCode.Application.Interfaces;
using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;

namespace IngaCode.Application.Services
{
    public class CollaboratorService : ICollaboratorService
    {
        private readonly ICollaboratorRepository _collaboratorRepository;

        public CollaboratorService(ICollaboratorRepository collaboratorRepository)
        {
            _collaboratorRepository = collaboratorRepository;
        }

        public async Task<IEnumerable<Collaborator>> GetAllCollaboratorsAsync()
        {
            return await _collaboratorRepository.GetAllAsync();
        }

        public async Task<Collaborator> GetCollaboratorByIdAsync(Guid id)
        {
            return await _collaboratorRepository.GetByIdAsync(id);
        }

        public async Task<Collaborator> CreateCollaboratorAsync(Collaborator collaborator)
        {
            await _collaboratorRepository.AddAsync(collaborator);
            return collaborator;
        }

        public async Task UpdateCollaboratorAsync(Collaborator collaborator)
        {
            await _collaboratorRepository.UpdateAsync(collaborator);
        }

        public async Task DeleteCollaboratorAsync(Guid id)
        {
            await _collaboratorRepository.DeleteAsync(id);
        }

        public async Task<Collaborator> GetCollaboratorByNameAsync(string name)
        {
            return await _collaboratorRepository.GetByNameAsync(name);
        }
    }
}
