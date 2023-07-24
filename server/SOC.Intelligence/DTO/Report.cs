using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Intelligence.DTO;

public class Report
{
	[JsonProperty("reportedAt")]
	public DateTime ReportedAt { get; set; }
	[JsonProperty("comment")]
	public string Comment { get; set; }
	[JsonProperty("categories")]
	public List<int> Categories { get; set; }
	[JsonProperty("reporterId")]
	public int ReporterId { get; set; }
	[JsonProperty("reporterCountryCode")]
	public string ReporterCountryCode { get; set; }
	[JsonProperty("reporterCountryName")]
	public string ReporterCountryName { get; set; }
	
}
