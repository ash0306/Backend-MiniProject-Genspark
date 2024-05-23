using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.ProductExceptions
{
    [Serializable]
    internal class NoSuchProductException : Exception
    {
        public NoSuchProductException()
        {
        }

        public NoSuchProductException(string? message) : base(message)
        {
        }

        public NoSuchProductException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoSuchProductException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}