using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Domain.Repositories;

namespace TodoApp.Bll.Features
{
    public class DeleteTodoCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, Unit>
    {
        private readonly ITodoRepository todoRepository;

        public DeleteTodoCommandHandler(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        public async Task<Unit> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await todoRepository.GetAsync(request.Id, cancellationToken);

            todoRepository.Delete(todo);

            return Unit.Value;
        }
    }
}
