using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Infotecs.MiniJournal.Domain.Users;

namespace MiniJournal.PostgreSql
{
    internal class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory connectionFactory;

        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<User> FindByIdAsync(long id)
        {
            using (var connection = this.connectionFactory.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM users WHERE id = @id", new { id });
            }
        }

        public async Task<User> FindByNameAsync(string name)
        {
            using (var connection = this.connectionFactory.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM users WHERE name = @name", new { name });
            }
        }

        public async Task AddAsync(User user)
        {
            using (var connection = this.connectionFactory.GetConnection())
            {
                await connection.QueryFirstOrDefaultAsync<User>("INSERT INTO users (name) VALUES (@Name)", user);
            }
        }
    }
}
