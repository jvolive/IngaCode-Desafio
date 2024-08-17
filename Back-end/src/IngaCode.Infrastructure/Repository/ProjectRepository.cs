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

    public async Task AddAsync(Project entity)
    {
        var query = @"
            INSERT INTO projects (name_proj, createdAt_proj, updatedAt_proj, deletedAt_proj)
            VALUES (@Name, @CreatedAt, @UpdatedAt, @DeletedAt) 
            RETURNING id_proj";
        entity.Id = await _dbConnection.ExecuteScalarAsync<Guid>(query, entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var query = "DELETE FROM projects WHERE id_proj = @Id";
        await _dbConnection.ExecuteAsync(query, new { Id = id });
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        var query = "SELECT * FROM projects";
        return await _dbConnection.QueryAsync<Project>(query);
    }

    public async Task<Project> GetByIdAsync(Guid id)
    {
        var query = "SELECT * FROM projects WHERE id_proj = @Id";
        return await _dbConnection.QuerySingleOrDefaultAsync<Project>(query, new { Id = id });
    }

    public async Task<Project> GetByNameAsync(string name)
    {
        var query = "SELECT * FROM projects WHERE name_proj = @Name";
        return await _dbConnection.QuerySingleOrDefaultAsync<Project>(query, new { Name = name });
    }

    public async Task UpdateAsync(Project entity)
    {
        var query = @"
            UPDATE projects
            SET name_proj = @Name, 
                createdAt_proj = @CreatedAt, 
                updatedAt_proj = @UpdatedAt, 
                deletedAt_proj = @DeletedAt
            WHERE id_proj = @Id";

        await _dbConnection.ExecuteAsync(query, entity);
    }
}