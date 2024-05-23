using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.ProductExceptions
{
    [Serializable]
    internal class UnableToRemoveProductException : Exception
    {
        public UnableToRemoveProductException()
        {
        }

        public UnableToRemoveProductException(string? message) : base(message)
        {
        }

        public UnableToRemoveProductException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToRemoveProductException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}