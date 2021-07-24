using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class PhoneNumberEmptyException : HomuaiException
    {
        protected PhoneNumberEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public PhoneNumberEmptyException() : base(ResourceTextException.PHONENUMBER_EMPTY)
        {
        }
    }
}
