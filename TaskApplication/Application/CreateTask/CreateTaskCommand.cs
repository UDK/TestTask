using MediatR;
using TaskDomain = TaskApplication.DB.Domain.Task;

namespace TaskApplication.Application.CreateTask;

public class CreateTaskCommand : IRequest<Unit>
{
    public TaskDomain Task { get; set; }
}
