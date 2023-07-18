using Newtonsoft.Json.Linq;

namespace SOC.IoT.ApiGateway.Entities
{
    public class DeviceHistory
    {
        public int Id { get; set; }
        public string DeviceID { get; set; }
        public DateTime Time { get; set; }
        public JObject? Configuration { get; set; }
    }
}
