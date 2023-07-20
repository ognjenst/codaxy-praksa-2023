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
        public Task<OutputAlert> CreateAlertAsync(InputCreateAlert inputCreateAlert, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<OutputCase> CreateCaseAsync(InputCreateCase inputCreateCase, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
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
