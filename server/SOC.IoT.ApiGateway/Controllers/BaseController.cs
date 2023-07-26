using Microsoft.AspNetCore.Mvc;
using SOC.IoT.ApiGateway.Entities;
using System.Security.Principal;

namespace SOC.IoT.ApiGateway.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        public User Account => (User)HttpContext.Items["User"];
    }
}
