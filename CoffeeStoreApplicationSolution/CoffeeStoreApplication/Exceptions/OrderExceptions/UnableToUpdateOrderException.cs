using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.OrderExceptions
{
    [Serializable]
    internal class UnableToUpdateOrderException : Exception
    {
        public UnableToUpdateOrderException()
        {
        }

        public UnableToUpdateOrderException(string? message) : base(message)
        {
        }

        public UnableToUpdateOrderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToUpdateOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}