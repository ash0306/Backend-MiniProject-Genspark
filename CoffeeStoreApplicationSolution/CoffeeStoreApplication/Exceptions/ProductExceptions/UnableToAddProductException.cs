using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.ProductExceptions
{
    [Serializable]
    public class UnableToAddProductException : Exception
    {
        public UnableToAddProductException()
        {
        }

        public UnableToAddProductException(string? message) : base(message)
        {
        }

        public UnableToAddProductException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToAddProductException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}