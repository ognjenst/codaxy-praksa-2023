using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Intelligence.DTO;

public class IntelligenceResponseDto
{
	[JsonProperty("ipAddress")]
	public string IPAddress { get; set; }
	[JsonProperty("isPublic")]
	public bool IsPublic { get; set; }
	[JsonProperty("ipVersion")]
	public int IpVersion { get; set; }
	[JsonProperty("isWhiteListed")]
	public bool IsWhitelisted { get; set; }
	[JsonProperty("abuseConfidenceScore")]
	public int AbuseConfidenceScore { get; set; }
	[JsonProperty("countryCode")]
	public string CountryCode { get; set; }
	[JsonProperty("countryName")]
	public string CountryName { get; set; }
	[JsonProperty("usageType")]
	public string UsageType { get; set; }
	[JsonProperty("isp")]
	public string ISP { get; set; }
	[JsonProperty("domain")]
	public string Domain { get; set; }
	[JsonProperty("hostnames")]
	public List<string> Hostnames { get; set; }
	[JsonProperty("isTor")]
	public bool IsTor { get; set; }
	[JsonProperty("totalReports")]
	public int TotalReports { get; set; }
	[JsonProperty("numDistinctUsers")]
	public int NumDistinctUsers { get; set; }
	[JsonProperty("lastReportedAt")]
	public DateTime LastReportedAt { get; set; }
	[JsonProperty("reports")]
	public List<Report> Reports { get; set; }

	public override string ToString()
	{
		return JsonConvert.SerializeObject(this, Formatting.Indented);
	}
}
