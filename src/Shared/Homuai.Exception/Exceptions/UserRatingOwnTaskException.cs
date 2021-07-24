using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class UserRatingOwnTaskException : HomuaiException
    {
        protected UserRatingOwnTaskException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public UserRatingOwnTaskException() : base(ResourceTextException.YOU_CANNOT_RATE_YOUR_OWN_TASK)
        {
        }
    }
}
