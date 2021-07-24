using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class InvalidRatingException : HomuaiException
    {
        protected InvalidRatingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidRatingException() : base(ResourceTextException.RATE_SCORE_BETWEEN_ZERO_FIVE)
        {
        }
    }
}
