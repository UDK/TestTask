using System.Linq.Expressions;

namespace TaskApplication.DB.Repositories;

public interface IRepository<T, ID> where T : class
{
    public Task<List<T>> GetList(Expression<Func<T, bool>> predicate, int offset, int limit);
    public Task CreateAsync(T item);
    public Task UpdateAsync(T updatedBook);
    public Task RemoveAsync(IEnumerable<ID> ids);
}
