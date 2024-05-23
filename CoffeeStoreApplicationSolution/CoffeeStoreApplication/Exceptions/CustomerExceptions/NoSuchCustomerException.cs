using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.CustomerExceptions
{
    [Serializable]
    internal class NoSuchCustomerException : Exception
    {
        public NoSuchCustomerException()
        {
        }

        public NoSuchCustomerException(string? message) : base(message)
        {
        }

        public NoSuchCustomerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoSuchCustomerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}