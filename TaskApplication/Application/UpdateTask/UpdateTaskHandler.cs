using MediatR;
using MongoDB.Bson;
using TaskApplication.DB.Repositories;
using TaskDomain = TaskApplication.DB.Domain.Task;


namespace TaskApplication.Application.UpdateTask;

internal class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, Unit>
{
    private readonly IRepository<TaskDomain, string> _repository;

    public UpdateTaskHandler(IRepository<TaskDomain, string> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(AddIdForSubTasks(request.Task));
        return Unit.Value;
    }

    private TaskDomain AddIdForSubTasks(TaskDomain rootTask)
    {
        rootTask.SubTasks = rootTask.SubTasks.Select(q =>
        {
            if (q.SubTasks.Count > 0)
                q.SubTasks = q.SubTasks.Select(w => AddIdForSubTasks(w)).ToList();
            if (!ObjectId.TryParse(q.Id, out _))
                q.Id = ObjectId.GenerateNewId().ToString();
            return q;
        }).ToList();
        if (!ObjectId.TryParse(rootTask.Id, out _))
            rootTask.Id = ObjectId.GenerateNewId().ToString();
        return rootTask;
    }
}
