using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.ProductExceptions
{
    [Serializable]
    internal class NoProductsFoundException : Exception
    {
        public NoProductsFoundException()
        {
        }

        public NoProductsFoundException(string? message) : base(message)
        {
        }

        public NoProductsFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoProductsFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}