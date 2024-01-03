using System;

namespace Auth.Domain.Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string message) : base(message)
        {

        }
    }
}
