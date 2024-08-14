using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using System.Data;
using Dapper;

namespace IngaCode.Infrastructure.Repository
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly IDbConnection _dbConnection;

        public CollaboratorRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AddAsync(Collaborator entity)
        {
            var query = @"
                INSERT INTO collaborators (id_collab, name_collab, createdAt_collab, updatedAt_collab, deletedAt_collab)
                VALUES (@Id, @Name, @CreatedAt, @UpdatedAt, @DeletedAt)";

            await _dbConnection.ExecuteAsync(query, entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var query = "DELETE FROM collaborators WHERE id_collab = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<IEnumerable<Collaborator>> GetAllAsync()
        {
            var query = "SELECT * FROM collaborators";
            return await _dbConnection.QueryAsync<Collaborator>(query);
        }

        public async Task<Collaborator> GetByIdAsync(Guid id)
        {
            var query = "SELECT * FROM collaborators WHERE id_collab = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<Collaborator>(query, new { Id = id });
        }

        public async Task<Collaborator> GetByNameAsync(string name)
        {
            var query = "SELECT * FROM collaborators WHERE name_collab = @Name";
            return await _dbConnection.QuerySingleOrDefaultAsync<Collaborator>(query, new { Name = name });
        }

        public async Task UpdateAsync(Collaborator entity)
        {
            var query = @"
                UPDATE collaborators
                SET name_collab = @Name, createdAt_collab = @CreatedAt,
                    updatedAt_collab = @UpdatedAt, deletedAt_collab = @DeletedAt
                WHERE id_collab = @Id";

            await _dbConnection.ExecuteAsync(query, entity);
        }
    }
}
