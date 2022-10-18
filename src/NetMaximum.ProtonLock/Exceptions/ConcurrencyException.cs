namespace NetMaximum.ProtonLock.Exceptions;

public class ConcurrencyException : ProtonLockException
{
    public ConcurrencyException()
    {
    }

    public ConcurrencyException(string message)
        : base(message)
    {
    }

    public ConcurrencyException(string message, Exception inner)
        : base(message, inner)
    {
    }
}