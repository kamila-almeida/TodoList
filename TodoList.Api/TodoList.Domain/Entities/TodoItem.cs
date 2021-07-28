using System;

namespace TodoList.Domain.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime DueDate { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
