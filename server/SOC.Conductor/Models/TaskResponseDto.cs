using Newtonsoft.Json;
using System.Text.Json;

namespace SOC.Conductor.Models
{
    public class TaskResponseDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("inputKeys")]
        public ICollection<string> inputKeys { get; set; }
    }
}
