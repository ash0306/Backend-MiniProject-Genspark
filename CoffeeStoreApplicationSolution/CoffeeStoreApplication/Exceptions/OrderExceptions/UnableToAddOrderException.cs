using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.OrderExceptions
{
    [Serializable]
    internal class UnableToAddOrderException : Exception
    {
        public UnableToAddOrderException()
        {
        }

        public UnableToAddOrderException(string? message) : base(message)
        {
        }

        public UnableToAddOrderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToAddOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}