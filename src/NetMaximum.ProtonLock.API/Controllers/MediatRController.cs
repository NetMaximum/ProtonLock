using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetMaximum.ProtonLock.API.Models;

namespace NetMaximum.ProtonLock.API.Controllers;

[ApiController]
[Route("[controller]")]
public class MediatRController : ControllerBase
{
    private readonly ILogger<LockController> _logger;
    private readonly IMediator _mediator;

    public MediatRController(ILogger<LockController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost("with-fingerprint")]
    public async Task<IActionResult> Post(WithFingerPrint model)
    {
        await _mediator.Send(new MediatRFingerPrint
        {
            Name = "Test Name"
        });
        
        return Accepted();
    }
    
    [HttpPost("without-fingerprint")]
    public async Task<IActionResult> PostWithout(WithoutFingerPrint model)
    {
        return null;
    }
}