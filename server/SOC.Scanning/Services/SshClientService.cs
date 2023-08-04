using Microsoft.Extensions.Options;
using SOC.Scanning.Options;
using Renci.SshNet;

namespace SOC.Scanning.Services;

internal class SshClientService : ISshClientService, IDisposable
{
    private readonly SshOptions _options;
    private readonly SshClient _client;

    public SshClientService(IOptions<SshOptions> options)
    {
        _options = options.Value;
        _client = new SshClient(_options.Address, _options.Port, _options.Username, _options.Password);
    }

    public void Connect()
    {
        _client.Connect();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }


    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _client.Disconnect();
            _client.Dispose();
        }
    }

    public SshCommand ExecuteCommand(string command)
    {
        return _client.RunCommand(command);
    }
}
