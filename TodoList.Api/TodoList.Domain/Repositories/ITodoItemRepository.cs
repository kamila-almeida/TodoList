using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Domain.Entities;

namespace TodoList.Domain.Repositories
{
    public interface ITodoItemRepository
    {
        Task InsertAsync(TodoItem todoItem);
        Task UpdateAsync(TodoItem todoItem);
        Task DeleteAsync(TodoItem todoItem);
        Task<List<TodoItem>> ListAsync();
        Task<List<TodoItem>> ListByUserAsync(int userId);
        Task<TodoItem> GetById(int id);
    }
}
