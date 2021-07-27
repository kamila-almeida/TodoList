using System.ComponentModel;

namespace TodoList.Domain.Enums
{
    public enum EMessages
    {
        [Description("Operation completed successfully.")]
        Success = 1,
        [Description("Invalid credentials.")]
        InvalidCredentials = 2
    }
}
