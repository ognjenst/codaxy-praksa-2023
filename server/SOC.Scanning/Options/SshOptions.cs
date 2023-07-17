namespace SOC.Scanning.Options;

internal sealed class SshOptions
{
    public static readonly string SectionName = "Ssh";

    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Address { get; set; }
    public required int Port { get; set; }
}
