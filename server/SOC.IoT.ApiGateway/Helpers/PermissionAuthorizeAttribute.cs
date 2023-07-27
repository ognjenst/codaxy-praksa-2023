using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SOC.IoT.ApiGateway.Entities;
using SOC.IoT.ApiGateway.Entities.Contexts;
using System.Security.Principal;
using System.Web.Http.Dependencies;

namespace SOC.IoT.ApiGateway.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class PermissionAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _scope;
    private readonly string _resource;

    public PermissionAuthorizeAttribute(string scope, string resource)
    {
        _scope = scope;
        _resource = resource;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
    }
}