using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.EmployeeExceptions
{
    [Serializable]
    public class UnableToUpdateEmployeeException : Exception
    {
        public UnableToUpdateEmployeeException()
        {
        }

        public UnableToUpdateEmployeeException(string? message) : base(message)
        {
        }

        public UnableToUpdateEmployeeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToUpdateEmployeeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}