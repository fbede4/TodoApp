using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Domain.UoW;

namespace TodoApp.Bll.PipelineBehaviors
{
    public class UnitOfWorkPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IUnitOfWork unitOfWork;

        public UnitOfWorkPipelineBehavior(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();

            if (typeof(TRequest).Name.EndsWith("Command"))
            {
                await unitOfWork.SaveChangesAsync();
            }

            return response;
        }
    }
}
