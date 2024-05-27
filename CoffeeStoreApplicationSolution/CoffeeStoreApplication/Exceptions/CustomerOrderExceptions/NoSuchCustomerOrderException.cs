using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.CustomerOrderExceptions
{
    [Serializable]
    public class NoSuchCustomerOrderException : Exception
    {
        public NoSuchCustomerOrderException()
        {
        }

        public NoSuchCustomerOrderException(string? message) : base(message)
        {
        }

        public NoSuchCustomerOrderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoSuchCustomerOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}