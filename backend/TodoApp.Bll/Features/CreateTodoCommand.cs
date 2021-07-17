using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Domain.Model;
using TodoApp.Domain.Repositories;

namespace TodoApp.Bll.Features
{
    public class CreateTodoCommand : IRequest<int>
    {
        public string Description { get; set; }
    }

    public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
    {
        public CreateTodoCommandValidator()
        {
            RuleFor(command => command.Description).NotEmpty();
        }
    }

    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, int>
    {
        private readonly ITodoRepository todoRepository;

        public CreateTodoCommandHandler(
            ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        public Task<int> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = new Todo(request.Description);

            todoRepository.Insert(todo);

            return Task.FromResult(todo.Id);
        }
    }
}
