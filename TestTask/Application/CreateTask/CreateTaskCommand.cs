using MediatR;
using TaskDomain = TestTask.DB.Domain.Task;

namespace TestTask.Application.CreateTask
{
    public class CreateTaskCommand : IRequest<Unit>
    {
        public TaskDomain Task { get; set; }
    }
}
