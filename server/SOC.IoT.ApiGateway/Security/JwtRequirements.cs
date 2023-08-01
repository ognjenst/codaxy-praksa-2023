using Microsoft.AspNetCore.Authorization;

namespace SOC.IoT.ApiGateway.Security
{
	public class JwtRequirements : IAuthorizationRequirement
	{
		public string RequiredRole { get; }
		public string RequiredPermission { get; }
        public JwtRequirements(string Role, string Permission)
        {
            RequiredRole = Role;    
            RequiredPermission = Permission;
        }
    }
}
