using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
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

        public Task<List<Todo>> GetTodosAsync(Expression<Func<Todo, bool>> filter = null, CancellationToken cancellationToken = default)
        {
            filter ??= todo => true;

            return dbSet
                .Where(filter)
                .ToListAsync(cancellationToken: cancellationToken);
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
