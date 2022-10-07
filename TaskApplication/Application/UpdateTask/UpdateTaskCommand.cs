using MediatR;
using TaskDomain = TaskApplication.DB.Domain.Task;

namespace TaskApplication.Application.UpdateTask;

public class UpdateTaskCommand : IRequest<Unit>
{
    public TaskDomain Task { get; set; }
}
