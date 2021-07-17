using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoApp.Bll.Features;
using TodoApp.Domain.Utilities.Paging;

namespace TodoApp.Api.Controllers
{
    [ApiController]
    [Route("todos")]
    public class TodosController : ControllerBase
    {
        private readonly IMediator mediator;

        public TodosController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public Task<PagedList<TodoItem>> GetTodos([FromQuery] TodoQuery todoQuery)
        {
            return mediator.Send(todoQuery);
        }

        [HttpPost]
        public Task<int> CreateTodo(TodoCreateCommand todoCreateCommand)
        {
            return mediator.Send(todoCreateCommand);
        }
    }
}
