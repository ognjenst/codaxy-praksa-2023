using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SOC.Intelligence.DTO;
using SOC.Intelligence.Exceptions;
using SOC.Intelligence.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Intelligence.Services;

public interface IIntelligenceService 
{
	public Task<IntelligenceResponseDto> CheckEndpoint(string ipAddress, string maxAgeInDays);
}

public class IntelligenceService : IIntelligenceService
{
	private readonly IntelligenceOptions _options;

    public IntelligenceService(IOptions<IntelligenceOptions> options)
    {
		_options = options.Value;
    }
    public async Task<IntelligenceResponseDto> CheckEndpoint(string ipAddress, string maxAgeInDays)
	{
		var client = new RestClient(_options.AbuseIPDBUrl);
		var request = new RestRequest();
		request.AddHeader("Key", _options.ApiKey);
		request.AddHeader("Accept", "application/json");
		request.AddParameter("ipAddress", ipAddress);
		request.AddParameter("maxAgeInDays", maxAgeInDays);
		request.AddParameter("verbose", "");
		

		RestResponse response = await client.ExecuteAsync(request);

		
		if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
		{
			JObject responseJson = JObject.Parse(response.Content);
			var result = JsonConvert.DeserializeObject<IntelligenceResponseDto>(responseJson["data"].ToString());
			Console.WriteLine(result.ToString());
			return result;
		}
		else 
		{
			throw new AppException($"Status code for check endpoint {response.StatusCode}");
		}
	}

	
}
