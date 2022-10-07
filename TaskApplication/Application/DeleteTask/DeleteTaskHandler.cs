using MediatR;
using TaskApplication.DB.Repositories;
using TaskDomain = TaskApplication.DB.Domain.Task;

namespace TaskApplication.Application.DeleteTask;

internal class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, Unit>
{
    private readonly IRepository<TaskDomain, string> _repository;

    public DeleteTaskHandler(IRepository<TaskDomain, string> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        await _repository.RemoveAsync(new[] { request.Id });
        return Unit.Value;
    }
}
