using Newtonsoft.Json.Linq;
using SOC.IoT.ApiGateway.Models;
using SOC.IoT.Domain.Entity;
using SOC.IoT.Domain.Enum;
using Swashbuckle.AspNetCore.Filters;

namespace SOC.IoT.ApiGateway.Controllers.Examples
{
    public class DeviceHistoryExample : IExamplesProvider<IEnumerable<DeviceHistoryDTO>>
    {
        public IEnumerable<DeviceHistoryDTO> GetExamples()
        {
            Device device = new Device
            {
                Id = "0xfedcba9876543210",
                Capabilities = new[]
                {
                    DeviceCapability.State,
                    DeviceCapability.Light,
                    DeviceCapability.ColorXy
                },
                State = new DeviceState { State = true }
            };
            return new List<DeviceHistoryDTO> 
            { 
                new DeviceHistoryDTO() 
                {
                    Time = new DateTime(2023, 7, 20, 9, 0, 0),
                    Configuration = JObject.FromObject(device).ToString(),
                } 
            };
        }
    }
}
