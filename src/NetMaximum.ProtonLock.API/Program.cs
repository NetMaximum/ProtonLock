using MediatR;
using NetMaximum.ProtonLock.Extensions;
using NetMaximum.ProtonLock.MediatR.Extensions;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigurationOptions configuration = new ConfigurationOptions
{
    AbortOnConnectFail = true,
    ConnectTimeout = 5000,
    User = "default",
    Password = "redispw"
};
configuration.EndPoints.Add("localhost:55039");

builder.Services.AddSingleton<IConnectionMultiplexer>(  _ => ConnectionMultiplexer.Connect(configuration, Console.Out));
builder.Services.AddProtonLock();

builder.Services.AddMediatR(typeof(Program));
builder.Services.AddProtonLockWithMediatR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public interface IMarker
{
    
}