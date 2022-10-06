using MediatR;
using TaskDomain = TestTask.DB.Domain.Task;

namespace TestTask.Application.UpdateTask
{
    public class UpdateTaskCommand : IRequest<Unit>
    {
        public TaskDomain Task { get; set; }
    }
}
