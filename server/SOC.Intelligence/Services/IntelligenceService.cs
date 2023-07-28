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
using System.Net.Http;
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
    private readonly HttpClient _client;

    public IntelligenceService(IOptions<IntelligenceOptions> options, HttpClient client)
    {
        _options = options.Value;
        _client = client;
    }

    // This method checks endpoint and returns all reports for endpoint
    public async Task<IntelligenceResponseDto> CheckEndpoint(string ipAddress, string maxAgeInDays)
    {
        _client.DefaultRequestHeaders.Add("Key", _options.ApiKey);
        _client.DefaultRequestHeaders.Add("Accept", "application/json");
        string apiUrl = $"{_options.AbuseIPDBUrl}?ipAddress={ipAddress}&maxAgeInDays={maxAgeInDays}&verbose";
        HttpResponseMessage response = await _client.GetAsync(apiUrl);

        if(response.IsSuccessStatusCode)
        {
            JObject responseJson = JObject.Parse(await response.Content.ReadAsStringAsync());
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
