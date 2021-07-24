using Homuai.Exception.ExceptionsBase;
using System;
using System.Runtime.Serialization;

namespace Homuai.Exception.Exceptions
{
    [Serializable]
    public class MonthlyRentInvalidException : HomuaiException
    {
        protected MonthlyRentInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public MonthlyRentInvalidException() : base(ResourceTextException.MONTHLYRENT_INVALID)
        {
        }
    }
}
