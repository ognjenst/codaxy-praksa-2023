using SlackNet.WebApi;
using SOC.Notifications;

string accessToken = "YOUR_ACCESS_TOKEN";
string channel = "YOUR_CHANNEL_NAME";
string message = "Hello, Slack!";

var slackService = new SlackService(accessToken);
await slackService.SendMessage(channel, message);
