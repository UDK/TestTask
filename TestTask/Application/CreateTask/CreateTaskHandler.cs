using MediatR;
using TestTask.DB.Repositories;
using TaskDomain = TestTask.DB.Domain.Task;

namespace TestTask.Application.CreateTask
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Unit>
    {
        private readonly IRepository<TaskDomain, int> _repository;

        public CreateTaskHandler (IRepository<TaskDomain, int> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(request.Task);
            return Unit.Value;
        }
    }
}
