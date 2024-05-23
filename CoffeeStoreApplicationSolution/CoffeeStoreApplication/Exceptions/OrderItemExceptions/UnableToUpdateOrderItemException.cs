using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.OrderItemExceptions
{
    [Serializable]
    internal class UnableToUpdateOrderItemException : Exception
    {
        public UnableToUpdateOrderItemException()
        {
        }

        public UnableToUpdateOrderItemException(string? message) : base(message)
        {
        }

        public UnableToUpdateOrderItemException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToUpdateOrderItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}