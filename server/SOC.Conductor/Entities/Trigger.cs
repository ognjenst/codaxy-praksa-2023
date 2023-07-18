namespace SOC.Conductor.Entities
{
    public class Trigger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Workflow> Workflows { get; set; }
    }
}
