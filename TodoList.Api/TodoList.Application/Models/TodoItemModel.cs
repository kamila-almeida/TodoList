using System;

namespace TodoList.Application.Models
{
    public class TodoItemModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool Delayed { get; set; }
        public int UserId { get; set; }
    }
}
