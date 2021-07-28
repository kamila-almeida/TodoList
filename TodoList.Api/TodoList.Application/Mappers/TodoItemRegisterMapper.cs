using AutoMapper;
using TodoList.Application.Models;
using TodoList.Domain.Entities;

namespace TodoList.Application.Mappers
{
    public class TodoItemRegisterMapper : Profile
    {
        public TodoItemRegisterMapper()
        {
            CreateMap<TodoItem, TodoItemRegisterModel>()
                .ForMember(p => p.Id, p => p.MapFrom(x => x.Id))
                .ForMember(p => p.Description, p => p.MapFrom(x => x.Description))
                .ForMember(p => p.DueDate, p => p.MapFrom(x => x.DueDate))
                .ReverseMap(); 
        }
    }
}
