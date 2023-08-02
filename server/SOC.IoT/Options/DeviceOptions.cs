using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.IoT.Options; 
public sealed class DeviceOptions {
    public static readonly string SectionName = "DeviceOptions";
    public string BaseUrl { get; set; }
    public string Id { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Brightness { get; set; }
	public int MaxNumberOfRepetitions { get; set; }
    public int DelayTime { get; set; }
    public int StartHour { get; set; }
    public int EndHour { get; set; }
}
