using AutoMapper;
using TodoList.Application.Models;
using TodoList.Domain.Entities;

namespace TodoList.Application.Mappers
{
    public class TodoItemMapper : Profile
    {
        public TodoItemMapper()
        {
            CreateMap<TodoItem, TodoItemModel>()
                .ForMember(t => t.Id, t => t.MapFrom(x => x.Id))
                .ForMember(t => t.Description, t => t.MapFrom(x => x.Description))
                .ForMember(t => t.CreationDate, t => t.MapFrom(x => x.CreationDate))
                .ForMember(t => t.UpdateDate, t => t.MapFrom(x => x.UpdateDate))
                .ForMember(t => t.EndDate, t => t.MapFrom(x => x.EndDate))
                .ForMember(t => t.DueDate, t => t.MapFrom(x => x.DueDate))
                .ForMember(t => t.UserId, t => t.MapFrom(x => x.UserId))
                .ReverseMap();            
        }
    }
}
