using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TodoApp.Bll.Features;
using TodoApp.Domain.Utilities.Paging;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodosController : ControllerBase
    {
        private readonly IMediator mediator;

        public TodosController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<PagedList<TodoItem>> GetTodos([FromQuery] TodoQuery todoQuery, CancellationToken cancellationToken)
        {
            return mediator.Send(todoQuery, cancellationToken);
        }

        [HttpPost]
        public Task<int> CreateTodo(CreateTodoCommand createTodoCommand, CancellationToken cancellationToken)
        {
            return mediator.Send(createTodoCommand, cancellationToken);
        }

        [HttpPatch("{id}")]
        public Task PatchTodo(int id, PatchTodoCommand patchTodoCommand, CancellationToken cancellationToken)
        {
            patchTodoCommand.Id = id;
            return mediator.Send(patchTodoCommand, cancellationToken);
        }

        [HttpDelete("{id}")]
        public Task DeleteTodo(int id, CancellationToken cancellationToken)
        {
            return mediator.Send(new DeleteTodoCommand
            {
                Id = id
            }, cancellationToken);
        }
    }
}
