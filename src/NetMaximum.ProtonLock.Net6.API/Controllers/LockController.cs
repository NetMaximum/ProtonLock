using Microsoft.AspNetCore.Mvc;
using NetMaximum.ProtonLock.Net6.API.Models;

namespace NetMaximum.ProtonLock.Net6.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LockController : ControllerBase
{
    private readonly ILogger<LockController> _logger;
    private readonly IClient _lockClient;

    public LockController(ILogger<LockController> logger, IClient lockClient)
    {
        _logger = logger;
        _lockClient = lockClient;
    }

    [HttpPost("with-fingerprint")]
    public async Task<IActionResult> Post(WithFingerPrint model)
    {
        return await CheckFingerprint(model);
    }
    
    [HttpPost("without-fingerprint")]
    public async Task<IActionResult> PostWithout(WithoutFingerPrint model)
    {
        return await CheckFingerprint(model);
    }

    private async Task<IActionResult> CheckFingerprint(WithoutFingerPrint model)
    {
        var duplicate = await _lockClient.DuplicateOccurenceAsync(model);
        if (duplicate)
        {
            return new ConflictResult();
        }

        return new OkResult();
    }
}