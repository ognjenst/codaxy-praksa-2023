using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SOC.IoT.ApiGateway.Entities;
using SOC.IoT.ApiGateway.Entities.Contexts;
using SOC.IoT.ApiGateway.Models;
using SOC.IoT.ApiGateway.Models.Requests;
using SOC.IoT.ApiGateway.Services;

namespace SOC.IoT.ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly SOCIoTDbContext _context;

        public UserController(IUserService userService,
            IMapper mapper, SOCIoTDbContext context)
        {
            _userService = userService;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginRequest request)
        {
            var response = _userService.Login(request);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User request)
        {
            var response = _userService.Register(request);
            if (response == "Success")
            {
                return Ok(new { message = "User registered successfuly." });
            }
            else
            {
                return BadRequest(new { message = "Error" });
            }
        }
    }
}
