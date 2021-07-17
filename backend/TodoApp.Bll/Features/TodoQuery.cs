using LinqKit;
using MediatR;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Domain.Model;
using TodoApp.Domain.Repositories;
using TodoApp.Domain.Utilities.Paging;

namespace TodoApp.Bll.Features
{
    public class TodoQuery : PagedQuery, IRequest<PagedList<TodoItem>>
    {
        public string SearchTerm { get; set; }
        public bool ShowCompletedTodos { get; set; }
        public bool ShowIncompleteTodos { get; set; }
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
            Expression<Func<Todo, bool>> filter = todo => true;

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                filter = filter.And(todo => todo.Description.ToLower().Contains(request.SearchTerm.ToLower()));
            }

            if (!request.ShowCompletedTodos)
            {
                filter = filter.And(todo => !todo.IsComplete);
            }

            if (!request.ShowIncompleteTodos)
            {
                filter = filter.And(todo => todo.IsComplete);
            }

            var todos = await todoRepository.GetTodosAsync(
                filter: filter,
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
