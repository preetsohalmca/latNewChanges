using System;
using System.Runtime.Serialization;
using Volvo.NVS.Utilities.Exceptions;

namespace Volvo.LAT.UserDomain.DomainLayer.Common.Utilities.GenericExceptions
{
    public class UserValidationException : NVSValidationException
    {
        public UserValidationException()
        {
        }

        public UserValidationException(string message)
            : base(message)
        {
        }

        public UserValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected UserValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
