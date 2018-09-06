using System.Threading.Tasks;

namespace Infotecs.MiniJournal.Domain.Users
{
    public interface IUserDomainService
    {
        Task CreateUserAsync(User user);
        Task<User> GetUserByNameAsync(string name);
        Task<User> GetUserByIdAsync(long userId);
    }
}