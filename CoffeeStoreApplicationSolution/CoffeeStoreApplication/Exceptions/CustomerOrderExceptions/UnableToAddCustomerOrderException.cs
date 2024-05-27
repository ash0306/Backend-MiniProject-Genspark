using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.CustomerOrderExceptions
{
    [Serializable]
    public class UnableToAddCustomerOrderException : Exception
    {
        public UnableToAddCustomerOrderException()
        {
        }

        public UnableToAddCustomerOrderException(string? message) : base(message)
        {
        }

        public UnableToAddCustomerOrderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToAddCustomerOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}