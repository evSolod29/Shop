namespace Auth.Application.Exceptions;

public class AccessDenidedException : Exception
{
    public AccessDenidedException(string message) : base(message)
    {

    }
}
