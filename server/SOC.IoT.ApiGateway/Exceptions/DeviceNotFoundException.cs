namespace SOC.IoT.ApiGateway.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ItemNotFoundException() : base() { }
        public ItemNotFoundException(string message) : base(message) { }
        public ItemNotFoundException(string message,  Exception innerException) : base(message, innerException) { }
    }
}
