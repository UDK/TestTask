using System.Text.Json.Serialization;

namespace TestTask.Infrasctructure.DTO
{
    public class TaskDTO
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }
        public string Text { get; set; }
        [JsonPropertyName("children")]
        public List<TaskDTO> SubTasks { get; set; }
    }
}
