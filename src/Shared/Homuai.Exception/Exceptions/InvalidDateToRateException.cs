using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class InvalidDateToRateException : HomuaiException
    {
        protected InvalidDateToRateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidDateToRateException() : base(ResourceTextException.RATE_TASK_JUST_CURRENT_MONTHS_SCHEDULE)
        {
        }
    }
}
