using MediatR;
using Microsoft.AspNetCore.Mvc;
using TestTask.Application;
using TestTask.Application.CreateTask;
using TaskDomain = TestTask.DB.Domain.Task;


namespace TestTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> Create(TaskDomain newItem)
        {
            await _mediator.Send(new CreateTaskCommand
            {
                Task = newItem
            });
            return Ok(newItem);
        }
    }
}
