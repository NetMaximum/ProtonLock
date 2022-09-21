# NetMaximum.ProtonLock 

ProtonLock is a simple library that leverages Redis to provide a mechanism to deal with concurrency issues. 


Examples available [https://github.com/NetMaximum/ProtonLock](https://github.com/NetMaximum/ProtonLock)

## Installation

## Core Library


Setup default timeout.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ProtonLock" : {
    "Timeout" : 10000
  }
}
```
Add ProtonLock to IOC.

```csharp

builder.Services.AddProtonLock(namedSection: builder.Configuration.GetSection("ProtonLock"));

```

Use ProtonLock 

```csharp
public class LockController : ControllerBase
{
    private readonly IClient _lockClient;

    public LockController(IClient lockClient)
    {
        _lockClient = lockClient;
    }

    [HttpPost("with-fingerprint")]
    public async Task<IActionResult> Post(WithFingerPrint model)
    {
        var duplicate = await _lockClient.DuplicateOccurenceAsync(model);
        if (duplicate)
        {
            return new ConflictResult();
        }

        return new OkResult();
    }
}
```

## With MediatR

Setup default timeout.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ProtonLock" : {
    "Timeout" : 10000
  }
}
```

Add ProtonLock to IOC.

```csharp
builder.Services.AddProtonLockWithMediatR(namedSection: builder.Configuration.GetSection("ProtonLock"));
```
Use ProtonLock

```csharp
public class MediatRController : ControllerBase
{
    private readonly IMediator _mediator;

    public MediatRController(IMediator mediator)
    {
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
}
```