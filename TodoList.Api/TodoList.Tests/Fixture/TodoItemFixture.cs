using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using TodoList.Application.Models;
using TodoList.Application.Services;
using TodoList.Application.Services.Interfaces;
using TodoList.Domain;
using TodoList.Domain.Repositories;
using TodoList.Shared;

namespace TodoList.Tests.Fixture
{
    public class TodoItemFixture : IDisposable
    {
        public IMapper Mapper { get; set; }
        public ITodoItemService TodoItemService { get; set; }
        public Mock<ITodoItemRepository> TodoItemRepository { get; set; }

        public List<Domain.Entities.TodoItem> DbTodoItems = new List<Domain.Entities.TodoItem>()
        {
            new Domain.Entities.TodoItem()
            {
                Id = 1,
                Description = "Task 1",
                CreationDate = new DateTime(2021,01,01),
                DueDate = new DateTime(2021,01,30),
                UserId = 1,
                User = new Domain.Entities.User
                {
                    Id = 1,
                    Email = "test_adm@ubistart.com",
                    Password =  Md5HashExtensions.CreateMD5("123456"),
                    Profile = EProfile.Administrator
                }
            },
            new Domain.Entities.TodoItem()
            {
                Id = 1,
                Description = "Task 2",
                CreationDate = new DateTime(2021,01,01),
                DueDate = new DateTime(2021,01,30),
                EndDate = new DateTime(2021,01,05),
                UserId = 2,
                User = new Domain.Entities.User
                {
                    Id = 2,
                    Email = "test_client@ubistart.com",
                    Password =  Md5HashExtensions.CreateMD5("abcdef"),
                    Profile = EProfile.Client
                }
            },
            new Domain.Entities.TodoItem()
            {
                Id = 1,
                Description = "Task 3",
                CreationDate = new DateTime(2021,01,01),
                DueDate = new DateTime(2021,01,30),
                UserId = 2,
                User = new Domain.Entities.User
                {
                    Id = 2,
                    Email = "test_client@ubistart.com",
                    Password =  Md5HashExtensions.CreateMD5("abcdef"),
                    Profile = EProfile.Client
                }
            },
            new Domain.Entities.TodoItem()
            {
                Id = 1,
                Description = "Task 4",
                CreationDate = new DateTime(2021,01,01),
                DueDate = new DateTime(2021,01,30),
                UserId = 2,
                User = new Domain.Entities.User
                {
                    Id = 2,
                    Email = "test_client@ubistart.com",
                    Password =  Md5HashExtensions.CreateMD5("abcdef"),
                    Profile = EProfile.Client
                }
            }
        };

        public TodoItemFixture()
        {
            var config = new MapperConfiguration(cfg => cfg.AddMaps(
                new Type[] {
                    typeof(TodoItemModel),
                    typeof(TodoItemRegisterModel),
                    typeof(TodoItemResultModel)}
                )
            );
            Mapper = config.CreateMapper();

            TodoItemRepository = new Mock<ITodoItemRepository>();
            TodoItemService = new TodoItemService(Mapper, TodoItemRepository.Object);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~TodoItemFixture()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbTodoItems = null;
            }
        }
    }
}

