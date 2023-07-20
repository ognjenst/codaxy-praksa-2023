namespace SOC.Conductor.DTOs
{
    public class CreateWorkflowDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Version { get; set; }
        public List<CreateTaskDto> Tasks { get; set; }
        public bool Restartable { get; set; } = true;
        public bool WorkflowStatusListenerEnabled { get; set; } = true;
        public string OwnerEmail { get; set; } = "conductor@example.com";
        public string TimeoutPolicy { get; set; } = "ALERT_ONLY";
        public int TimeoutSeconds { get; set; } = 0;
    }
}
