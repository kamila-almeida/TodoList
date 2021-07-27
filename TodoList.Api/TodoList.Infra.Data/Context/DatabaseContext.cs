using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
using TodoList.Infra.Data.Configuration;

namespace TodoList.Infra.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<TodoItem> TodoList { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("TodoListDB");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TodoItemConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
