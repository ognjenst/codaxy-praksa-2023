using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using SOC.IoT.ApiGateway.Entities;
using SOC.IoT.ApiGateway.Entities.Contexts;
using System.Security.Principal;
using System.Web.Http.Dependencies;

namespace SOC.IoT.ApiGateway.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class PermissionAuthorizeAttribute : Attribute, IAuthorizationFilter
{
	private readonly string _permission;
	

	public PermissionAuthorizeAttribute(string permission)
	{
		_permission = permission;
	}

	public void OnAuthorization(AuthorizationFilterContext context)
	{
		var account = (User)context.HttpContext.Items["Account"];

		var _context = context.HttpContext.RequestServices.GetRequiredService<SOCIoTDbContext>();

		
		if (account == null) 
		{
			// not logged in or role not authorized
			context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
		}
		else {
			// check permissions

			var currentUserPermissions = _context.Permissions.Where(x => x.RoleId == account.RoleId).Select(x => x.Name).ToList();
			if (!currentUserPermissions.Any(x => x == _permission))
			{
				context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
			}
			
		}
	}
}