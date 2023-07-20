using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Ticketing.Handler
{
    internal class TicketRequest : IRequest<TicketResponse>
    {
        public string Message { get; set; }
    }

    internal class TicketResponse
    {
        public string Message { get; set; }
    }
    public class CreateTicketHandler
    {
    }
}
