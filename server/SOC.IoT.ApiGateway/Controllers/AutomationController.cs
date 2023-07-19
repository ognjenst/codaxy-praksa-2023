﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOC.Conductor.Client.Generated;

namespace SOC.IoT.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutomationController : ControllerBase
    {
        private readonly IAutomationService _automationService;

        public AutomationController(IAutomationService automationService)
        {
            _automationService = automationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAutomationAsync(AutomationDto automationDto)
        {
            var automation = await _automationService.AutomationAsync(automationDto);

            await Task.Delay(1000);

            return Ok(automation);
        }
    }
}