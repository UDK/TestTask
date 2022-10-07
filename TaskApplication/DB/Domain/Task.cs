using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace TaskApplication.DB.Domain;

public class Task : IEntity<string>
{
    [BsonId]
    [BsonElement("_id")]
    [JsonPropertyName("_id")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Text { get; set; }
    [JsonPropertyName("children")]
    public List<Task> SubTasks { get; set; }
}
