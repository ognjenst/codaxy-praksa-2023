using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json.Linq;

namespace SOC.IoT.Base.Infrastructure;

internal class MqttService : IMqttService
{
    private readonly ILogger<MqttService> _logger;
    private readonly IConfiguration _configuration;

    private readonly IMqttClient _writeClient;
    private readonly MqttFactory _mqttFactory = new();
    private readonly MqttApplicationMessageBuilder _messageBuilder = new();
    private string _mqttServerUrl;
    private readonly TaskCompletionSource _startSync = new();
    private readonly Dictionary<string, IMqttClient> _readClients = new();
    private Func<MqttClientOptions> _clientOptions;

    public MqttService(ILogger<MqttService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;

        var mqttUrl = _configuration["MqttUrl"];
        if (mqttUrl is null)
        {
            var error = "MqttUrl is not defined in appsettings.";
            _logger.LogCritical(error);
            throw new InvalidOperationException(error);
        }

        _mqttServerUrl = mqttUrl;
        _clientOptions = () =>
            _mqttFactory
                .CreateClientOptionsBuilder()
                .WithTcpServer(mqttUrl)
                .WithClientId($"{Guid.NewGuid()}")
                .Build();

        _writeClient = _mqttFactory.CreateMqttClient();

        _ = StartAsync();
    }

    private async Task StartAsync()
    {
        if (_startSync.Task.IsCompleted)
        {
            return;
        }
        _logger.LogInformation($"Connecting to MQTT at [{_mqttServerUrl}]");

        var mqttClientOptions = _mqttFactory
            .CreateClientOptionsBuilder()
            .WithTcpServer(_mqttServerUrl)
            .Build();

        _writeClient.DisconnectedAsync += (args) =>
        {
            if (args.Exception is not null)
            {
                _logger.LogError(args.Exception.Message);
            }
            return Task.CompletedTask;
        };

        await _writeClient.ConnectAsync(mqttClientOptions);
        _logger.LogInformation("Connected to MQTT!");

        _startSync.TrySetResult();
    }

    public async Task PublishAsync(string topic, JObject message)
    {
        await _startSync.Task;
        var applicationMessage = _messageBuilder
            .WithTopic(topic)
            .WithPayload(message.ToString(Newtonsoft.Json.Formatting.None))
            .Build();

        await _writeClient.PublishAsync(applicationMessage);
    }

    public async Task SubscribeAsync(
        string topic,
        Func<MqttApplicationMessageReceivedEventArgs, Task> action
    )
    {
        await _startSync.Task;

        var client = _mqttFactory.CreateMqttClient();

        _readClients[topic] = client;

        await client.ConnectAsync(_clientOptions());

        var filterBuilder = new MqttTopicFilterBuilder().WithTopic(topic);
        var filter = filterBuilder.Build();
        var optionsBuilder = new MqttClientSubscribeOptionsBuilder();
        var options = optionsBuilder.WithTopicFilter(filter).Build();

        await client.SubscribeAsync(options);
        client.ApplicationMessageReceivedAsync += action;
    }

    public Task UnsubscribeAsync(string topic)
    {
        throw new NotImplementedException();
    }
}
