using Microsoft.Extensions.Options;
using SlackNet;
using SlackNet.Blocks;
using SlackNet.WebApi;
using SOC.Notifications.Options;
using static SOC.Notifications.Util.Util;

namespace SOC.Notifications.Services
{
    public class SlackService : ISlackService
    {
        private readonly ISlackApiClient _slackClient;
        private readonly string _slackChannel;
        private readonly string _slackContextBlockImageUrl;

        public SlackService(IOptions<SlackOptions> slackOptions)
        {
            SlackOptions options = slackOptions.Value;
            _slackClient = new SlackServiceBuilder()
                .UseApiToken(options.AccessToken)
                .GetApiClient();
            _slackChannel = options.Channel;
            _slackContextBlockImageUrl = options.ContextBlockImageUrl;
        }

        public async Task SendMessage(string message)
        {
            await _slackClient.Chat.PostMessage(
                new Message
                {
                    Channel = _slackChannel,
                    Blocks = GetSlackBlocks(message, _slackContextBlockImageUrl)
                }
            );
        }
    }
}
