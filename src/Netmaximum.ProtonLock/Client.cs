using StackExchange.Redis;

namespace Netmaximum.ProtonLock;

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
                var key = fingerprint.FingerPrint();
                flag = !await _connectionMultiplexer.GetDatabase(1)
                    .StringSetAsync(
                        key, 
                        RedisValue.EmptyString, 
                        _elapsed,
                        When.NotExists, 
                        CommandFlags.DemandMaster);
            }
              
        }  
        catch (Exception ex)  
        {
            flag = true;  
        }  
  
        return flag;  
    }
}