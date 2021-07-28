using AutoMapper;
using TodoList.Application.Models;
using TodoList.Domain.Entities;

namespace TodoList.Application.Mappers
{
    public class UserResultMapper : Profile
    {
        public UserResultMapper()
        {
            CreateMap<User, UserResultModel>()
                .ForMember(p => p.Id, p => p.MapFrom(x => x.Id))
                .ForMember(p => p.Email, p => p.MapFrom(x => x.Email))
                .ReverseMap();
        }
    }
}
