using System.Reflection.Metadata;

namespace SOC.IoT.ApiGateway.Entities
{
    public class Scope
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
    }
}
