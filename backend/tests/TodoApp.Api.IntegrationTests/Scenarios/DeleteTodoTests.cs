using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;
using TodoApp.Dal;
using TodoApp.Domain.Model;
using Xunit;

namespace TodoApp.Api.IntegrationTests.Scenarios
{
    public class DeleteTodoTests : IClassFixture<TestWebApplicationFactory>
    {
        protected readonly WebApplicationFactory<Startup> factory;
        protected readonly HttpClient httpClient;

        public DeleteTodoTests()
        {
            factory = new TestWebApplicationFactory();
            httpClient = factory.CreateClient();
        }

        [Fact]
        public async Task DeleteTodoEndpoint_ShouldReturnOkAndDeleteTodo_WhenIdExists()
        {
            // Arrange
            var todo = new Todo("Some description");

            using (var scope = factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TodoAppDbContext>();
                dbContext.Todos.Add(todo);
                await dbContext.SaveChangesAsync();
            }

            // Act
            var httpResponseMessage = await httpClient.DeleteAsync($"api/todos/{todo.Id}");

            // Assert
            httpResponseMessage.StatusCode.Should().Be(200);

            using (var scope = factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TodoAppDbContext>();

                var todoExists = await dbContext.Todos.AnyAsync(t => t.Id == todo.Id);
                todoExists.Should().Be(false);
            }
        }

        [Fact]
        public async Task DeleteTodoEndpoint_ShouldReturnNotFound_WhenIdDoesNotExist()
        {
            // Arange
            var nonExistentId = 14;

            // Act
            var httpResponseMessage = await httpClient.DeleteAsync($"todos/{nonExistentId}");

            // Assert
            httpResponseMessage.StatusCode.Should().Be(404);
        }
    }
}
