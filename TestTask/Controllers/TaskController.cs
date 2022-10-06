using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskInfrastructure.DTO;
using TaskDomain = TaskApplication.DB.Domain.Task;
using TaskApplication.Application.CreateTask;
using TaskApplication.Application.GetAllTasks;
using TaskApplication.Application.UpdateTask;
using TaskApplication.Application.DeleteTask;

namespace TestTask.Controllers;

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
    public async Task<IActionResult> Create(CreateTaskDTO newItem)
    {
        await _mediator.Send(new CreateTaskCommand
        {
            Task = new TaskDomain
            {
                Text = newItem.Text
            }
        });
        return Ok(newItem);
    }

    [HttpPost("all")]
    public async Task<IActionResult> GetList(GetListDTO param)
    {
        var items = await _mediator.Send(new GetAllTasksQuery()
        {
            Offset = param.Offset,
            Limit = param.Limit
        });
        return Ok(items);
    }

    [HttpPut()]
    public async Task<IActionResult> Update(TaskDomain updatedTask)
    {
        await _mediator.Send(new UpdateTaskCommand()
        {
            Task = updatedTask
        });
        return Ok(updatedTask);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        await _mediator.Send(new DeleteTaskCommand()
        {
            Id = id
        });
        return NoContent();
    }
}
