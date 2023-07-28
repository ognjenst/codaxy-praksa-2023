using ConductorSharp.Engine.Interface;
using ConductorSharp.Engine.Model;
using ConductorSharp.Engine.Util;
using MediatR;
using SOC.Intelligence.DTO;
using SOC.Intelligence.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOC.Intelligence.Handler;

public class TicketingRequest : IRequest<IntelligenceResponseDto> 
{
	public string IpAddress { get; set; }
	public string MaxDaysInAge { get; set; }
}

[OriginalName("intelligence_task")]
public class IntelligenceHandler : ITaskRequestHandler<TicketingRequest, IntelligenceResponseDto>
{
	private readonly IIntelligenceService _intelligenceService;
    public IntelligenceHandler(IIntelligenceService intelligenceService)
    {
        _intelligenceService = intelligenceService;
    }
	
	// This method is delegating request handlond to the IntelligenceService
    public async Task<IntelligenceResponseDto> Handle(TicketingRequest request, CancellationToken cancellationToken)
	{
		return await _intelligenceService.CheckEndpoint(request.IpAddress, request.MaxDaysInAge);
	}
}
