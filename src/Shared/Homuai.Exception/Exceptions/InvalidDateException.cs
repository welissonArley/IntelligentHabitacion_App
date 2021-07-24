using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class InvalidDateException : HomuaiException
    {
        protected InvalidDateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public InvalidDateException(DateTime date) : base(string.Format(ResourceTextException.DATE_MUST_BE_LESS_THAN, date.ToShortDateString()))
        {
        }
    }
}
