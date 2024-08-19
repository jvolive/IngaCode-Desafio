using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using System.Data;
using Dapper;

namespace IngaCode.Infrastructure.Repository;
public class UserRepository : IUserRepository
{
    private readonly IDbConnection _dbConnection;

    public UserRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        var query = @"
        SELECT username_user, password_user 
        FROM users 
        WHERE username_user = @Username";

        return await _dbConnection.QuerySingleOrDefaultAsync<User>(query, new { Username = username });
    }

    public async Task<bool> VerifyUserPasswordAsync(string username, string password)
    {
        var query = @"
            SELECT verify_password(password_user, @Password) 
            FROM users 
            WHERE username_user = @Username";

        var isValid = await _dbConnection.QuerySingleAsync<bool>(query, new { Username = username, Password = password });
        return isValid;
    }
}