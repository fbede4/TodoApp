using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Domain.Model;
using TodoApp.Domain.Repositories;
using TodoApp.Domain.UoW;

namespace TodoApp.Bll.Features
{
    public class TodoCreateCommand : IRequest<int>
    {
        public string Description { get; set; }
    }

    public class TodoCreateCommandHandler : IRequestHandler<TodoCreateCommand, int>
    {
        private readonly ITodoRepository todoRepository;
        private readonly IUnitOfWork unitOfWork;

        public TodoCreateCommandHandler(
            ITodoRepository todoRepository,
            IUnitOfWork unitOfWork)
        {
            this.todoRepository = todoRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(TodoCreateCommand request, CancellationToken cancellationToken)
        {
            var todo = new Todo(request.Description);

            todoRepository.Insert(todo);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return todo.Id;
        }
    }
}
