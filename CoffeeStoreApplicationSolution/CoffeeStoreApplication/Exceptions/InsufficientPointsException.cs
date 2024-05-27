using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions
{
    [Serializable]
    public class InsufficientPointsException : Exception
    {
        public InsufficientPointsException()
        {
        }

        public InsufficientPointsException(string? message) : base(message)
        {
        }

        public InsufficientPointsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InsufficientPointsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}