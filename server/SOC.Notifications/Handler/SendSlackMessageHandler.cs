using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util;
using MediatR;
using Microsoft.Extensions.Logging;
using SOC.Notifications.Services;

namespace SOC.Notifications.Handler
{
    internal class SendSlackMessageRequest : IRequest<NoOutput>
    {
        public string Message { get; set; }
    }

    [OriginalName("NOTIFICATIONS_send_slack_message")]
    internal class SendSlackMessageHandler : ITaskRequestHandler<SendSlackMessageRequest, NoOutput>
    {
        private readonly ISlackService _slackService;
        private readonly ILogger<SendSlackMessageHandler> _logger;

        public SendSlackMessageHandler(
            ISlackService slackService,
            ILogger<SendSlackMessageHandler> logger
        )
        {
            _slackService = slackService;
            _logger = logger;
        }

        public async Task<NoOutput> Handle(
            SendSlackMessageRequest request,
            CancellationToken cancellationToken
        )
        {
            _logger.LogInformation("Sending to Slack message: {@Request}", request);
            await _slackService.SendMessage(request.Message);
            _logger.LogInformation("Message successfully sent.");
            return new NoOutput();
        }
    }
}
