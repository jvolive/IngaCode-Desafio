using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using System.Data;
using Dapper;

namespace IngaCode.Infrastructure.Repository;

public class TaskEntityRepository : ITaskEntityRepository
{
    private readonly IDbConnection _dbConnection;

    public TaskEntityRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<TaskEntity>> GetAllAsync()
    {
        var query = "SELECT * FROM tasks";
        return await _dbConnection.QueryAsync<TaskEntity>(query);
    }

    public async Task<TaskEntity> GetByNameAsync(string name)
    {
        var query = "SELECT * FROM tasks WHERE name_task = @Name";
        return await _dbConnection.QuerySingleOrDefaultAsync<TaskEntity>(query, new { Name = name });
    }

    public async Task AddAsync(TaskEntity entity)
    {
        var query = @"
                INSERT INTO tasks (name_task, description_task, proj_id)
                VALUES (@Name, @Description, @ProjectId)
                RETURNING id_task";
        entity.Id = await _dbConnection.ExecuteScalarAsync<Guid>(query, new
        {
            Name = entity.Name,
            Description = entity.Description,
            ProjectId = entity.ProjectId
        });
    }

    public async Task UpdateAsync(TaskEntity entity)
    {
        var query = @"
        UPDATE tasks
        SET name_task = @Name, 
            description_task = @Description, 
            proj_id = @ProjectId,
            updatedAt_task = @UpdatedAt, 
        WHERE name_task = @Name";

        await _dbConnection.ExecuteAsync(query, new
        {
            Name = entity.Name,
            Description = entity.Description,
            ProjectId = entity.ProjectId,
            UpdatedAt = entity.UpdatedAt,
        });
    }

    public async Task DeleteAsync(string name)
    {
        var query = "DELETE FROM tasks WHERE id_task = @Name";
        await _dbConnection.ExecuteAsync(query, new { Name = name });
    }
}