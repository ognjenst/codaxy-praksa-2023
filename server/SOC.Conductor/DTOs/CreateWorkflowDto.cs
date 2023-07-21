namespace SOC.Conductor.DTOs
{
    public class CreateWorkflowDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Version { get; set; }
        public List<CreateTaskDto> Tasks { get; set; }
    }
}
