using System.Threading;
using System.Threading.Tasks;

namespace TodoApp.Domain.UoW
{
    public interface IUnitOfWork
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
