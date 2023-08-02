using Microsoft.AspNetCore.Authorization;

namespace SOC.IoT.ApiGateway.Security
{
	public class JwtRequirements : IAuthorizationRequirement
	{
		public string Permission { get; set; }

        public JwtRequirements(string permission)
        {
            Permission = permission;
        }
    }
}
