using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.CustomerExceptions
{
    [Serializable]
    public class UnableToAddCustomerException : Exception
    {
        public UnableToAddCustomerException()
        {
        }

        public UnableToAddCustomerException(string? message) : base(message)
        {
        }

        public UnableToAddCustomerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnableToAddCustomerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}