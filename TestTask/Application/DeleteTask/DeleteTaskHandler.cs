using MediatR;
using TestTask.DB.Repositories;
using TaskDomain = TestTask.DB.Domain.Task;

namespace TestTask.Application.DeleteTask
{
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, Unit>
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
}
