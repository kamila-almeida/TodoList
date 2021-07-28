using System;

namespace TodoList.Application.Models
{
    public class TodoItemResultModel
    {
        public int Id { get; set; }
        public UserResultModel User { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
