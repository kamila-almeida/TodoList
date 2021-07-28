﻿using System;

namespace TodoList.Application.Models
{
    public class TodoItemRegisterModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }        
    }
}
