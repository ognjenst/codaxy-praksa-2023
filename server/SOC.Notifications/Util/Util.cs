using SlackNet.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Notifications.Util
{
    public static class Util
    {
        public static Block[] GetSlackBlocks(string message, string slackContextBlockImageUrl)
        {
            return new Block[]
            {
                new HeaderBlock
                {
                    Text = new PlainText
                    {
                        Text = ":tada: *SOC: NEW MESSAGE!* :tada:",
                        Emoji = true
                    }
                },
                new DividerBlock(),
                new SectionBlock
                {
                    Text = message
                },
                new DividerBlock(),
                new ContextBlock
                {
                    Elements = new IContextElement[]
                    {
                        new Image
                        {
                            ImageUrl = slackContextBlockImageUrl,
                            AltText = "Codaxy",
                        },
                        new PlainText
                        {
                            Text = "Codaxy Praksa 2023"
                        }
                    }
                }
            };
        }
    }
}
