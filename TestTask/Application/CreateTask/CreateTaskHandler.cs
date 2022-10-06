using MediatR;
using MongoDB.Bson;
using TestTask.DB.Repositories;
using TaskDomain = TestTask.DB.Domain.Task;

namespace TestTask.Application.CreateTask
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Unit>
    {
        private readonly IRepository<TaskDomain, string> _repository;

        public CreateTaskHandler (IRepository<TaskDomain, string> repository)
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
}
