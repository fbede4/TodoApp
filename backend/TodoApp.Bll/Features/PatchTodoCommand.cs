using FluentValidation;
using MediatR;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Domain.Repositories;

namespace TodoApp.Bll.Features
{
    public class PatchTodoCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Description { get; set; }

        public bool? IsComplete { get; set; }
    }

    public class PatchTodoCommandValidator : AbstractValidator<PatchTodoCommand>
    {
        public PatchTodoCommandValidator()
        {
            // as it is a patch endpoint, we only validate non-null values
            RuleFor(command => command.Description).NotEmpty().When(command => command.Description != null);
        }
    }

    public class PatchTodoCommandHandler : IRequestHandler<PatchTodoCommand, Unit>
    {
        private readonly ITodoRepository todoRepository;

        public PatchTodoCommandHandler(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        public async Task<Unit> Handle(PatchTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await todoRepository.GetAsync(request.Id, cancellationToken);

            todo.IsComplete = request.IsComplete ?? todo.IsComplete;
            todo.Description = request.Description ?? todo.Description;

            todoRepository.Update(todo);

            return Unit.Value;
        }
    }
}
