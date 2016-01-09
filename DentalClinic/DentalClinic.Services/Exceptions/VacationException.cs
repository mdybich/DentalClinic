using System;

namespace DentalClinic.Services.Exceptions
{
    [Serializable]
    public class VacationException : ApplicationException
    {
        public VacationException() { }

        public VacationException(string message) : base(message) { }
        public VacationException(string message, Exception inner) : base(message, inner) { }

        protected VacationException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
