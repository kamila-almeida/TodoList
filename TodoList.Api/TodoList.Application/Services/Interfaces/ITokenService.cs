using TodoList.Application.Models;

namespace TodoList.Application.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserModel user);
    }
}
