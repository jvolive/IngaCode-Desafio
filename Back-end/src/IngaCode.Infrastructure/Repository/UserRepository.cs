using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using System.Data;
using Dapper;

namespace IngaCode.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDbConnection _dbConnection;

    public UserRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        var query = "SELECT * FROM users WHERE username = @Username";
        return await _dbConnection.QuerySingleOrDefaultAsync<User>(query, new { Username = username });
    }
}
