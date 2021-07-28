using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Domain.Entities;
using TodoList.Domain.Repositories;

namespace TodoList.Infra.Data.Repository
{
    public class TodoItemRepository : ITodoItemRepository
    {
        protected internal readonly DatabaseContext _context;

        public TodoItemRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(TodoItem todoItem)
        {
            await _context.Set<TodoItem>().AddAsync(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TodoItem todoItem)
        {
            _context.Set<TodoItem>().Update(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TodoItem todoItem)
        {
            _context.Set<TodoItem>().Remove(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TodoItem>> ListAsync()
        {
            return await _context.TodoItem.Include(x=>x.User).AsNoTracking().ToListAsync();
        }

        public async Task<List<TodoItem>> ListByUserAsync(int userId)
        {
            return await _context.TodoItem.Where(x=>x.UserId == userId).ToListAsync();
        }

        public async Task<TodoItem> GetById(int id)
        {
            return await _context.TodoItem.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
