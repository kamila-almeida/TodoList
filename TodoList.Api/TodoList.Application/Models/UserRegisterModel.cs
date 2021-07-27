using System;
using System.Collections.Generic;
using System.Text;
using TodoList.Domain;

namespace TodoList.Application.Models
{
    public class UserRegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public EProfile Profile { get; set; }
    }
}
