using AutoMapper;
using TodoList.Application.Models;
using TodoList.Domain.Entities;

namespace TodoList.Application.Mappers
{
    public class TodoItemResultMapper : Profile
    {
        public TodoItemResultMapper()
        {
            CreateMap<TodoItem, TodoItemResultModel>()
                .ForMember(t => t.Id, t => t.MapFrom(x => x.Id))
                .ForMember(t => t.User, t => t.MapFrom(x => x.User))
                .ForMember(t => t.Description, t => t.MapFrom(x => x.Description))
                .ForMember(t => t.DueDate, t => t.MapFrom(x => x.DueDate))
                .ReverseMap();
        }
    }
}
