using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.OrderItemExceptions
{
    [Serializable]
    public class NoSuchOrderItemException : Exception
    {
        public NoSuchOrderItemException()
        {
        }

        public NoSuchOrderItemException(string? message) : base(message)
        {
        }

        public NoSuchOrderItemException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoSuchOrderItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}