using SlackNet;
using SlackNet.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Notifications
{
    public class SlackService
    {
        private readonly ISlackApiClient _slackClient;

        public SlackService(string accessToken)
        {
            _slackClient = new SlackServiceBuilder()
             .UseApiToken(accessToken)
             .GetApiClient();
        }

        public async Task SendMessage(string channel, string message)
        {
            await _slackClient.Chat.PostMessage(new Message { Text = "Hello, Slack!", Channel = "#general" });
        }
    }
}
