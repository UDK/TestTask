using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace TestTask.Infrasctructure.DTO
{
    public class UpdateTaskDTO
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        public string Text { get; set; }
        [JsonPropertyName("children")]
        public List<UpdateTaskDTO> SubTasks { get; set; }
    }
}
