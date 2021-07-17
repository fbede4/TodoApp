using NSubstitute;
using System.Threading.Tasks;
using TodoApp.Bll.Features;
using TodoApp.Domain.Model;
using TodoApp.Domain.Repositories;
using Xunit;

namespace TodoApp.Bll.UnitTests
{
    public class CreateTodoCommandHandlerTests
    {
        private readonly CreateTodoCommandHandler sut;
        private readonly ITodoRepository todoRepository = Substitute.For<ITodoRepository>();

        public CreateTodoCommandHandlerTests()
        {
            sut = new CreateTodoCommandHandler(todoRepository);
        }

        [Fact]
        public async Task CreateTodoCommandHandler_ShouldInsertTodo_Always()
        {
            // Arrange
            var createTodoCommand = new CreateTodoCommand
            {
                Description = "Test todo"
            };

            // Act
            await sut.Handle(createTodoCommand, default);

            // Assert
            todoRepository.Received().Insert(Arg.Is<Todo>(todo => 
                todo.Description == createTodoCommand.Description
                && todo.IsComplete == false
                && todo.Id == 0));
        }
    }
}
