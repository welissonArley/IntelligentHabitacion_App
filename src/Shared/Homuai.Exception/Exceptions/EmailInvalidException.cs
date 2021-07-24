using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class EmailInvalidException : HomuaiException
    {
        protected EmailInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public EmailInvalidException() : base(ResourceTextException.EMAIL_INVALID)
        {
        }
    }
}
