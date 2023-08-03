using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util;
using MediatR;
using Newtonsoft.Json;
using SOC.Ticketing.Generated;
using SOC.Ticketing.Services;
using System.Text.Json;
using static ConductorSharp.Engine.Service.DeploymentValidator;

namespace SOC.Ticketing.Handler
{

    public class TicketingRequest : IRequest<NoOutput>
    {
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("severity")]
        public string Severity { get; set; }
    }

    [OriginalName("TICKETING_create_ticket_on_hive")]
    public class TicketingHandler : ITaskRequestHandler<TicketingRequest, NoOutput>
    {
        private readonly ITicketingService _ticketingService;
        public TicketingHandler(ITicketingService ticketingService)
        {
            _ticketingService = ticketingService;
        }

        public async Task<NoOutput> Handle(TicketingRequest request, CancellationToken cancellationToken)
        {
            InputCreateCaseSeverity inputSeverity;
            switch (request.Severity){
                case "LOW":
                    inputSeverity = InputCreateCaseSeverity._1;
                    break;
                case "MEDIUM":
                    inputSeverity = InputCreateCaseSeverity._2;
                    break;
                case "HIGH":
                    inputSeverity = InputCreateCaseSeverity._3;
                    break;
                case "CRITICAL":
                    inputSeverity = InputCreateCaseSeverity._4;
                    break;
                default:
                    throw new ArgumentException("Invalid severity level provided.");
            }

            string title = request.Title;

            var inputCreateCase = new InputCreateCase()
            {
                Title = title,
                Description = request.Message,
                Severity = inputSeverity
            };

            var inputCeateTask = new InputCreateTask()
            {
                Title = title
            };

            try
            {
                var createCaseResponse = await _ticketingService.CreateAsync<InputCreateCase, OutputCase>(inputCreateCase, "case");
                var caseId = createCaseResponse.Id;
                await _ticketingService.CreateTaskAsync(inputCeateTask, caseId);
                return new NoOutput();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}