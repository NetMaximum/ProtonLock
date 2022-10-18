namespace NetMaximum.ProtonLock.Exceptions;

public class ProtonLockException : Exception
{
    public ProtonLockException()
    {
    }

    public ProtonLockException(string message) : base(message)
    {
    }

    public ProtonLockException(string message, Exception inner): base(message, inner)
    {
    }
}