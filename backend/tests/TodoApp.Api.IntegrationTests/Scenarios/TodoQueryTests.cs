using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Api.IntegrationTests.Extensions;
using TodoApp.Bll.Features;
using TodoApp.Dal;
using TodoApp.Domain.Model;
using TodoApp.Domain.Utilities.Paging;
using Xunit;

namespace TodoApp.Api.IntegrationTests.Scenarios
{
    public class TodoQueryTests : TestBase
    {
        public override string EndpointRoute => "api/todos";
        private readonly IFixture fixture = new Fixture();

        [Fact]
        public async Task TodoQueryEndpoint_ShouldReturnTheRequestedAmounOfTodos_WhenThatManyTodosExist()
        {
            // Arrange
            var testTodos = fixture.Build<Todo>()
                .CreateMany(10);

            using (var scope = factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TodoAppDbContext>();
                dbContext.Todos.AddRange(testTodos);
                await dbContext.SaveChangesAsync();
            }

            var todoQuery = new TodoQuery
            {
                PageIndex = 0,
                PageSize = 10,
                SearchTerm = "",
                ShowCompletedTodos = true,
                ShowIncompleteTodos = true
            };

            // Act
            var httpResponseMessage = await httpClient.GetAsync($"{EndpointRoute}?{todoQuery.ToQueryString()}");

            // Assert
            httpResponseMessage.StatusCode.Should().Be(200);

            var todosPagedList = await httpResponseMessage.ConvertAsync<PagedList<TodoItem>>();

            foreach (var todo in testTodos)
            {
                todosPagedList.Items.Should().Contain(item => 
                    item.Id == todo.Id
                    && item.Description == todo.Description
                    && item.IsComplete == todo.IsComplete);
            }
            todosPagedList.TotalCount.Should().Be(testTodos.Count());
            todosPagedList.PageSize.Should().Be(todoQuery.PageSize);
            todosPagedList.PageIndex.Should().Be(todoQuery.PageIndex);
        }
    }
}