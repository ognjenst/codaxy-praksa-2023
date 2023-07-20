using SOC.Conductor.Client.Generated;
using SOC.Ticketing.Models;

namespace SOC.Ticketing.Services
{
    public interface ITicketingService
    {
        Task<OutputCase> GetCaseAsync(string id, CancellationToken cancellationToken = default);
        Task<OutputCase> CreateCaseAsync(InputCreateCase inputCreateCase, CancellationToken cancellationToken = default);
        Task<Response> DeleteCaseAsync(string id, CancellationToken cancellationToken = default);
        Task<Response> UpdateCaseAsync(InputUpdateCase inputUpdateCase, CancellationToken cancellationToken = default);

        Task<OutputAlert> GetAlertAsync(string id, CancellationToken cancellationToken = default);
        Task<OutputAlert> CreateAlertAsync(InputCreateAlert inputCreateAlert, CancellationToken cancellationToken = default);
        Task<Response> DeleteAlertAsync(string id, CancellationToken cancellationToken = default);
        Task<Response> UpdateAlertAsync(string id, InputUpdateAlert inputUpdateAlert, CancellationToken cancellationToken = default);

        Task<OutputTask> GetTaskAsync(string id, CancellationToken cancellationToken = default);
        Task<OutputTask> CreateTaskAsync(InputCreateTask inputCreateTask, CancellationToken cancellationToken = default);
        Task<Response> DeleteTaskAsync(string id, CancellationToken cancellationToken = default);
        Task<Response> UpdateTaskAsync(InputUpdateTask inputUpdateTask, CancellationToken cancellationToken = default);

    }
}
