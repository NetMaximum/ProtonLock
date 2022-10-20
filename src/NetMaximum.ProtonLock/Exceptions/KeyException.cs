namespace NetMaximum.ProtonLock.Exceptions;

public class KeyException : ProtonLockException
{
    public KeyException(string message)
        : base(message)
    {
    }
}