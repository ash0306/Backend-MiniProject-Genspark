using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.CustomerExceptions
{
    [Serializable]
    public class UnableToUpdateCustomerException : Exception
    {
        public UnableToUpdateCustomerException()
        {
        }

        public UnableToUpdateCustomerException(string? message) : base(message)
        {
        }

        public UnableToUpdateCustomerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToUpdateCustomerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}