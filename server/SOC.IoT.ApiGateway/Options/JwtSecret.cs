namespace SOC.IoT.ApiGateway.Options
{
    public sealed class JwtSecret
    {
        public static readonly string SectionName = "Jwt";

        public string Key { get; set; }
    }
}
