using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.OrderItemExceptions
{
    [Serializable]
    public class UnableToAddOrderItemException : Exception
    {
        public UnableToAddOrderItemException()
        {
        }

        public UnableToAddOrderItemException(string? message) : base(message)
        {
        }

        public UnableToAddOrderItemException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToAddOrderItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}