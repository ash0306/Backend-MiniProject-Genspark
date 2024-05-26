using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.ProductExceptions
{
    [Serializable]
    internal class ProductOutOfStockException : Exception
    {
        public ProductOutOfStockException()
        {
        }

        public ProductOutOfStockException(string? message) : base(message)
        {
        }

        public ProductOutOfStockException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ProductOutOfStockException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}