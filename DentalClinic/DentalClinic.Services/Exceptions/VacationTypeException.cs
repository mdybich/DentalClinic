using System;

namespace DentalClinic.Services.Exceptions
{
    [Serializable]
    public class VacationTypeException : ApplicationException
    {
        public VacationTypeException() { }

        public VacationTypeException(string message) : base(message) { }
        public VacationTypeException(string message, Exception inner) : base(message, inner) { }

        protected VacationTypeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
