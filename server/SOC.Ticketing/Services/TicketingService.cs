using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SOC.Ticketing.Exceptions;
using SOC.Ticketing.Generated;
using SOC.Ticketing.Options;
using System.Net.Http.Headers;

namespace SOC.Ticketing.Services
{
    public class TicketingService : ITicketingService
    {
        private readonly HttpClient _httpClient;
        private readonly TicketingOptions _ticketingOptions;


        public TicketingService(IOptions<TicketingOptions> ticketingOptions)
        {
            _ticketingOptions = ticketingOptions.Value;
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(_ticketingOptions.BaseUri)
            };
            _httpClient.DefaultRequestHeaders.Authorization =
             new AuthenticationHeaderValue("Bearer", _ticketingOptions.HiveApiKey);
        }

        public async Task<OutputTask> CreateTaskAsync(InputCreateTask inputCreateTask, string caseId, CancellationToken cancellationToken = default)
        {
            var serializedTask = JsonConvert.SerializeObject(inputCreateTask);
            var content = new StringContent(serializedTask, new MediaTypeHeaderValue("application/json"));
            var response = await _httpClient.PostAsync($"v1/case/{caseId}/task", content);
            return await HandleHttpResponse<OutputTask>(response);
        }

        public async Task<TOutput> CreateAsync<TInput, TOutput>(TInput input, string endpointRoute, CancellationToken cancellationToken = default)
        {
            var serializedData = JsonConvert.SerializeObject(input);
            var content = new StringContent(serializedData, new MediaTypeHeaderValue("application/json"));
            var response = await _httpClient.PostAsync($"v1/{endpointRoute}", content);
            return await HandleHttpResponse<TOutput>(response);
        }

        public async Task<TOutput> GetAsync<TOutput>(string id, string endpointRoute, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"v1/{endpointRoute}/{id}");
            return await HandleHttpResponse<TOutput>(response);
        }

        public async Task<Response> DeleteAsync(string id, string endpointRoute, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"v1/{endpointRoute}/{id}");
            return await HandleHttpResponse<Response>(response);
        }

        public async Task<Response> UpdateAsync<TInput>(string id, TInput input, string endpointRoute, CancellationToken cancellationToken = default)
        {
            var serializedCase = JsonConvert.SerializeObject(input);
            var content = new StringContent(serializedCase, new MediaTypeHeaderValue("application/json"));
            var response = await _httpClient.PatchAsync($"v1/{endpointRoute}/{id}", content);
            return await HandleHttpResponse<Response>(response);
        }

        private async Task<TOutput> HandleHttpResponse<TOutput>(HttpResponseMessage response)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TOutput>(responseData);
            }
            else
            {
                var exceptionData = JsonConvert.DeserializeObject<Response>(responseData);
                throw new HiveException(exceptionData.Message);
            }
        }
    }
}
