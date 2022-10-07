using MediatR;

namespace TaskApplication.Application.DeleteTask;

public class DeleteTaskCommand : IRequest<Unit>
{
    public string Id { get; set; }
}
