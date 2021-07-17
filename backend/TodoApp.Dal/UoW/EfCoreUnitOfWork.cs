using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Domain.UoW;

namespace TodoApp.Dal.UoW
{
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        private readonly DbContext dbContext;

        public EfCoreUnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
