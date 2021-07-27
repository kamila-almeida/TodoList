using TodoList.Domain;

namespace TodoList.Application.Models
{
    public class UserModel
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EProfile Profile { get; set; }
        public string Token { get; set; }
    }
}
