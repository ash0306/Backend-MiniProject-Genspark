using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.OrderItemExceptions
{
    [Serializable]
    public class UnableToRemoveOrderItemException : Exception
    {
        public UnableToRemoveOrderItemException()
        {
        }

        public UnableToRemoveOrderItemException(string? message) : base(message)
        {
        }

        public UnableToRemoveOrderItemException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToRemoveOrderItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}