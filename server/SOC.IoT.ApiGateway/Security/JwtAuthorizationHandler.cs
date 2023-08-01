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
		private readonly JwtSecret _options;
		private readonly SOCIoTDbContext _context;

        public JwtAuthorizationHandler(IOptions<JwtSecret> options, SOCIoTDbContext context)
        {
            _context = context;
			_options = options.Value;
        }
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, JwtRequirements requirement)
		{
			Console.WriteLine(context.User.Claims);

			var userId = context.User.Claims.FirstOrDefault()?.Value;

			if (userId != null)
			{
				var currentUser = _context.Users
					.Where(x => x.Id.ToString() == userId)
					.FirstOrDefault();

				var currentUserPermissions = _context.Permissions
					.Where(x => x.RoleId == currentUser.RoleId)
					.Select(x => x.Name)
					.ToList();

				if (currentUserPermissions.Any(x => x == requirement.Permission))
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
				throw new AppException("This user doesn't exist!");
			}

			return Task.CompletedTask;
		}
	}
}
