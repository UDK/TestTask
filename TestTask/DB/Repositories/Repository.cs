using Microsoft.Extensions.Options;
using MongoDB.Driver;
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
            var mongoClient = new MongoClient(
                settings.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                settings.DatabaseName);

            _collection = mongoDatabase.GetCollection<T>(
                settings.CollectionName);
        }

        public async Task<List<T>> GetList(Expression<Func<T, bool>> predicate, int offset, int limit) => await _collection.Find(predicate)
            .Skip(offset).Limit(limit).ToListAsync();
        public async System.Threading.Tasks.Task CreateAsync(T item) =>
            await _collection.InsertOneAsync(item);
        public async System.Threading.Tasks.Task UpdateAsync(ID id, T updatedBook) =>
            //await _collection.ReplaceOneAsync(q => q.Id == id, updatedBook);
            throw new Exception();
        public async System.Threading.Tasks.Task RemoveAsync(IEnumerable<ID> ids) =>
            await _collection.DeleteManyAsync(q => ids.Contains(q.Id));
    }
}
