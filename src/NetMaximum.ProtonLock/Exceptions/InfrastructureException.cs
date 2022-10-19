namespace NetMaximum.ProtonLock.Exceptions;

public class InfrastructureException : ProtonLockException
{
    public InfrastructureException()
    {
    }

    public InfrastructureException(string message)
        : base(message)
    {
    }

    public InfrastructureException(string message, Exception inner)
        : base(message, inner)
    {
    }
}