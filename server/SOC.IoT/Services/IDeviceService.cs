using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.IoT.Services; 

public interface IDeviceService {
    public Task UpdateDevice(string id); //here we need more DeviceUpdateDTO payload
}
