using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using System.Data;
using Dapper;

namespace IngaCode.Infrastructure.Repository;
public class ProjectRepository : IProjectRepository
{
    private readonly IDbConnection _dbConnection;

    public ProjectRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        var query = "SELECT * FROM projects";
        return (await _dbConnection.QueryAsync<Project>(query)).ToList();
    }

    public async Task<Project> GetByNameAsync(string name)
    {
        var query = "SELECT * FROM projects WHERE name_proj = @Name";
        return await _dbConnection.QuerySingleOrDefaultAsync<Project>(query, new { Name = name });
    }

    public async Task AddAsync(Project entity)
    {
        var query = @"
            INSERT INTO projects (name_proj)
            VALUES (@Name) 
            RETURNING id_proj";
        entity.Id = await _dbConnection.ExecuteScalarAsync<Guid>(query, entity);
    }

    public async Task UpdateAsync(Project entity)
    {
        var query = @"
            UPDATE projects
            SET name_proj = @Name, 
                updatedAt_proj = @UpdatedAt, 
            WHERE id_proj = @Name";

        await _dbConnection.ExecuteAsync(query, new
        {
            Name = entity.Name,
            UpdatedAt = entity.UpdatedAt,
        });
    }

    public async Task DeleteAsync(string name)
    {
        var query = "DELETE FROM projects WHERE name_proj = @Name";
        await _dbConnection.ExecuteAsync(query, new { Name = name });
    }
}