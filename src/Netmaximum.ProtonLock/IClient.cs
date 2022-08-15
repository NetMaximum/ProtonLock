namespace Netmaximum.ProtonLock;

public interface IClient
{
    Task<bool> DuplicateOccurenceAsync(IFingerprint fingerprint);
}