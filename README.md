# NetMaximum.ProtonLock 

[![https://img.shields.io/nuget/v/netmaximum.protonlock?style=for-the-badge](https://img.shields.io/nuget/v/netmaximum.protonlock?style=for-the-badge)](https://www.nuget.org/packages/NetMaximum.ProtonLock "Markdown Land")


ProtonLock is a simple library that leverages Redis to provide a mechanism to deal with concurrency issues. In the future other backends will be supported such as :

* Postgres
* Sql Server


## Examples

The quickest way to get up and running is to look at the test folder available at [https://github.com/NetMaximum/ProtonLock](https://github.com/NetMaximum/ProtonLock).

## Installation

## Core Library

Setup default timeout. This is usually stored in your app.*.json configuration file.

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
Add ProtonLock to IOC. (Usually Program.cs)

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

Setup default timeout. This is usually be stored in your app.*.json configuration file.

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

Add ProtonLock to IOC. (Usually Program.cs)

```csharp
builder.Services.AddProtonLockWithMediatR(namedSection: builder.Configuration.GetSection("ProtonLock"));
```

Next an object that implements ```IFingerPrint``` needs to be implemented:

```csharp
public class MediatRFingerPrint : IFingerprint, IRequest<Unit>
{
    public string Name { get; set; } = string.Empty;
    public int age { get; set; } = 10;
    
    public string FingerPrint()
    {
        
        return Name + age;
    }
    // Async Task for allowing other things....
}
```
When the pipeline behaviour in mediator detects this interface it'll call the ```FingerPrint``` method and check with the backend store for potential concurrency issues. If a concurrency issue is detected a ```ConcurrencyException``` will be thrown.


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