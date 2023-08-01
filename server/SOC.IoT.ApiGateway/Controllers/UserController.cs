using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SOC.IoT.ApiGateway.Entities;
using SOC.IoT.ApiGateway.Entities.Contexts;
using SOC.IoT.ApiGateway.Models;
using SOC.IoT.ApiGateway.Models.Requests;
using SOC.IoT.ApiGateway.Models.Responses;
using SOC.IoT.ApiGateway.Options;
using SOC.IoT.ApiGateway.Services;
using System.Formats.Asn1;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SOC.IoT.ApiGateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly SOCIoTDbContext _context;
        private readonly JwtSecret _options;

        public UserController(IUserService userService,
            IMapper mapper, SOCIoTDbContext context, IOptions<JwtSecret> options)
        {
            _userService = userService;
            _mapper = mapper;
            _context = context;
            _options = options.Value;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
        {
            var response = _userService.Login(request);
            return Ok(response);
        }

        [AllowAnonymous]
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

        [Authorize]
        [HttpPost("login-test")]
        public async Task<IActionResult> LoginTest(LoginRequest request)
        {
            return Ok("Poruka");
        }
    }
}
