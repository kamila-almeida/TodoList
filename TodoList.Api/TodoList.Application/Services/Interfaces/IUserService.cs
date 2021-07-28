using System.Threading.Tasks;
using TodoList.Application.Models;

namespace TodoList.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseModel<UserModel>> Authenticate(LoginModel loginModel);
        Task<BaseModel<UserRegisterModel>> CreateUserAsync(UserRegisterModel userModel);
        int GetUserId(string email);
    }
}
