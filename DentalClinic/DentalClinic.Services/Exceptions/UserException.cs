using System;

namespace DentalClinic.Services.Exceptions
{
    [Serializable]
    public class UserException : ApplicationException
    {
        public UserException() { }

        public UserException(string message) : base(message) { }
        public UserException(string message, Exception inner) : base(message, inner) { }

        protected UserException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
