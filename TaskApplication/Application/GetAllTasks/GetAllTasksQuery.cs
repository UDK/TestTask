using MediatR;
using TaskDomain = TaskApplication.DB.Domain.Task;

namespace TaskApplication.Application.GetAllTasks;

public class GetAllTasksQuery : IRequest<IEnumerable<TaskDomain>>
{
    public int Offset { get; set; }

    public int Limit { get; set; }
}
