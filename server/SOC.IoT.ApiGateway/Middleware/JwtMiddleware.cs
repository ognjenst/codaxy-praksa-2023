using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SOC.IoT.ApiGateway.Entities.Contexts;
using SOC.IoT.ApiGateway.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SOC.IoT.ApiGateway.Middleware
{
	public class JwtMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly JwtSecret _appSettings;

		public JwtMiddleware(RequestDelegate next, IOptions<JwtSecret> appSettings)
		{
			_next = next;
			_appSettings = appSettings.Value;
		}

		public async Task Invoke(HttpContext context, SOCIoTDbContext dataContext)
		{
			var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

			if (token != null)
				await attachAccountToContext(context, dataContext, token);

			await _next(context);
		}

		private async Task attachAccountToContext(HttpContext context, SOCIoTDbContext dataContext, string token)
		{
			try
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var key = Encoding.ASCII.GetBytes(_appSettings.Key);
				tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					// set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
					ClockSkew = TimeSpan.Zero
				}, out SecurityToken validatedToken);

				var jwtToken = (JwtSecurityToken)validatedToken;
				var accountId = jwtToken.Claims.First(x => x.Type == "id").Value;

				// attach account to context on successful jwt validation
				var account = dataContext.Users.FirstOrDefault(x => x.Id == Int32.Parse(accountId));
				context.Items["Account"] = account;
			}
			catch
			{
				// do nothing if jwt validation fails
				// account is not attached to context so request won't have access to secure routes
			}
		}
	}
}
