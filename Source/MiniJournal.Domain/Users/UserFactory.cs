using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Users.Exceptions;

namespace Infotecs.MiniJournal.Domain.Users
{
    internal class UserFactory : IUserFactory
    {
        public Task<User> CreateAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new EmptyUserNameException();
            }

            var user = new User(name);

            return Task.FromResult(user);
        }
    }
}
