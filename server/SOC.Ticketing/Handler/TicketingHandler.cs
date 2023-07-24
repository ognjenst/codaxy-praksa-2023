using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util;
using MediatR;
using SOC.Ticketing.Generated;
using SOC.Ticketing.Services;

namespace SOC.Ticketing.Handler
{

    public class TicketingRequest : IRequest<NoOutput>
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public InputCreateCaseSeverity? Severity { get; set; }
    }

    [OriginalName("ticketing")]
    public class TicketingHandler : ITaskRequestHandler<TicketingRequest, NoOutput>
    {
        private readonly ITicketingService _ticketingService;
        public TicketingHandler(ITicketingService ticketingService)
        {
            _ticketingService = ticketingService;
        }

        public async Task<NoOutput> Handle(TicketingRequest request, CancellationToken cancellationToken)
        {
            //ICollection<string> tags = request.Priority?.Split(',').Select(tag => tag.Trim()).ToList() ?? new List<string>();

            var inputCreateCase = new InputCreateCase()
            {
                Title = request.Title,
                Description = request.Message,
                Severity = request.Severity
            };

            var inputCeateTask = new InputCreateTask()
            {
                Title = request.Title
            };

            try
            {
                var createCaseResponse = await _ticketingService.CreateAsync<InputCreateCase, OutputCase>(inputCreateCase, "case");
                var caseId = createCaseResponse.Id;
                await _ticketingService.CreateTaskAsync(inputCeateTask, caseId);
                return new NoOutput();
            }catch(Exception ex)
            {
                throw;
            }
        }
    }
}