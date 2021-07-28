using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Domain.Entities;
using TodoList.Domain.Repositories;

namespace TodoList.Infra.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        protected internal readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            return await _context.User.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();    
        }

        public async Task CreateUser(User user)
        {
            _context.Set<User>().Add(user);
            await _context.SaveChangesAsync();
        }

        public int GetUserIdByEmail(string email)
        {
           return _context.User.Where(x => x.Email == email).Select(x=>x.Id).FirstOrDefault();
        }
    }
}
