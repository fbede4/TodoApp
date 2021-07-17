using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Domain.Exceptions;
using TodoApp.Domain.Model;
using TodoApp.Domain.Repositories;
using TodoApp.Domain.Utilities.Paging;

namespace TodoApp.Dal.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DbSet<Todo> dbSet;

        public TodoRepository(TodoAppDbContext todoAppDbContext)
        {
            this.dbSet = todoAppDbContext.Set<Todo>();
        }

        public async Task<Todo> GetAsync(int id, CancellationToken cancellationToken = default)
        {
            var todo = await dbSet.SingleOrDefaultAsync(todo => todo.Id == id, cancellationToken);

            return todo ?? throw new EntityNotFoundException(typeof(Todo), id);
        }

        public async Task<PagedList<TResult>> GetTodosAsync<TResult>(
            Expression<Func<Todo, bool>> filter,
            Expression<Func<Todo, TResult>> selector,
            int pageIndex,
            int pageSize,
            CancellationToken cancellationToken)
        {
            filter ??= todo => true;

            var count = await dbSet
                .CountAsync(cancellationToken);

            var items = await dbSet
                .Where(filter)
                .OrderBy(todo => todo.IsComplete)
                    .ThenByDescending(todo => todo.Id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Select(selector)
                .ToListAsync(cancellationToken);

            var pagedList = new PagedList<TResult>(items, pageIndex, pageSize, count);

            return pagedList;
        }

        public Todo Insert(Todo todo)
        {
            dbSet.Add(todo);
            return todo;
        }

        public Todo Update(Todo todo)
        {
            dbSet.Update(todo);
            return todo;
        }

        public void Delete(Todo todo)
        {
            dbSet.Remove(todo);
        }
    }
}
