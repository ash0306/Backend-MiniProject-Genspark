using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.OrderItemExceptions
{
    [Serializable]
    internal class NoOrderItemsFoundException : Exception
    {
        public NoOrderItemsFoundException()
        {
        }

        public NoOrderItemsFoundException(string? message) : base(message)
        {
        }

        public NoOrderItemsFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoOrderItemsFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}