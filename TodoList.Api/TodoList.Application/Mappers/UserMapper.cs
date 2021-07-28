using AutoMapper;
using TodoList.Application.Models;
using TodoList.Domain.Entities;

namespace TodoList.Application.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserModel>()
                .ForMember(p => p.Id, p => p.MapFrom(x => x.Id))
                .ForMember(p => p.Email, p => p.MapFrom(x => x.Email))
                .ForMember(p => p.Password, p => p.MapFrom(x => x.Password))
                .ForMember(p => p.Profile, p => p.MapFrom(x => x.Profile))
                .ForMember(p => p.Token, p => p.Ignore())
                .ReverseMap();

            CreateMap<User, LoginModel>()
                .ForMember(p => p.Email, p => p.MapFrom(x => x.Email))
                .ForMember(p => p.Password, p => p.MapFrom(x => x.Password))
                .ReverseMap();
        }        
    }
}
