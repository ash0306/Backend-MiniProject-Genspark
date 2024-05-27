using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.CustomerOrderExceptions
{
    [Serializable]
    public class NoCustomerOrdersFoundException : Exception
    {
        public NoCustomerOrdersFoundException()
        {
        }

        public NoCustomerOrdersFoundException(string? message) : base(message)
        {
        }

        public NoCustomerOrdersFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoCustomerOrdersFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}