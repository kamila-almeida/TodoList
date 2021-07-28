using System.ComponentModel;

namespace TodoList.Domain.Enums
{
    public enum EMessages
    {
        [Description("Operation completed successfully.")]
        Success = 1,
        [Description("Invalid credentials.")]
        InvalidCredentials = 2,
        [Description("Todo item Id must be informed.")]
        InvalidTodoItemId = 3,
        [Description("Another user for that e-mail already exists. Try with another one.")]
        UserAlreadyExists = 4,
        [Description("It's not possible to edit a finished item.")]
        EditItemFinished = 5,
        [Description("This item is already finished.")]
        ItemAlreadyFinished = 6,
        [Description("You are not authorized to perform this operation.")]
        UserNotAuthorized = 7,
        [Description("Unknown error. Try again later.")]
        InternalError = 99
    }
}
