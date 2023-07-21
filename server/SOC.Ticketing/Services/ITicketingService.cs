using SOC.Conductor.Client.Generated;


namespace SOC.Ticketing.Services
{
    public interface ITicketingService
    {

        Task<OutputTask> CreateTaskAsync(InputCreateTask inputCreateTask, string caseId, CancellationToken cancellationToken = default);

        Task<TOutput> GetAsync<TOutput>(string id, string type, CancellationToken cancellationToken = default);
        Task<TOutput> CreateAsync<TInput, TOutput>(TInput input, string type, CancellationToken cancellationToken = default);
        Task<Response> DeleteAsync(string id, string type, CancellationToken cancellationToken = default);
        Task<Response> UpdateAsync<TInput>(string id, TInput input, string type, CancellationToken cancellationToken = default);

    }
}
