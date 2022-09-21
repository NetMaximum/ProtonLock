using MediatR;
using Microsoft.AspNetCore.Mvc;
using NetMaximum.ProtonLock.API.Models;
using NetMaximum.ProtonLock.Exceptions;

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
        try
        {
            await _mediator.Send(new MediatRFingerPrint
            {
                Name = model.Name
            });
        }
        catch (ConcurrencyException e)
        {
            return Conflict();
        }
        
        return Ok();
    }
    
    [HttpPost("without-fingerprint")]
    public async Task<IActionResult> PostWithout(WithoutFingerPrint model)
    {
        
        
        await _mediator.Send(new MediatRWithoutFingerPrint()
        {
            Name = model.Name
        });
        
        return Ok();
    }
}