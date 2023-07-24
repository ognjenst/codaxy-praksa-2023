using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.IoT.Services;

internal class DeviceService : IDeviceService {
    
    //this should be changed with deviceclient
    private readonly HttpClient _httpClient;

    public DeviceService(HttpClient httpClient) {
        _httpClient = httpClient;
    }

    public async Task UpdateDevice(string id) {
        throw new NotImplementedException("not implemented exception ...");
    }
}
