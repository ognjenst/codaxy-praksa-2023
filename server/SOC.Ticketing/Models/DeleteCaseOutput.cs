using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Ticketing.Models
{
    public class DeleteCaseOutput
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }

    }
}
