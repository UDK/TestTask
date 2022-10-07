using MediatR;
using TaskApplication.DB.Repositories;
using TaskDomain = TaskApplication.DB.Domain.Task;

namespace TaskApplication.Application.GetAllTasks;

internal class GetAllTasksHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskDomain>>
{
    private readonly IRepository<TaskDomain, string> _repository;

    public GetAllTasksHandler(IRepository<TaskDomain, string> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskDomain>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        var items = await _repository.GetList((q) => true, request.Offset, request.Limit);
        return items;
    }
}
