using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using SOC.IoT.ApiGateway.Entities;
using SOC.IoT.ApiGateway.Entities.Contexts;
using SOC.IoT.ApiGateway.Exceptions;
using SOC.IoT.ApiGateway.Options;

namespace SOC.IoT.ApiGateway.Security
{
	public class JwtAuthorizationHandler : AuthorizationHandler<JwtRequirements>
	{
		
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, JwtRequirements requirement)
		{
			// Get permissions from JWT
			var userPermissions = context.User.Claims
				.Where(x => x.Type == "Permission")
				.Select(x => x.Value)
				.ToList();

			if (userPermissions != null)
			{
				// Check if user permissions have required permission
				if (userPermissions.Any(x => x == requirement.Permission))
				{
					context.Succeed(requirement);
				}
				else
				{
					context.Fail();
				}
			}
			else 
			{
				context.Fail();
			}

			return Task.CompletedTask;
		}
	}
}
