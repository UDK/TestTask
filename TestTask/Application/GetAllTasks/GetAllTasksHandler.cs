using MediatR;
using MongoDB.Bson;
using TaskDomain = TestTask.DB.Domain.Task;
using TestTask.DB.Repositories;

namespace TestTask.Application.GetAllTasks
{
    public class GetAllTasksHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskDomain>>
    {
        private readonly IRepository<TaskDomain, string> _repository;

        public GetAllTasksHandler(IRepository<TaskDomain, string> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaskDomain>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.GetList((q) => true,0,999);
            return items;
        }
    }
}
