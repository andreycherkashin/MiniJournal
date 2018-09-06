using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Infotecs.MiniJournal.Domain.Users.Exceptions;

namespace Infotecs.MiniJournal.Domain.Users
{
    internal class UserDomainService : IUserDomainService
    {
        private readonly IUserRepository userRepository;

        public UserDomainService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new EmptyUserNameException();

            var user = await this.userRepository.FindByNameAsync(name);

            if (user == null)
            {
                throw new UserNotFoundException();                
            }

            return user;
        }

        public async Task<User> GetUserByIdAsync(long userId)
        {
            var user = await this.userRepository.FindByIdAsync(userId);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }

        public async Task CreateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (await this.userRepository.FindByNameAsync(user.Name) != null)
            {
                throw new DuplicateUserNameException();
            }

            await this.userRepository.AddAsync(user);
        }
    }
}
