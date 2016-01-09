using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinic.Services.Exceptions
{
    [Serializable]
    public class LeaveTypeException : ApplicationException
    {
        public LeaveTypeException() { }

        public LeaveTypeException(string message) : base(message) { }
        public LeaveTypeException(string message, Exception inner) : base(message, inner) { }

        protected LeaveTypeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }


    }
}
