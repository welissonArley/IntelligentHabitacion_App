using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class InvalidTaskException : HomuaiException
    {
        protected InvalidTaskException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidTaskException() : base(ResourceTextException.INVALID_TASK)
        {
        }
    }
}
