using System;

namespace TodoList.Domain.Entities
{
    public class TodoItem
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime DueDate { get; set; }
        public User User { get; set; }
    }
}
