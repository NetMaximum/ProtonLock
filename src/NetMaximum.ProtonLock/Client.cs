using NetMaximum.ProtonLock.Exceptions;
using StackExchange.Redis;

namespace NetMaximum.ProtonLock;

public class Client : IClient
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly TimeSpan _elapsed;

    public Client(IConnectionMultiplexer connectionMultiplexer, TimeSpan elapsed)
    {
        _connectionMultiplexer = connectionMultiplexer;
        _elapsed = elapsed;
    }
    public async Task<bool> DuplicateOccurenceAsync(object model)
    {
        bool flag = false;
        
        try
        {
            if (model is IFingerprint fingerprint)
            {
                var requirement = fingerprint.FingerPrint();
                flag = !await _connectionMultiplexer.GetDatabase(1)
                    .StringSetAsync(
                        requirement.Fingerprint, 
                        RedisValue.EmptyString, 
                        requirement.Duration ?? _elapsed,
                        When.NotExists, 
                        CommandFlags.DemandMaster);
            }
              
        }  
        catch (Exception ex)
        {
            throw new InfrastructureException(ex.Message, ex);
        }  
  
        return flag;  
    }
}