using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using SOC.IoT.ApiGateway.Entities.Contexts;
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
			throw new NotImplementedException();
		}
	}
}
