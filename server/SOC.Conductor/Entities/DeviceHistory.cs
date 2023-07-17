using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace SOC.Conductor.Entities
{
    public class DeviceHistory
    {
        public int Id { get; set; } 

        public string DeviceID { get; set; }

        public DateTime Time { get; set; }

        public JObject? Configuration { get; set; }


    }
}
