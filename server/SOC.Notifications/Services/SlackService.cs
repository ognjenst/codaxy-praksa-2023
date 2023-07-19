using SlackNet;
using SlackNet.Blocks;
using SlackNet.WebApi;
using static SOC.Notifications.Util.Util;

namespace SOC.Notifications.Services
{
    public class SlackService : ISlackService
    {
        private readonly ISlackApiClient _slackClient;
        private readonly string _slackChannel;
        private readonly string _slackContextBlockImageUrl;

        public SlackService(string accessToken, string slackChannel, string slackContextBlockImageUrl)
        {
            _slackClient = new SlackServiceBuilder().UseApiToken(accessToken).GetApiClient();
            _slackChannel = slackChannel;
            _slackContextBlockImageUrl = slackContextBlockImageUrl;
        }

        public async Task SendMessage(string message)
        {
            await _slackClient.Chat.PostMessage(new Message { Channel = _slackChannel , Blocks = GetSlackBlocks(message, _slackContextBlockImageUrl) });
        }
    }
}
