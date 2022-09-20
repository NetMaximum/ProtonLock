namespace NetMaximum.ProtonLock;

public interface IClient
{
    Task<bool> DuplicateOccurenceAsync(object fingerprint);
}