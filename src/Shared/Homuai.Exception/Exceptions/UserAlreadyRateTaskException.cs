using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class UserAlreadyRateTaskException : HomuaiException
    {
        protected UserAlreadyRateTaskException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserAlreadyRateTaskException() : base(ResourceTextException.YOU_ALREADY_RATE_THIS_TASK)
        {
        }
    }
}
