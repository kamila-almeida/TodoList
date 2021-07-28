using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Application.Models;
using TodoList.Tests.Fixture;
using Xunit;

namespace TodoList.Tests.TodoItem
{
    public class ListTodoItemTests : IClassFixture<TodoItemFixture>
    {
        private readonly TodoItemFixture fixture;
        public ListTodoItemTests(TodoItemFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task ListAllTodoItems()
        {
            // Arrange
            fixture.TodoItemRepository.Setup(x => x
            .ListAsync()).ReturnsAsync(() => fixture.DbTodoItems);

            var map = fixture.Mapper.Map<List<TodoItemResultModel>>(fixture.DbTodoItems);

            // Act
            var result = await fixture.TodoItemService.ListAsync(1, 10, null);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(map);
        }

        [Fact]
        public async Task ListByUser()
        {
            // Arrange
            fixture.TodoItemRepository.Setup(x => x
            .ListByUserAsync(2)).ReturnsAsync(() => fixture.DbTodoItems.Where(x=>x.UserId == 2).ToList());

            var map = fixture.Mapper.Map<List<TodoItemModel>>(fixture.DbTodoItems.Where(x => x.UserId == 2).ToList());
            map.ForEach(x => x.Delayed = x.DueDate.Date < DateTime.Now.Date);

            // Act
            var result = await fixture.TodoItemService.ListByUserAsync(2);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(map);
        }

        [Fact]
        public async Task ListAllDelayed()
        {
            // Arrange
            fixture.TodoItemRepository.Setup(x => x
            .ListAsync()).ReturnsAsync(() => fixture.DbTodoItems);
            var delayedItems = true;

            var map = fixture.Mapper.Map<List<TodoItemResultModel>>(fixture.DbTodoItems
                .Where(x => x.DueDate.Date < DateTime.Now.Date == delayedItems && x.EndDate == null));

            // Act
            var result = await fixture.TodoItemService.ListAsync(1, 10, delayedItems);

            // Assert
            result.Success.Should().BeTrue();
            result.Data.Should().BeEquivalentTo(map);
        }
    }
}
