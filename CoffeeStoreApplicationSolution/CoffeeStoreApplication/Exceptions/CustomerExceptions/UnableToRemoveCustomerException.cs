using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.CustomerExceptions
{
    [Serializable]
    public class UnableToRemoveCustomerException : Exception
    {
        public UnableToRemoveCustomerException()
        {
        }

        public UnableToRemoveCustomerException(string? message) : base(message)
        {
        }

        public UnableToRemoveCustomerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToRemoveCustomerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}