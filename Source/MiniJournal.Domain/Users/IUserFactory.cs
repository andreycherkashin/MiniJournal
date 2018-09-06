using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infotecs.MiniJournal.Domain.Users
{
    public interface IUserFactory
    {
        Task<User> CreateAsync(string name);
    }
}
