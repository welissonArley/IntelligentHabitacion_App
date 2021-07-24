using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class EmailEmptyException : HomuaiException
    {
        protected EmailEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public EmailEmptyException() : base(ResourceTextException.EMAIL_EMPTY)
        {
        }
    }
}
