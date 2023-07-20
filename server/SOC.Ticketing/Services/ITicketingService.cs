using SOC.Conductor.Client.Generated;
using SOC.Ticketing.Models;

namespace SOC.Ticketing.Services
{
    public interface ITicketingService
    {
        Task<OutputCase> GetCaseAsync(int id, CancellationToken cancellationToken = default);
        Task<OutputCase> CreateCaseAsync(InputCreateCase inputCreateCase, CancellationToken cancellationToken = default);
        Task<Response> DeleteCaseAsync(int id, CancellationToken cancellationToken = default);
        Task<Response> UpdateCaseAsync(InputUpdateCase inputUpdateCase, CancellationToken cancellationToken = default);

        Task<OutputAlert> GetAlertAsync(int id, CancellationToken cancellationToken = default);
        Task<OutputAlert> CreateAlertAsync(InputCreateAlert inputCreateAlert, CancellationToken cancellationToken = default);
        Task<Response> DeleteAlertAsync(int id, CancellationToken cancellationToken = default);
        Task<Response> UpdateAlertAsync(InputUpdateAlert inputUpdateAlert, CancellationToken cancellationToken = default);

        Task<OutputTask> GetTaskAsync(int id, CancellationToken cancellationToken = default);
        Task<OutputTask> CreateTaskAsync(InputCreateTask inputCreateTask, CancellationToken cancellationToken = default);
        Task<Response> DeleteTaskAsync(int id, CancellationToken cancellationToken = default);
        Task<Response> UpdateTaskAsync(InputUpdateTask inputUpdateTask, CancellationToken cancellationToken = default);

    }
}
