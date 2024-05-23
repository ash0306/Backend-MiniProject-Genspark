using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.OrderExceptions
{
    [Serializable]
    internal class UnableToRemoveOrderException : Exception
    {
        public UnableToRemoveOrderException()
        {
        }

        public UnableToRemoveOrderException(string? message) : base(message)
        {
        }

        public UnableToRemoveOrderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToRemoveOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}