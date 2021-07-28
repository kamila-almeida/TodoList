using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Application.Models;
using TodoList.Domain.Enums;
using TodoList.Shared;
using TodoList.Tests.Fixture;
using Xunit;

namespace TodoList.Tests.TodoItem
{
    public class UpdateTodoItemTests : IClassFixture<TodoItemFixture>
    {
        private readonly TodoItemFixture fixture;
        public UpdateTodoItemTests(TodoItemFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory, MemberData(nameof(ValidData))]
        public async Task UpdateTodoItemWithValidData(TodoItemRegisterModel todoItemRegisterModel)
        {
            // Arrange
            var todoItem = fixture.Mapper.Map<Domain.Entities.TodoItem>(todoItemRegisterModel);
            todoItem.Id = 1;

            fixture.TodoItemRepository.Setup(x => x
            .GetById(It.IsAny<int>()))
                .ReturnsAsync(() => fixture.DbTodoItems[0]);

            // Act
            var result = await fixture.TodoItemService.UpdateAsync(todoItemRegisterModel, 1);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Id.Should().NotBe(null);
        }

        [Theory, MemberData(nameof(InvalidData))]
        public async Task UpdateTodoItemWithInvalidData(TodoItemRegisterModel todoItemRegisterModel)
        {
            // Arrange
            var todoItem = fixture.Mapper.Map<Domain.Entities.TodoItem>(todoItemRegisterModel);
            todoItem.Id = 1;

            fixture.TodoItemRepository.Setup(x => x
            .GetById(It.IsAny<int>()))
                .ReturnsAsync(() => fixture.DbTodoItems[0]);

            // Act
            var result = await fixture.TodoItemService.UpdateAsync(todoItemRegisterModel, 1);

            // Assert
            result.Success.Should().BeFalse();
            result.Data.Should().BeNull();
        }

        [Theory, MemberData(nameof(ValidData))]
        public async Task UpdateTodoItemWAlreadyFinished(TodoItemRegisterModel todoItemRegisterModel)
        {
            // Arrange
            var todoItem = fixture.Mapper.Map<Domain.Entities.TodoItem>(todoItemRegisterModel);
            todoItem.Id = 1;

            fixture.TodoItemRepository.Setup(x => x
            .GetById(It.IsAny<int>()))
                .ReturnsAsync(() => fixture.DbTodoItems[1]);

            // Act
            var result = await fixture.TodoItemService.UpdateAsync(todoItemRegisterModel, 2);

            // Assert
            result.Success.Should().BeFalse();
            result.Data.Should().BeNull();
            result.Messages.Select(x => x.Description).Should().Contain(EMessages.EditItemFinished.GetEnumDescription());
        }

        [Theory, MemberData(nameof(ValidData))]
        public async Task UpdateWithUnauthorizedUser(TodoItemRegisterModel todoItemRegisterModel)
        {
            // Arrange
            var todoItem = fixture.Mapper.Map<Domain.Entities.TodoItem>(todoItemRegisterModel);
            todoItem.Id = 1;
            var userId = 2;

            fixture.TodoItemRepository.Setup(x => x
            .GetById(It.IsAny<int>()))
                .ReturnsAsync(() => fixture.DbTodoItems[0]);

            // Act
            var result = await fixture.TodoItemService.UpdateAsync(todoItemRegisterModel, userId);

            // Assert
            result.Success.Should().BeFalse();
            result.Data.Should().BeNull();
            result.Messages.Select(x => x.Description).Should().Contain(EMessages.UserNotAuthorized.GetEnumDescription());
        }

        [Theory]
        [InlineData(new object[] { 1, 1 })]
        public async Task FinishTodoItemWithSuccess(int itemId, int userId)
        {
            fixture.TodoItemRepository.Setup(x => x
            .GetById(It.IsAny<int>()))
                .ReturnsAsync(() => fixture.DbTodoItems[0]);

            // Act
            var result = await fixture.TodoItemService.FinishItemAsync(itemId, userId);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Id.Should().NotBe(null);
        }

        [Theory]
        [InlineData(new object[] { 1, 2 })]
        public async Task FinishTodoItemAlreadyFinished(int itemId, int userId)
        {
            fixture.TodoItemRepository.Setup(x => x
            .GetById(It.IsAny<int>()))
                .ReturnsAsync(() => fixture.DbTodoItems[1]);

            // Act
            var result = await fixture.TodoItemService.FinishItemAsync(itemId, userId);

            // Assert
            result.Success.Should().BeFalse();
            result.Data.Should().BeNull();
            result.Messages.Select(x=>x.Description).Should().Contain(EMessages.ItemAlreadyFinished.GetEnumDescription());
        }

        public static IEnumerable<object[]> ValidData
        {
            get
            {
                return new[]
                {
                    //Updating description
                    new object[]
                    {
                        new TodoItemRegisterModel()
                        {
                            Id = 1,
                            Description = "Task Test",
                            DueDate = new DateTime(2021,01,30)
                        }
                    },
                    //Updating due date
                    new object[]
                    {
                        new TodoItemRegisterModel()
                        {
                            Id = 1,
                            Description = "Task Test",
                            DueDate = new DateTime(2021,02,05)
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
                            Id = 1,
                            Description = "Task Test"
                        }
                    }
                };
            }
        }
    }
}
