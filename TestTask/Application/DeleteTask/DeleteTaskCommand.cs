using MediatR;

namespace TestTask.Application.DeleteTask
{
    public class DeleteTaskCommand : IRequest<Unit>
    {
        public string Id { get; set; }
    }
}
