using System.Threading.Tasks;
using TodoList.Domain.Entities;

namespace TodoList.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string email, string password);
        Task CreateUser(User user);
    }
}
