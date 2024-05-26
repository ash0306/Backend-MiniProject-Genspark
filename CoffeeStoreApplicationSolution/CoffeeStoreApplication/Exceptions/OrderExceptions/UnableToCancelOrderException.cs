using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.OrderExceptions
{
    [Serializable]
    internal class UnableToCancelOrderException : Exception
    {
        public UnableToCancelOrderException()
        {
        }

        public UnableToCancelOrderException(string? message) : base(message)
        {
        }

        public UnableToCancelOrderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToCancelOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}