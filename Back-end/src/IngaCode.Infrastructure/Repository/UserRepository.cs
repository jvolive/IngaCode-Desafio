using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using System.Data;
using Dapper;

namespace IngaCode.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AddAsync(User entity)
        {
            var query = @"
                INSERT INTO users (id_user, username_user, password_user, createdAt_user, updatedAt_user, deletedAt_user)
                VALUES (@Id, @UserName, @Password, @CreatedAt, @UpdatedAt, @DeletedAt)";

            await _dbConnection.ExecuteAsync(query, entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var query = "DELETE FROM users WHERE id_user = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var query = "SELECT * FROM users";
            return await _dbConnection.QueryAsync<User>(query);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var query = "SELECT * FROM users WHERE id_user = @Id";
            return await _dbConnection.QuerySingleOrDefaultAsync<User>(query, new { Id = id });
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var query = "SELECT * FROM users WHERE username_user = @UserName";
            return await _dbConnection.QuerySingleOrDefaultAsync<User>(query, new { UserName = username });
        }

        public async Task UpdateAsync(User entity)
        {
            var query = @"
                UPDATE users
                SET username_user = @UserName, password_user = @Password, createdAt_user = @CreatedAt,
                    updatedAt_user = @UpdatedAt, deletedAt_user = @DeletedAt
                WHERE id_user = @Id";

            await _dbConnection.ExecuteAsync(query, entity);
        }
    }
}
