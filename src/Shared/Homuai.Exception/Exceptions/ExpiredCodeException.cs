using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class ExpiredCodeException : HomuaiException
    {
        protected ExpiredCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ExpiredCodeException() : base(ResourceTextException.EXPIRED_CODE)
        {
        }
    }
}
