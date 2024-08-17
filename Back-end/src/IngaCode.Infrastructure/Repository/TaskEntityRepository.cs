using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using System.Data;
using Dapper;

namespace IngaCode.Infrastructure.Repository
{
    public class TaskEntityRepository : ITaskEntityRepository
    {
        private readonly IDbConnection _dbConnection;

        public TaskEntityRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AddAsync(TaskEntity entity)
        {
            var query = @"
                INSERT INTO tasks (id_task, name_task, description_task, proj_id, createdAt_task, updatedAt_task, deletedAt_task)
                VALUES (@Id, @Name, @Description, @ProjectId, @CreatedAt, @UpdatedAt, @DeletedAt)
                RETURNING id_task";
            entity.Id = await _dbConnection.ExecuteScalarAsync<Guid>(query, entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var query = "DELETE FROM tasks WHERE id_task = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<IEnumerable<TaskEntity>> GetAllAsync()
        {
            var query = "SELECT * FROM tasks";
            return await _dbConnection.QueryAsync<TaskEntity>(query);
        }

        public async Task<TaskEntity> GetByIdAsync(Guid id)
        {
            var query = "SELECT * FROM tasks WHERE id_task = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<TaskEntity>(query, new { Id = id });
        }

        public async Task<TaskEntity> GetByNameAsync(string name)
        {
            var query = "SELECT * FROM tasks WHERE name_task = @Name";
            return await _dbConnection.QuerySingleOrDefaultAsync<TaskEntity>(query, new { Name = name });
        }

        public async Task UpdateAsync(TaskEntity entity)
        {
            var query = @"
                UPDATE tasks
                SET name_task = @Name, description_task = @Description, proj_id = @ProjectId,
                    createdAt_task = @CreatedAt, updatedAt_task = @UpdatedAt, deletedAt_task = @DeletedAt
                WHERE id_task = @Id";

            await _dbConnection.ExecuteAsync(query, entity);
        }

        public async Task<IEnumerable<TaskEntity>> GetByProjectIdAsync(Guid projectId)
        {
            var query = "SELECT * FROM tasks WHERE proj_id = @ProjectId";
            return await _dbConnection.QueryAsync<TaskEntity>(query, new { ProjectId = projectId });
        }

        public async Task<IEnumerable<TaskEntity>> GetTasksByDateAsync(DateTime date)
        {
            var query = "SELECT * FROM tasks WHERE CAST(createdAt_task AS DATE) = @Date";
            return await _dbConnection.QueryAsync<TaskEntity>(query, new { Date = date.Date });
        }
    }
}
