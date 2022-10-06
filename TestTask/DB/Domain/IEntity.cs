using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TestTask.DB.Domain
{
    public interface IEntity<T>
    {
        [BsonId]
        public T Id { get; set; }
    }
}
