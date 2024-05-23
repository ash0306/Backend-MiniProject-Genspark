using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.EmployeeExceptions
{
    [Serializable]
    internal class UnableToRemoveEmployeeException : Exception
    {
        public UnableToRemoveEmployeeException()
        {
        }

        public UnableToRemoveEmployeeException(string? message) : base(message)
        {
        }

        public UnableToRemoveEmployeeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToRemoveEmployeeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}