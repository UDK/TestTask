using MediatR;
using TaskApplication.DB.Repositories;
using TaskDomain = TaskApplication.DB.Domain.Task;

namespace TaskApplication.Application.CreateTask;

internal class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Unit>
{
    private readonly IRepository<TaskDomain, string> _repository;

    public CreateTaskHandler(IRepository<TaskDomain, string> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        request.Task.SubTasks = new List<TaskDomain>();
        await _repository.CreateAsync(request.Task);
        return Unit.Value;
    }
}
