using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Domain;
using TodoList.Shared;
using TodoList.Tests.Fixture;
using Xunit;

namespace TodoList.Tests.TodoItem
{
    public class DeleteTodoItemTests : IClassFixture<TodoItemFixture>
    {
        private readonly TodoItemFixture fixture;
        public DeleteTodoItemTests(TodoItemFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory, MemberData(nameof(ValidData))]
        public async Task DeleteWithSuccess(Domain.Entities.TodoItem todoItem)
        {
            // Arrange
            var todoItemId = todoItem.Id;
            var userId = todoItem.UserId;

            fixture.TodoItemRepository.Setup(x => x
            .GetById(It.IsAny<int>()))
                .ReturnsAsync(() => todoItem);

            // Act
            var result = await fixture.TodoItemService.DeleteAsync(todoItemId, userId);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().BeNull();
        }

        [Theory, MemberData(nameof(ValidData))]
        public async Task DeleteWithUnauthorizedUser(Domain.Entities.TodoItem todoItem)
        {
            // Arrange
            var todoItemId = 1;
            var userId = 2;

            fixture.TodoItemRepository.Setup(x => x
            .GetById(It.IsAny<int>()))
                .ReturnsAsync(() => todoItem);

            // Act
            var result = await fixture.TodoItemService.DeleteAsync(todoItemId, userId);

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
                        }
                    }
                };
            }
        }
    }
}
