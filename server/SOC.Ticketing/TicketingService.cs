using HiveMQtt.Client.Options;
using HiveMQtt.Client;
using HiveMQtt.MQTT5.ReasonCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiveMQtt.MQTT5.Types;
using System.Text.Json;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace SOC.Ticketing
{
    public class TicketingService
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
        public async Task DisplayConnectionStatus()
        {
            Console.WriteLine($"Connecting ...");
            
            Case _case = new Case();
            _case.Title = "title2";
            _case.Description = "desc3";
            var content = new StringContent(JsonConvert.SerializeObject(_case), new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            var res1 = await httpClient.PostAsync("v1/case", content);
            Console.WriteLine(res1);
            Console.WriteLine("***************");

            var res2 = await httpClient.GetAsync("v1/case/3");
            var content2 = await res2.Content.ReadAsStringAsync();
            Console.WriteLine(content2);
        }



    }

}
