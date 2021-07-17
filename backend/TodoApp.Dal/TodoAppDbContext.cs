using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Model;

namespace TodoApp.Dal
{
    public class TodoAppDbContext : DbContext
    {
        public TodoAppDbContext(DbContextOptions<TodoAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
