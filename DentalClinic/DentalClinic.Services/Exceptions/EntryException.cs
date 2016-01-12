using System;

namespace DentalClinic.Services.Exceptions
{
    [Serializable]
    public class EntryException : ApplicationException
    {
        public EntryException() { }

        public EntryException(string message) : base(message) { }
        public EntryException(string message, Exception inner) : base(message, inner) { }

        protected EntryException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
