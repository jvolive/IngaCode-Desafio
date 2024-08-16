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

    public async Task AddAsync(TimeTracker entity)
    {
        var query = @"
                INSERT INTO time_tracker (id_time_tracker, start_date_time_tracker, end_date_time_tracker, time_zone_id, task_id, collab_id, createdAt_time_tracker, updatedAt_time_tracker)
                VALUES (@Id, @StartDateTime, @EndDateTime, @TimeZoneId, @TaskId, @CollabId, @CreatedAt, @UpdatedAt)";

        await _dbConnection.ExecuteAsync(query, entity);
    }

    public async Task<TimeTracker> GetByIdAsync(Guid id)
    {
        var query = "SELECT * FROM time_tracker WHERE id_time_tracker = @Id";
        return await _dbConnection.QuerySingleOrDefaultAsync<TimeTracker>(query, new { Id = id });
    }

    public async Task UpdateAsync(TimeTracker entity)
    {
        var query = @"
                UPDATE time_tracker
                SET end_date_time_tracker = @EndDateTime, updatedAt_time_tracker = @UpdatedAt
                WHERE id_time_tracker = @Id";

        await _dbConnection.ExecuteAsync(query, entity);
    }
}