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
        private readonly SlackOptions _slackOptions;
        private readonly ISlackApiClient _slackClient;
        private readonly string _slackChannel;
        private readonly string _slackContextBlockImageUrl;

        public SlackService(IOptions<SlackOptions> slackOptions)
        {
            _slackOptions = slackOptions.Value;
            _slackClient = new SlackServiceBuilder()
                .UseApiToken(_slackOptions.AccessToken)
                .GetApiClient();
            _slackChannel = _slackOptions.Channel;
            _slackContextBlockImageUrl = _slackOptions.ContextBlockImageUrl;
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
