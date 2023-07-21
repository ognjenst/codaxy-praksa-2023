using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util;
using MediatR;
using SOC.Ticketing.Services;

namespace SOC.Ticketing.Handler
{

    public class TicketingRequest : IRequest<NoOutput>
    {
        public string Message { get; set; }
    }

    [OriginalName("ticketing")]
    public class TicketingHandler : ITaskRequestHandler<TicketingRequest, NoOutput>
    {
        private readonly ITicketingService _ticketingService;
        public TicketingHandler(ITicketingService ticketingService)
        {
            _ticketingService = ticketingService;
        }

        public Task<NoOutput> Handle(TicketingRequest request, CancellationToken cancellationToken)
        {
          
            return Task.FromResult(new NoOutput());
        }
    }
}