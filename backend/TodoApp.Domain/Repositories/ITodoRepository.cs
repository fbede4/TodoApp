using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Domain.Model;
using TodoApp.Domain.Utilities.Paging;

namespace TodoApp.Domain.Repositories
{
    public interface ITodoRepository
    {
        Todo Insert(Todo todo);

        Task<List<Todo>> GetTodosAsync(
            Expression<Func<Todo, bool>> filter = null,
            CancellationToken cancellationToken = default);

        Task<PagedList<TResult>> GetTodosAsync<TResult>(
            Expression<Func<Todo, bool>> filter,
            Expression<Func<Todo, TResult>> selector,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken = default);
    }
}
