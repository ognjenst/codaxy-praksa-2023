using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.IoT.Options; 
public sealed class DeviceOptions {
    public static readonly string SectionName = "DeviceOptions";

    public string BaseUrl { get; set; }
}
