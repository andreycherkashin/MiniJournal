using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infotecs.MiniJournal.Domain.Users
{
    public interface IUserRepository
    {
        Task<User> FindByIdAsync(long id);
        Task<User> FindByNameAsync(string name);
        Task AddAsync(User user);
    }
}
