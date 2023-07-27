using Microsoft.AspNetCore.Authorization;

namespace SOC.IoT.ApiGateway.Helpers
{
    public sealed class PolicyRequirement : IAuthorizationRequirement
    {
        public string Policy { get; set; }

        public PolicyRequirement(string policy) => Policy = policy;
    }
}
