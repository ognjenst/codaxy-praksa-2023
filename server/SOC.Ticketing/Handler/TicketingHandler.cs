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
        public string Type { get; set; }
        public string Priority { get; set; }
        public string Title { get; set; }
    }

    // TODO
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
            ICollection<string> tags = request.Type?.Split(',').Select(tag => tag.Trim()).ToList() ?? new List<string>();

            var inputCreateCase = new InputCreateCase()
            {
                Title = request.Title,
                Description = request.Message,
                Tags = tags
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

            //return Task.FromResult(new NoOutput());
        }
    }
}