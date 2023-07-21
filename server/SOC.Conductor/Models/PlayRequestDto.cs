using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SOC.Conductor.Models
{
    public class PlayRequestDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("version")]
        public int Version { get; set; }
        [JsonProperty("input")]
        public Dictionary<string, object> Input { get; set; }
    }
}
