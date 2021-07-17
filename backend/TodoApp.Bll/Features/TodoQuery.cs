using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Domain.Repositories;
using TodoApp.Domain.Utilities.Paging;

namespace TodoApp.Bll.Features
{
    public class TodoQuery : PagedQuery, IRequest<PagedList<TodoItem>>
    {
    }

    public class TodoItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
    }

    public class TodoQueryHandler : IRequestHandler<TodoQuery, PagedList<TodoItem>>
    {
        private readonly ITodoRepository todoRepository;

        public TodoQueryHandler(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        public async Task<PagedList<TodoItem>> Handle(TodoQuery request, CancellationToken cancellationToken)
        {
            var todos = await todoRepository.GetTodosAsync(
                filter: null,
                todo => new TodoItem
                {
                    Id = todo.Id,
                    Description = todo.Description,
                    IsComplete = todo.IsComplete
                },
                request.PageIndex,
                request.PageSize,
                cancellationToken);

            return todos;
        }
    }
}
