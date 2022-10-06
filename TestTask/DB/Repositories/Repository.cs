using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections;
using System.Linq.Expressions;
using TestTask.DB.Domain;
using TestTask.Infrasctructure.Configuration;

namespace TestTask.DB.Repositories
{
    public class Repository<T, ID> : IRepository<T, ID> where T : class, IEntity<ID>
    {
        private readonly IMongoCollection<T> _collection;

        public Repository(
        ConfigureSettings settings)
        {
            _collection = new MongoClient(settings.ConnectionString)
                .GetDatabase(settings.DatabaseName)
                .GetCollection<T>(settings.CollectionName);
        }

        public async Task<List<T>> GetList(Expression<Func<T, bool>> predicate, int offset, int limit) => await _collection.Find(predicate)
            .Skip(offset).Limit(limit).ToListAsync();
        public async System.Threading.Tasks.Task CreateAsync(T item) =>
            await _collection.InsertOneAsync(item);
        public async System.Threading.Tasks.Task UpdateAsync(T updatedTask)
        {
            try
            {
                var ss = await _collection.ReplaceOneAsync(q => q.Id.Equals(ObjectId.Parse(updatedTask.Id as string)), updatedTask);
            }
            catch (Exception e)
            {

            }
        }
        public async System.Threading.Tasks.Task RemoveAsync(IEnumerable<ID> ids) =>
            await _collection.DeleteManyAsync(q => ids.Contains(q.Id));
    }
}
