using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.EmployeeExceptions
{
    [Serializable]
    public class UnableToAddEmployeeException : Exception
    {
        public UnableToAddEmployeeException()
        {
        }

        public UnableToAddEmployeeException(string? message) : base(message)
        {
        }

        public UnableToAddEmployeeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToAddEmployeeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}