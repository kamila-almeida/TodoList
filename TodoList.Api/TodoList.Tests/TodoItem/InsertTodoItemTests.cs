using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Application.Models;
using TodoList.Tests.Fixture;
using Xunit;

namespace TodoList.Tests.TodoItem
{
    public class InsertTodoItemTests : IClassFixture<TodoItemFixture>
    {
        private readonly TodoItemFixture fixture;
        public InsertTodoItemTests(TodoItemFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory, MemberData(nameof(ValidData))]
        public async Task InsertTodoItemWithValidData(TodoItemRegisterModel todoItemRegisterModel)
        {
            // Act
            var result = await fixture.TodoItemService.InsertAsync(todoItemRegisterModel, 1);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Id.Should().NotBe(null);
        }

        [Theory, MemberData(nameof(InvalidData))]
        public async Task InsertTodoItemWithInvalidData(TodoItemRegisterModel todoItemRegisterModel)
        {
            // Act
            var result = await fixture.TodoItemService.InsertAsync(todoItemRegisterModel, 1);

            // Assert
            result.Success.Should().BeFalse();
            result.Data.Should().BeNull();
        }

        public static IEnumerable<object[]> ValidData
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        new TodoItemRegisterModel()
                        {
                            Id = 1,
                            Description = "Task 1",                            
                            DueDate = new DateTime(2021,01,30)                            
                        }
                    }
                };
            }
        }

        public static IEnumerable<object[]> InvalidData
        {
            get
            {
                return new[]
                {
                    //No description
                    new object[]
                    {
                        new TodoItemRegisterModel()
                        {
                            Id = 1,
                            Description = "",
                            DueDate = new DateTime(2021,01,30)
                        }
                    },
                    //No due date
                    new object[]
                    {
                        new TodoItemRegisterModel()
                        {
                            Id = 2,
                            Description = "Task 1"
                        }
                    },                    
                };
            }
        }
    }
}
