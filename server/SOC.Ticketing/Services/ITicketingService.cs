using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Ticketing.Services
{
    public interface ITicketingService
    {
        Task CreateTicket(String message);
        Task OpenCases();
    }
}
