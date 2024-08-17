using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using System.Data;
using Dapper;

namespace IngaCode.Infrastructure.Repository;

public class CollaboratorRepository : ICollaboratorRepository
{
    private readonly IDbConnection _dbConnection;

    public CollaboratorRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<Collaborator> GetByNameAsync(string name)
    {
        var query = "SELECT * FROM collaborators WHERE name_collab = @Name";
        return await _dbConnection.QuerySingleOrDefaultAsync<Collaborator>(query, new { Name = name });
    }

    public async Task<Collaborator> GetByUserIdAsync(Guid userId)
    {
        var query = "SELECT * FROM collaborators WHERE user_id = @UserId";
        return await _dbConnection.QuerySingleOrDefaultAsync<Collaborator>(query, new { UserId = userId });
    }

    public async Task<IEnumerable<Collaborator>> GetAllAsync()
    {
        var query = "SELECT * FROM collaborators";
        return await _dbConnection.QueryAsync<Collaborator>(query);
    }
}
