using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                var outputTaskString = await response.Content.ReadAsStringAsync();

                OutputTask outputTask = JsonConvert.DeserializeObject<OutputTask>(outputTaskString);

                return outputTask;
            }
            else
            {
                //todo: add logging
                return new OutputTask();
            }
        }

        public async Task<TOutput> CreateAsync<TInput, TOutput>(TInput input, string type, CancellationToken cancellationToken = default)
        {
            var serializedData = JsonConvert.SerializeObject(input);
            var content = new StringContent(serializedData, new MediaTypeHeaderValue("application/json"));

            var response = await _httpClient.PostAsync($"v1/{type}", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TOutput>(responseData);
            }
            else
            {
                return default;
            }
        }

        public async Task<TOutput> GetAsync<TOutput>(string id, string type, CancellationToken cancellationToken = default)
        {

            var response = await _httpClient.GetAsync($"v1/{type}/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var outputString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(outputString);
                TOutput output = JsonConvert.DeserializeObject<TOutput>(outputString);
                return output;
            }
            else
            {
                return default;
            }
        }

        public async Task<Response> DeleteAsync(string id, string type, CancellationToken cancellationToken = default)
        {

            var response = await _httpClient.DeleteAsync($"v1/{type}/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                Response responseOutput = JsonConvert.DeserializeObject<Response>(responseString);
                return responseOutput;
            }
            else
            {
                //todo: add logging
                return new Response();
            }
        }

        public async Task<Response> UpdateAsync<TInput>(string id, TInput input, string type, CancellationToken cancellationToken = default)
        {
            var serializedCase = JsonConvert.SerializeObject(input);

            var content = new StringContent(serializedCase, new MediaTypeHeaderValue("application/json"));

            var response = await _httpClient.PatchAsync($"v1/{type}/{id}", content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var outputString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(outputString);
                Response outputCase = JsonConvert.DeserializeObject<Response>(outputString);
                return outputCase;
            }
            else
            {
                return new Response();
            }
        }
    }
}
