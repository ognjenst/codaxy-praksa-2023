using Newtonsoft.Json.Linq;

namespace SOC.IoT.ApiGateway.Models
{
    public class DeviceHistoryDTO
    {
        public DateTime Time { get; set; }
        public JObject? Configuration { get; set; }
    }
}
