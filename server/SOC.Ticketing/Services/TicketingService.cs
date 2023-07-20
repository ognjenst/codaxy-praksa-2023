using SOC.Conductor.Client.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Ticketing.Services
{
    public class TicketingService : ITicketingService
    {
        public Task<OutputAlert> CreateAlertAsync(InputCreateAlert inputCreateAlert)
        {
            throw new NotImplementedException();
        }

        public Task<OutputCase> CreateCaseAsync(InputCreateCase inputCreateCase)
        {
            throw new NotImplementedException();
        }

        public Task<OutputTask> CreateTaskAsync(InputCreateTask inputCreateTask)
        {
            throw new NotImplementedException();
        }

        public Task<Response> DeleteAlertAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> DeleteCaseAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> DeleteTaskAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OutputAlert> GetAlertAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OutputCase> GetCaseAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OutputTask> GetTaskAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateAlertAsync(InputUpdateAlert inputUpdateAlert)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateCaseAsync(InputUpdateCase inputUpdateCase)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateTaskAsync(InputUpdateTask inputUpdateTask)
        {
            throw new NotImplementedException();
        }
    }
}
