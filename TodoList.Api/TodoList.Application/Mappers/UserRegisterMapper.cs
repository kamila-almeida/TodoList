using AutoMapper;
using TodoList.Application.Models;
using TodoList.Domain.Entities;

namespace TodoList.Application.Mappers
{
    public class UserRegisterMapper : Profile
    {
        public UserRegisterMapper()
        {
            CreateMap<User, UserRegisterModel>()
                .ForMember(p => p.Email, p => p.MapFrom(x => x.Email))
                .ForMember(p => p.Password, p => p.MapFrom(x => x.Password))
                .ReverseMap();
        }
    }
}
