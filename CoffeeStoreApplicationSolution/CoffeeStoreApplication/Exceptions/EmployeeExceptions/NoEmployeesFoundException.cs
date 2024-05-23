using System.Runtime.Serialization;

namespace CoffeeStoreApplication.Exceptions.EmployeeExceptions
{
    [Serializable]
    internal class NoEmployeesFoundException : Exception
    {
        public NoEmployeesFoundException()
        {
        }

        public NoEmployeesFoundException(string? message) : base(message)
        {
        }

        public NoEmployeesFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoEmployeesFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}