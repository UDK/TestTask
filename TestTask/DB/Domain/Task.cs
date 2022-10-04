using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestTask.DB.Domain
{
    public class Task : IEntity<int>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
        public string Text { get; set; }
        public IEnumerable<Task> SubTasks {get;set;}
    }
}
