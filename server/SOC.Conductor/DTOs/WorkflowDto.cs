namespace SOC.Conductor.DTOs;

public sealed class WorkflowDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Version { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public bool Enabled { get; set; }
}
