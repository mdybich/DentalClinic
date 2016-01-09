using System;

namespace DentalClinic.Services.Exceptions
{
    [Serializable]
    public class LeaveException : ApplicationException
    {
        public LeaveException() { }

        public LeaveException(string message) : base(message) { }
        public LeaveException(string message, Exception inner) : base(message, inner) { }

        protected LeaveException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
