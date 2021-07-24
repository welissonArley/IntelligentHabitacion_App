using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.ExceptionsBase
{
    [Serializable]
    public class InvalidLoginException : HomuaiException
    {
        protected InvalidLoginException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidLoginException() : base(ResourceTextException.USER_OR_PASSWORD_INVALID)
        {
        }
    }
}
