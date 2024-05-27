using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.ProductExceptions
{
    [Serializable]
    public class UnableToUpdateProductException : Exception
    {
        public UnableToUpdateProductException()
        {
        }

        public UnableToUpdateProductException(string? message) : base(message)
        {
        }

        public UnableToUpdateProductException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToUpdateProductException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}