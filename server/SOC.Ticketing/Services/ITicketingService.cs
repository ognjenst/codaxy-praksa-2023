using SOC.Conductor.Client.Generated;

namespace SOC.Ticketing.Services
{
    public interface ITicketingService
    {
        Task<OutputCase> GetCaseAsync(int id, string message);
        Task OpenCases();
    }
}
