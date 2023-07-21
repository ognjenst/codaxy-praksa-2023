using Newtonsoft.Json;
using SOC.Ticketing.Generated;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Ticketing.Services
{
    public class TicketingService : ITicketingService
    {
        private HttpClient httpClient;

        public TicketingService()
        {
            httpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://192.168.101.17:9000/api/")

            };
            httpClient.DefaultRequestHeaders.Authorization =
             new AuthenticationHeaderValue("Bearer", "u1ikRLJLb4k6xyJV/VPKP8G07doaLpUg");

        }
        public async Task<OutputAlert> CreateAlertAsync(InputCreateAlert inputCreateAlert, CancellationToken cancellationToken = default)
        {
            var serializedAlert = JsonConvert.SerializeObject(inputCreateAlert);

            var content = new StringContent(serializedAlert, new MediaTypeHeaderValue("application/json"));
            var response = await httpClient.PostAsync("v1/alert", content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var outputAlertString = await response.Content.ReadAsStringAsync();
               
                OutputAlert outputAlert = JsonConvert.DeserializeObject<OutputAlert>(outputAlertString);
                return outputAlert;
            }
            else
            {
                //todo: add logging

                return new OutputAlert();
            }
            
        }

        public async Task<OutputCase> CreateCaseAsync(InputCreateCase inputCreateCase, CancellationToken cancellationToken = default)
        {
            var serializedCase = JsonConvert.SerializeObject(inputCreateCase);

            var content = new StringContent(serializedCase, new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            var response = await httpClient.PostAsync("v1/case", content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var outputCaseString = await response.Content.ReadAsStringAsync();
                Console.WriteLine(outputCaseString);
                OutputCase outputCase = JsonConvert.DeserializeObject<OutputCase>(outputCaseString);
                return outputCase;
            }
            else
            {
                //dodati
                Console.WriteLine(response);
                return new OutputCase();
            }
        }

        public async Task<OutputTask> CreateTaskAsync(InputCreateTask inputCreateTask, string caseId, CancellationToken cancellationToken = default)
        {
            var serializedTask = JsonConvert.SerializeObject(inputCreateTask);

            var content = new StringContent(serializedTask, new MediaTypeHeaderValue("application/json"));
            var response = await httpClient.PostAsync($"v1/case/{caseId}/task", content);
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

        public async Task<Response> DeleteAlertAsync(string id, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.DeleteAsync($"v1/alert/{id}");
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

        public async Task<Response> DeleteCaseAsync(string id, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.DeleteAsync($"v1/case/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                
                Response responseOutput = JsonConvert.DeserializeObject<Response>(responseString);
                return responseOutput;
            }
            else
            {
                //dodati
                return new Response();
            }
        }

        public async Task<Response> DeleteTaskAsync(string id, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.DeleteAsync($"v1/task/{id}");
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

        public async Task<OutputAlert> GetAlertAsync(string id, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.GetAsync($"v1/alert/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var outputAlertString = await response.Content.ReadAsStringAsync();
                
                OutputAlert outputAlert = JsonConvert.DeserializeObject<OutputAlert>(outputAlertString);
                return outputAlert;
            }
            else
            {
                //todo: add logging
                return new OutputAlert();
            }
        }

        public async Task<OutputCase> GetCaseAsync(string id, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.GetAsync($"v1/case/{id}");
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var outputCaseString = await response.Content.ReadAsStringAsync();
                
                OutputCase outputCase = JsonConvert.DeserializeObject<OutputCase> (outputCaseString);
                return outputCase;
            }
            else
            {
                return new OutputCase();
            }
        }

        public async Task<OutputTask> GetTaskAsync(string id, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.GetAsync($"v1/task/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
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

        public async Task<Response> UpdateAlertAsync(string id, InputUpdateAlert inputUpdateAlert, CancellationToken cancellationToken = default)
        {
            var serializedAlert = JsonConvert.SerializeObject(inputUpdateAlert);

            var content = new StringContent(serializedAlert, new MediaTypeHeaderValue("application/json"));
            var response = await httpClient.PatchAsync($"v1/alert/{id}", content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var outputAlertString = await response.Content.ReadAsStringAsync();
                
                Response outputAlert = JsonConvert.DeserializeObject<Response>(outputAlertString);
                return outputAlert;
            }
            else
            {
                //todo: add logging
               
                return new Response();
            }
        }

        public async Task<Response> UpdateCaseAsync(string id, InputUpdateCase inputUpdateCase, CancellationToken cancellationToken = default)
        {
            var serializedCase = JsonConvert.SerializeObject(inputUpdateCase);

            var content = new StringContent(serializedCase, new MediaTypeHeaderValue("application/json"));
            var response = await httpClient.PatchAsync($"v1/case/{id}", content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var outputCaseString = await response.Content.ReadAsStringAsync();
                
                Response outputCase = JsonConvert.DeserializeObject<Response>(outputCaseString);
                return outputCase;
            }
            else
            {
                //dodati
                
                return new Response();
            }
        }

        public async Task<Response> UpdateTaskAsync(string id, InputUpdateTask inputUpdateTask, CancellationToken cancellationToken = default)
        {
            var serializedTask = JsonConvert.SerializeObject(inputUpdateTask);

            var content = new StringContent(serializedTask, new MediaTypeHeaderValue("application/json"));
            var response = await httpClient.PatchAsync($"v1/task/{id}", content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var outputTaskString = await response.Content.ReadAsStringAsync();

                Response outputTask = JsonConvert.DeserializeObject<Response>(outputTaskString);
                return outputTask;
            }
            else
            {
                //todo: add logging

                return new Response();
            }
        }

    }
}
