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
        var sql = @"
            SELECT 
                id_proj AS Id, 
                name_proj AS Name, 
                createdAt_proj AS CreatedAt, 
                updatedAt_proj AS UpdatedAt, 
                deletedAt_proj AS DeletedAt 
            FROM projects";
        return (await _dbConnection.QueryAsync<Project>(sql)).ToList();
    }

    public async Task<Project> GetByNameAsync(string name)
    {
        var sql = @"
            SELECT 
                id_proj AS Id, 
                name_proj AS Name, 
                createdAt_proj AS CreatedAt, 
                updatedAt_proj AS UpdatedAt, 
                deletedAt_proj AS DeletedAt 
            FROM projects
            WHERE name_proj = @Name";
        return await _dbConnection.QuerySingleOrDefaultAsync<Project>(sql, new { Name = name });
    }

    public async Task AddAsync(Project entity)
    {
        var sql = @"
            INSERT INTO projects (name_proj)
            VALUES (@Name) 
            RETURNING id_proj";
        entity.Id = await _dbConnection.ExecuteScalarAsync<Guid>(sql, entity);
    }

    public async Task UpdateByNameAsync(Project entity, string oldName)
    {
        var sql = @"
                UPDATE projects
                SET name_proj = @NewName, 
                    updatedAt_proj = @UpdatedAt
                WHERE name_proj = @OldName";

        var parameters = new DynamicParameters();
        parameters.Add("NewName", entity.Name);
        parameters.Add("OldName", oldName);
        parameters.Add("UpdatedAt", entity.UpdatedAt ?? DateTime.Now);

        var affectedRows = await _dbConnection.ExecuteAsync(sql, parameters);

        if (affectedRows == 0)
        {
            throw new Exception("No rows were updated. Check if the old name exists.");
        }
    }

    public async Task DeleteAsync(string name)
    {
        var sql = @"DELETE FROM projects WHERE name_proj = @Name";
        await _dbConnection.ExecuteAsync(sql, new { Name = name });
    }
}