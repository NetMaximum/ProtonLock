using Microsoft.AspNetCore.Mvc;
using NetMaximum.ProtonLock.API.Models;

namespace NetMaximum.ProtonLock.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LockController : ControllerBase
{
    private readonly ILogger<LockController> _logger;

    public LockController(ILogger<LockController> logger)
    {
        _logger = logger;
    }

    [HttpPost("with-fingerprint")]
    public IActionResult Post(WithFingerPrint model)
    {
        return new OkResult();
    }
    
    [HttpPost("without-fingerprint")]
    public IActionResult PostWithout(WithoutFingerPrint model)
    {
        return new OkResult();
    }
}