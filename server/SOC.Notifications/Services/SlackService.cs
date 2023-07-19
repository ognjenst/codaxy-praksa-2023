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

        public SlackService(SlackOptions slackOptions)
        {
            _slackClient = new SlackServiceBuilder().UseApiToken(slackOptions.AccessToken).GetApiClient();
            _slackChannel = slackOptions.Channel;
            _slackContextBlockImageUrl = slackOptions.ContextBlockImageUrl;
        }

        public async Task SendMessage(string message)
        {
            await _slackClient.Chat.PostMessage(new Message { Channel = _slackChannel, Blocks = GetSlackBlocks(message, _slackContextBlockImageUrl) });
        }
    }
}
