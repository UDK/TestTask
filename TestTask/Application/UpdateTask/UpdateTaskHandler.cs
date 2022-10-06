using MediatR;
using MongoDB.Bson;
using TestTask.Application.UpdateTask;
using TestTask.DB.Repositories;
using TaskDomain = TestTask.DB.Domain.Task;


namespace TestTask.Application.UpdateTask
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, Unit>
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
                return q;
            }).ToList();
            if (!ObjectId.TryParse(rootTask.Id, out _))
                rootTask.Id = ObjectId.GenerateNewId().ToString();
            return rootTask;
        }
    }
}
