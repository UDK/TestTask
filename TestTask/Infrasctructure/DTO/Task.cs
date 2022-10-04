namespace TestTask.Infrasctructure.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public IEnumerable<TaskDTO> SubTasks { get; set; }
    }
}
