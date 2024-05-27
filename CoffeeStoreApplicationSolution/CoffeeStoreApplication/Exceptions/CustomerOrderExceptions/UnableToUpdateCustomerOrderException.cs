using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.CustomerOrderExceptions
{
    [Serializable]
    public class UnableToUpdateCustomerOrderException : Exception
    {
        public UnableToUpdateCustomerOrderException()
        {
        }

        public UnableToUpdateCustomerOrderException(string? message) : base(message)
        {
        }

        public UnableToUpdateCustomerOrderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToUpdateCustomerOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}