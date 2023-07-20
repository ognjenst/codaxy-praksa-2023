using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SOC.Conductor.Client.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
        public Task<OutputAlert> CreateAlertAsync(InputCreateAlert inputCreateAlert, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
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
                Console.WriteLine(response);
                return new OutputCase();
            }

        }

        public Task<OutputTask> CreateTaskAsync(InputCreateTask inputCreateTask, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Response> DeleteAlertAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Response> DeleteCaseAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Response> DeleteTaskAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OutputAlert> GetAlertAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OutputCase> GetCaseAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OutputTask> GetTaskAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateAlertAsync(InputUpdateAlert inputUpdateAlert, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateCaseAsync(InputUpdateCase inputUpdateCase, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateTaskAsync(InputUpdateTask inputUpdateTask, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
