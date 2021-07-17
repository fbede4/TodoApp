using NSubstitute;
using System.Threading.Tasks;
using TodoApp.Bll.Features;
using TodoApp.Domain.Model;
using TodoApp.Domain.Repositories;
using Xunit;

namespace TodoApp.Bll.UnitTests
{
    public class PatchTodoCommandHandlerTests
    {
        private readonly PatchTodoCommandHandler sut;
        private readonly ITodoRepository todoRepository = Substitute.For<ITodoRepository>();

        public PatchTodoCommandHandlerTests()
        {
            sut = new PatchTodoCommandHandler(todoRepository);
        }

        [Fact]
        public async Task PatchTodoCommandHandler_ShouldPatchDescriptionOnly_WhenDescriptionIsNotNullAndIsCompleteIsNull()
        {
            // Arrange
            var todoId = 1;

            var patchTodoCommand = new PatchTodoCommand
            {
                Id = todoId,
                Description = "Modified description",
                IsComplete = null
            };

            var todoEntity = new Todo("Original description")
            {
                Id = todoId
            };

            todoRepository.GetAsync(patchTodoCommand.Id).Returns(todoEntity);

            // Act
            await sut.Handle(patchTodoCommand, default);

            // Assert
            todoRepository.Received().Update(Arg.Is<Todo>(todo =>
                todo.Id == patchTodoCommand.Id
                && todo.Description == patchTodoCommand.Description
                && todo.IsComplete == todoEntity.IsComplete));
        }

        [Fact]
        public async Task PatchTodoCommandHandler_ShouldPatchIsCompleteOnly_WhenIsCompleteIsNotNullAndDescriptionIsNull()
        {
            // Arrange
            var todoId = 1;

            var patchTodoCommand = new PatchTodoCommand
            {
                Id = todoId,
                Description = null,
                IsComplete = true
            };

            var todoEntity = new Todo("Original description")
            {
                Id = todoId
            };

            todoRepository.GetAsync(patchTodoCommand.Id).Returns(todoEntity);

            // Act
            await sut.Handle(patchTodoCommand, default);

            // Assert
            todoRepository.Received().Update(Arg.Is<Todo>(todo =>
                todo.Id == patchTodoCommand.Id
                && todo.IsComplete == patchTodoCommand.IsComplete
                && todo.Description == todoEntity.Description));
        }
    }
}
