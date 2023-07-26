using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Intelligence.Options;


public class IntelligenceOptions
{
	public const string SectionName = "IntelligenceOptions";
	public required string ApiKey { get; set; }
	public required string AbuseIPDBUrl { get; set; }
}
