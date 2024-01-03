using System;

namespace Shared.DTO
{
    public class ServiceResponse<T>
    {
        public ServiceResponse(bool succeeded, string message)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public ServiceResponse(bool succeeded, string message, T result) : this(succeeded, message)
        {
            Result = result;
        }

        public bool Succeeded { get; private set; }
        public string Message { get; private set; }
        public T? Result { get; private set; }
    }
}
