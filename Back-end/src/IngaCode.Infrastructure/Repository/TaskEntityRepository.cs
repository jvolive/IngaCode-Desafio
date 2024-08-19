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
        var sql = @"
            SELECT 
                id_task AS Id, 
                name_task AS Name,
                description_task AS Description ,
                proj_id AS ProjectId,
                collab_name AS CollaboratorName,
                createdAt_task AS CreatedAt, 
                updatedAt_task AS UpdatedAt, 
                deletedAt_task AS DeletedAt
            FROM tasks
            WHERE deletedAt_task IS NULL";
        return await _dbConnection.QueryAsync<TaskEntity>(sql);
    }

    public async Task<TaskEntity> GetByNameAsync(string name)
    {
        var sql = @"
            SELECT 
                id_task AS Id, 
                name_task AS Name,
                description_task AS Description ,
                proj_id AS ProjectId,
                collab_name AS CollaboratorName,
                createdAt_task AS CreatedAt, 
                deletedAt_task AS DeletedAt
            FROM tasks
            WHERE name_task = @Name
            AND deletedAt_task IS NULL";
        return await _dbConnection.QuerySingleOrDefaultAsync<TaskEntity>(sql, new { Name = name });
    }

    public async Task AddAsync(TaskEntity entity)
    {
        var sql = @"
                INSERT INTO tasks (name_task, description_task, proj_id, collab_name)
                VALUES (@Name, @Description, @ProjectId, @CollaboratorName)
                RETURNING id_task";
        entity.Id = await _dbConnection.ExecuteScalarAsync<Guid>(sql, entity);
    }

    public async Task UpdateByNameAsync(TaskEntity entity, string oldName)
    {
        var sql = @"
                UPDATE tasks
                SET name_task = @NewName,
                    description_task = @Description,
                    proj_id = @ProjectId,
                    collab_name =@CollaboratorName,
                    updatedAt_task = @UpdatedAt
                WHERE name_task = @OldName";

        var parameters = new DynamicParameters();
        parameters.Add("NewName", entity.Name);
        parameters.Add("OldName", oldName);
        parameters.Add("Description", entity.Description);
        parameters.Add("ProjectId", entity.ProjectId);
        parameters.Add("CollaboratorName", entity.CollaboratorName);
        parameters.Add("UpdatedAt", entity.UpdatedAt ?? DateTime.Now);

        var affectedRows = await _dbConnection.ExecuteAsync(sql, parameters);

        if (affectedRows == 0)
        {
            throw new Exception("No rows were updated. Check if the old name exists.");
        }
    }

    public async Task DeleteAsync(string name)
    {
        var sql = @"
            UPDATE tasks
            SET deletedAt_task = @DeletedAt
            WHERE name_task = @Name";

        var parameters = new DynamicParameters();
        parameters.Add("Name", name);
        parameters.Add("DeletedAt", DateTime.UtcNow);

        await _dbConnection.ExecuteAsync(sql, parameters);
    }
}