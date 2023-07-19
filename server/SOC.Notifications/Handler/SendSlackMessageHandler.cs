using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util;
using MediatR;
using SOC.Notifications.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Notifications.Handler
{

    internal class SendSlackMessageRequest : IRequest<NoOutput>
    {
        public string Message { get; set; }
    }

    [OriginalName("SEND_slack_message")]
    internal class SendSlackMessageHandler : ITaskRequestHandler<SendSlackMessageRequest, NoOutput>
    {
        private readonly ISlackService _slackService;
        public SendSlackMessageHandler(ISlackService slackService)
        {
            _slackService = slackService;
        }

        public async Task<NoOutput> Handle(SendSlackMessageRequest request, CancellationToken cancellationToken)
        {
            await _slackService.SendMessage(request.Message);
            return new NoOutput();
        }
    }
}
