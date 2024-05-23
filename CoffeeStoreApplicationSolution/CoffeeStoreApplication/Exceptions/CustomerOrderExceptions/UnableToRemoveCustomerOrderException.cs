using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.CustomerOrderExceptions
{
    [Serializable]
    internal class UnableToRemoveCustomerOrderException : Exception
    {
        public UnableToRemoveCustomerOrderException()
        {
        }

        public UnableToRemoveCustomerOrderException(string? message) : base(message)
        {
        }

        public UnableToRemoveCustomerOrderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToRemoveCustomerOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}