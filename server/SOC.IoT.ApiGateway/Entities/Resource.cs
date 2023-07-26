namespace SOC.IoT.ApiGateway.Entities
{
    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Scope> Scopes { get; set; }
    }
}
