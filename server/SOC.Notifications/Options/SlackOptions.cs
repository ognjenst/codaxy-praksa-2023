using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Notifications.Options
{
    public sealed class SlackOptions
    {
        public static readonly string SectionName = "SlackOptions";

        public required string AccessToken { get; set; }
        public required string Channel { get; set; }
        public required string ContextBlockImageUrl { get; set; }
    }
}
