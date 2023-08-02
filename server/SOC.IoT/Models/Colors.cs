using SOC.IoT.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.IoT.Models
{
    public class Colors
    {
        public static readonly DeviceColorXy RED = new() { X = 0.6942, Y = 0.2963 };
        public static readonly DeviceColorXy YELLOW = new() { X = 0.4339, Y = 0.5008 };
        public static readonly DeviceColorXy BLUE = new() { X = 0.1355, Y = 0.0399 };
    }
}
