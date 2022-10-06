using MediatR;
using TaskDomain = TestTask.DB.Domain.Task;

namespace TestTask.Application.GetAllTasks
{
    public class GetAllTasksQuery : IRequest<IEnumerable<TaskDomain>>
    {
    }
}
