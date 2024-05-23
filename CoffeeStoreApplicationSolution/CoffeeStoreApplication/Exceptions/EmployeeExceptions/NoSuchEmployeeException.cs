using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.EmployeeExceptions
{
    [Serializable]
    internal class NoSuchEmployeeException : Exception
    {
        public NoSuchEmployeeException()
        {
        }

        public NoSuchEmployeeException(string? message) : base(message)
        {
        }

        public NoSuchEmployeeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoSuchEmployeeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}