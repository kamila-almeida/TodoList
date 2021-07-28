using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Application.Models;

namespace TodoList.Application.Services.Interfaces
{
    public interface ITodoItemService
    {
        Task<BaseModel<TodoItemModel>> InsertAsync(TodoItemRegisterModel todoItemRegisterModel, int userId);
        Task<BaseModel<TodoItemModel>> UpdateAsync(TodoItemRegisterModel todoItemRegisterModel, int userId);
        Task<BaseModel<TodoItemModel>> FinishItemAsync(int itemId, int userId);
        Task<BaseModel<TodoItemModel>> DeleteAsync(int id, int userId);
        Task<BaseModel<List<TodoItemResultModel>>> ListAsync(int page, int pageSize, bool? delayedItems);
        Task<BaseModel<List<TodoItemModel>>> ListByUserAsync(int userId);
    }
}
