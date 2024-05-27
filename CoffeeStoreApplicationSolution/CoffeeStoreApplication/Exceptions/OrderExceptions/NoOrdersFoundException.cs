using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.OrderExceptions
{
    [Serializable]
    public class NoOrdersFoundException : Exception
    {
        public NoOrdersFoundException()
        {
        }

        public NoOrdersFoundException(string? message) : base(message)
        {
        }

        public NoOrdersFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoOrdersFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}