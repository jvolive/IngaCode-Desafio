using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using System.Data;
using Dapper;

namespace IngaCode.Infrastructure.Repository;

public class TimeTrackerRepository : ITimeTrackerRepository
{
    private readonly IDbConnection _dbConnection;

    public TimeTrackerRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<TimeTracker>> GetByTaskIdAsync(Guid taskId)
    {
        var sql = @"
            SELECT 
                id_time_tracker AS Id,
                start_date_time_tracker AS StartDateTime,
                end_date_time_tracker AS EndDateTime,
                time_zone_id AS TimeZoneId,
                task_id AS TaskId,
                collab_id AS CollabId,
                createdAt_time_tracker AS CreatedAt,
                updatedAt_time_tracker AS UpdatedAt,
                deletedAt_time_tracker AS DeletedAt
            FROM time_tracker 
            WHERE task_id = @TaskId 
            AND deletedAt_time_tracker IS NULL";
        return await _dbConnection.QueryAsync<TimeTracker>(sql, new { TaskId = taskId });
    }

    public async Task<TimeTracker> GetByIdAsync(Guid id)
    {
        var sql = @"
            SELECT 
                id_time_tracker AS Id,
                start_date_time_tracker AS StartDateTime,
                end_date_time_tracker AS EndDateTime,
                time_zone_id AS TimeZoneId,
                task_id AS TaskId,
                collab_id AS CollabId,
                createdAt_time_tracker AS CreatedAt,
                updatedAt_time_tracker AS UpdatedAt,
                deletedAt_time_tracker AS DeletedAt
            FROM time_tracker
            WHERE id_time_tracker = @Id 
            AND deletedAt_time_tracker IS NULL";
        return await _dbConnection.QuerySingleOrDefaultAsync<TimeTracker>(sql, new { Id = id });
    }

    private async Task<IEnumerable<TimeTracker>> GetConflictingTimeTrackers(Guid taskId, DateTime start, DateTime end, Guid? excludeId = null)
    {
        var sql = @"
            SELECT 
                id_time_tracker AS Id,
                start_date_time_tracker AS StartDateTime,
                end_date_time_tracker AS EndDateTime,
                time_zone_id AS TimeZoneId,
                task_id AS TaskId,
                collab_id AS CollabId,
                createdAt_time_tracker AS CreatedAt,
                updatedAt_time_tracker AS UpdatedAt,
                deletedAt_time_tracker AS DeletedAt
            FROM time_tracker
            WHERE task_id = @TaskId
            AND deletedAt_time_tracker IS NULL
            AND (
                (start_date_time_tracker <= @End AND end_date_time_tracker >= @Start)
            )";

        var parameters = new DynamicParameters();
        parameters.Add("TaskId", taskId);
        parameters.Add("Start", start);
        parameters.Add("End", end);

        if (excludeId.HasValue)
        {
            sql += " AND id_time_tracker != @ExcludeId";
            parameters.Add("ExcludeId", excludeId.Value);
        }

        return await _dbConnection.QueryAsync<TimeTracker>(sql, parameters);
    }

    public async Task AddAsync(TimeTracker entity)
    {
        if (entity.Id == Guid.Empty)
        {
            entity.Id = Guid.NewGuid();
        }

        entity.CreatedAt = DateTime.UtcNow;
        entity.UpdatedAt = DateTime.UtcNow;

        var conflictingTrackers = await GetConflictingTimeTrackers(entity.TaskId, entity.StartDateTime, entity.EndDateTime);
        if (conflictingTrackers.Any())
        {
            throw new InvalidOperationException("O intervalo de tempo fornecido colide com outro time tracker.");
        }

        var sql = @"
            INSERT INTO time_tracker (
                id_time_tracker, start_date_time_tracker, end_date_time_tracker, time_zone_id,
                task_id, collab_id, createdAt_time_tracker, updatedAt_time_tracker
            )
            VALUES (
                @Id, @StartDateTime, @EndDateTime, @TimeZoneId,
                @TaskId, @CollabId, @CreatedAt, @UpdatedAt
            )";

        var parameters = new DynamicParameters();
        parameters.Add("Id", entity.Id);
        parameters.Add("StartDateTime", entity.StartDateTime);
        parameters.Add("EndDateTime", entity.EndDateTime);
        parameters.Add("TimeZoneId", entity.TimeZoneId);
        parameters.Add("TaskId", entity.TaskId);
        parameters.Add("CollabId", entity.CollabId);
        parameters.Add("CreatedAt", entity.CreatedAt);
        parameters.Add("UpdatedAt", entity.UpdatedAt);

        await _dbConnection.ExecuteAsync(sql, parameters);
    }

    public async Task UpdateAsync(TimeTracker entity, Guid id)
    {
        var taskExistsSql = "SELECT EXISTS (SELECT 1 FROM tasks WHERE id_task = @TaskId)";
        var collabExistsSql = "SELECT EXISTS (SELECT 1 FROM collaborators WHERE id_collab = @CollabId)";

        var parameters = new DynamicParameters();
        parameters.Add("TaskId", entity.TaskId);
        parameters.Add("CollabId", entity.CollabId);

        var taskExists = await _dbConnection.ExecuteScalarAsync<bool>(taskExistsSql, parameters);
        var collabExists = await _dbConnection.ExecuteScalarAsync<bool>(collabExistsSql, parameters);

        if (!taskExists)
        {
            throw new InvalidOperationException("O TaskId fornecido não existe.");
        }

        if (!collabExists)
        {
            throw new InvalidOperationException("O CollabId fornecido não existe.");
        }

        var conflictingTrackers = await GetConflictingTimeTrackers(entity.TaskId, entity.StartDateTime, entity.EndDateTime, entity.Id);
        if (conflictingTrackers.Any())
        {
            throw new InvalidOperationException("O intervalo de tempo fornecido colide com outro time tracker.");
        }

        var sql = @"
            UPDATE time_tracker
            SET start_date_time_tracker = @StartDateTime,
                end_date_time_tracker = @EndDateTime,
                time_zone_id = @TimeZoneId,
                task_id = @TaskId,
                collab_id = @CollabId,
                updatedAt_time_tracker = @UpdatedAt,
                deletedAt_time_tracker = @DeletedAt
            WHERE id_time_tracker = @Id";

        var updateParameters = new DynamicParameters();
        updateParameters.Add("StartDateTime", entity.StartDateTime);
        updateParameters.Add("EndDateTime", entity.EndDateTime);
        updateParameters.Add("TimeZoneId", entity.TimeZoneId);
        updateParameters.Add("TaskId", entity.TaskId);
        updateParameters.Add("CollabId", entity.CollabId);
        updateParameters.Add("UpdatedAt", entity.UpdatedAt ?? DateTime.Now);
        updateParameters.Add("DeletedAt", entity.DeletedAt);
        updateParameters.Add("Id", entity.Id);

        await _dbConnection.ExecuteAsync(sql, updateParameters);
    }

    public async Task DeleteAsync(Guid id)
    {
        var sql = @"
            UPDATE time_tracker 
            SET deletedAt_time_tracker = @DeletedAt 
            WHERE id_time_tracker = @Id";

        await _dbConnection.ExecuteAsync(sql, new { Id = id, DeletedAt = DateTime.UtcNow });
    }

    public async Task StartTimeTrackerAsync(Guid id, Guid taskId, DateTime startDateTime)
    {
        var sql = @"
            UPDATE time_tracker
            SET start_date_time_tracker = @StartDateTime, 
                updatedAt_time_tracker = @UpdatedAt
            WHERE task_id = @TaskId 
            AND id_time_tracker = @Id
            AND deletedAt_time_tracker IS NULL";

        var parameters = new DynamicParameters();
        parameters.Add("TaskId", taskId);
        parameters.Add("StartDateTime", startDateTime);
        parameters.Add("UpdatedAt", DateTime.Now);
        parameters.Add("Id", id);

        await _dbConnection.ExecuteAsync(sql, parameters);
    }

    public async Task StopTimeTrackerAsync(Guid id, Guid taskId, DateTime endDateTime)
    {
        var sql = @"
            UPDATE time_tracker
            SET end_date_time_tracker = @EndDateTime, 
                updatedAt_time_tracker = @UpdatedAt
            WHERE task_id = @TaskId
            AND id_time_tracker = @Id
            AND deletedAt_time_tracker IS NULL";

        var parameters = new DynamicParameters();
        parameters.Add("TaskId", taskId);
        parameters.Add("EndDateTime", endDateTime);
        parameters.Add("UpdatedAt", DateTime.Now);
        parameters.Add("Id", id);

        await _dbConnection.ExecuteAsync(sql, parameters);
    }

}
