using System;
using System.Collections.Generic;
using AutoMapper;
using Moq;
using TodoList.Application.Models;
using TodoList.Application.Services;
using TodoList.Application.Services.Interfaces;
using TodoList.Domain;
using TodoList.Domain.Repositories;
using TodoList.Shared;

namespace TodoList.Tests.Fixture
{
    public class UserFixture : IDisposable
    {
        public IMapper Mapper { get; set; }
        public IUserService UserService { get; set; }
        public Mock<IUserRepository> UserRepository { get; set; }

        public List<Domain.Entities.User> DbUsers = new List<Domain.Entities.User>()
        {
            new Domain.Entities.User()
            {
                Id = 1,
                Email = "test_adm@ubistart.com",
                Password =  Md5HashExtensions.CreateMD5("123456"),
                Profile = EProfile.Administrator
            },
            new Domain.Entities.User()
            {
                Id = 2,
                Email = "test_client@ubistart.com",
                Password =  Md5HashExtensions.CreateMD5("abcdef"),
                Profile = EProfile.Client
            },
        };

        public UserFixture()
        {
            var config = new MapperConfiguration(cfg => cfg.AddMaps(
                new Type[] {
                    typeof(UserModel),
                    typeof(LoginModel),
                    typeof(UserRegisterModel),
                    typeof(UserResultModel)}
                )
            );
            Mapper = config.CreateMapper();

            UserRepository = new Mock<IUserRepository>();
            var tokenService = new Mock<ITokenService>();
            UserService = new UserService(Mapper, UserRepository.Object, tokenService.Object);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        ~UserFixture()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbUsers = null;
            }
        }
    }
}
